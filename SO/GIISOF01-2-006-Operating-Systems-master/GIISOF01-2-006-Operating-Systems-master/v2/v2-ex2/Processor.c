#include "Processor.h"
#include "ProcessorBase.h"
#include "OperatingSystem.h"
#include "Buses.h"
#include "MMU.h"
#include <stdio.h>
#include <string.h>
#include "Clock.h"

// Internals Functions prototypes

int Processor_FetchInstruction();
void Processor_DecodeAndExecuteInstruction();
void Processor_ManageInterrupts();
void Processor_ShowTime(char section);

// Processor registers
int registerPC_CPU; // Program counter
int registerAccumulator_CPU; // Accumulator
MEMORYCELL registerIR_CPU; // Instruction register
unsigned int registerPSW_CPU = 128; // Processor state word, initially protected mode
int registerMAR_CPU; // Memory Address Register
MEMORYCELL registerMBR_CPU; // Memory Buffer Register

int registerA_CPU; // General purpose register

int interruptLines_CPU; // Processor interrupt lines

// interrupt vector table: an array of handle interrupt memory addresses routines
int interruptVectorTable[INTERRUPTTYPES];

// For PSW show "--------X---FNZS"
char pswmask []="----------------";

// Initialization of the interrupt vector table
void Processor_InitializeInterruptVectorTable(int interruptVectorInitialAddress) {
	int i;
	for (i=0; i< INTERRUPTTYPES;i++)  // Inicialice all to inicial YRET
		interruptVectorTable[i]=interruptVectorInitialAddress-1;

	interruptVectorTable[SYSCALL_BIT]=interruptVectorInitialAddress;  // SYSCALL_BIT=2
	interruptVectorTable[EXCEPTION_BIT]=interruptVectorInitialAddress+2; // EXCEPTION_BIT=6
	interruptVectorTable[CLOCKINT_BIT]=interruptVectorInitialAddress+4; // ECEPTION_BIT=9
}


// This is the instruction cycle loop (fetch, decoding, execution, etc.).
// The processor stops working when an POWEROFF signal is stored in its
// PSW register
void Processor_InstructionCycleLoop() {

	while (!Processor_PSW_BitState(POWEROFF_BIT)) {
		if (Processor_FetchInstruction()==CPU_SUCCESS)
			Processor_DecodeAndExecuteInstruction();
		if (interruptLines_CPU && !Processor_PSW_BitState(INTERRUPT_MASKED_BIT))
			Processor_ManageInterrupts();
	}
}

// Fetch an instruction from main memory and put it in the IR register
int Processor_FetchInstruction() {

	// The instruction must be located at the logical memory address pointed by the PC register
	registerMAR_CPU=registerPC_CPU;
	// Send to the MMU the address in which the reading has to take place: use the address bus for this
	Buses_write_AddressBus_From_To(CPU, MMU);
	// Tell the main memory controller to read
	if (MMU_readMemory()==MMU_SUCCESS) {
		// All the read data is stored in the MBR register. Because it is an instruction
		// we have to copy it to the IR register
		memcpy((void *) (&registerIR_CPU), (void *) (&registerMBR_CPU), sizeof(MEMORYCELL));
		// Show initial part of HARDWARE message with Operation Code and operands
		// Show message: operationCode operand1 operand2
		char codedInstruction[13]; // Coded instruction with separated fields to show
		Processor_GetCodedInstruction(codedInstruction,registerIR_CPU);
		Processor_ShowTime(HARDWARE);
		ComputerSystem_DebugMessage(1, HARDWARE, codedInstruction);
	}
	else {
		// Show message: "_ _ _ "
		Processor_ShowTime(HARDWARE);
		ComputerSystem_DebugMessage(2,HARDWARE);
		return CPU_FAIL;
	}
	return CPU_SUCCESS;
}


// Decode and execute the instruction in the IR register
void Processor_DecodeAndExecuteInstruction() {
	int tempAcc; // for save accumulator if necesary

	// Decode
	char operationCode=Processor_DecodeOperationCode(registerIR_CPU);
	int operand1=Processor_DecodeOperand1(registerIR_CPU);
	int operand2=Processor_DecodeOperand2(registerIR_CPU);

	Processor_DeactivatePSW_Bit(OVERFLOW_BIT);

	// Execute
	switch (operationCode) {

		// Instruction ADD
		case 'a':
			registerAccumulator_CPU= operand1 + operand2;
			Processor_CheckOverflow(operand1,operand2);
			registerPC_CPU++;
			break;

		// Instruction  MEMADD operand1 memAdress
		case 'm':
			registerMAR_CPU = operand2;
			// Send to the main memory controller the address in which the reading has to take place: use the address bus for this
			Buses_write_AddressBus_From_To(CPU,MMU);
			// Tell the main memory controller to read
			MMU_readMemory();
			registerAccumulator_CPU = operand1 + registerMBR_CPU.cell;
			Processor_CheckOverflow(operand1, registerMBR_CPU.cell);
			registerPC_CPU++;
			break;

		// Instruction SHIFT (SAL and SAR)
		case 's':
			  operand1<0 ? (registerAccumulator_CPU <<= (-operand1)) : (registerAccumulator_CPU >>= operand1);
			  registerPC_CPU++;
			  break;

		// Instruction DIV
		case 'd':
			if (operand2 == 0)
				Processor_RaiseInterrupt(EXCEPTION_BIT);
			else {
				registerAccumulator_CPU=operand1 / operand2;
				registerPC_CPU++;
			}
			break;

		// Instruction TRAP
		case 't':
			Processor_RaiseInterrupt(SYSCALL_BIT);
			registerA_CPU=operand1;
			registerPC_CPU++;
			break;

		// Instruction NOP
		case 'n':
			registerPC_CPU++;
			break;

		// Instruction JUMP
		case 'j':
			registerPC_CPU+= operand1;
			break;

		// Instruction ZJUMP
		case 'z':  // Jump if ZERO_BIT on
			if (Processor_PSW_BitState(ZERO_BIT))
				registerPC_CPU+= operand1;
			else
				registerPC_CPU++;
			break;

		// Instruction WRITE
		case 'w':
			registerMBR_CPU.cell=registerAccumulator_CPU;
			registerMAR_CPU=operand1;
			// Send to the main memory controller the data to be written: use the data bus for this
			Buses_write_DataBus_From_To(CPU, MAINMEMORY);
			// Send to the main memory controller the address in which the writing has to take place: use the address bus for this
			Buses_write_AddressBus_From_To(CPU, MMU);
			// Tell the main memory controller to write
			MMU_writeMemory();
			registerPC_CPU++;
			break;

		// Instruction READ
		case 'r':
			registerMAR_CPU=operand1;
			// Send to the main memory controller the address in which the reading has to take place: use the address bus for this
			Buses_write_AddressBus_From_To(CPU, MMU);
			// Tell the main memory controller to read
			MMU_readMemory();
			// Copy the read data to the accumulator register
			registerAccumulator_CPU= registerMBR_CPU.cell;
			registerPC_CPU++;
			break;

		// Instruction INC
		case 'i':
			tempAcc=registerAccumulator_CPU;
			registerAccumulator_CPU += operand1;
			Processor_CheckOverflow(tempAcc,operand1);
			registerPC_CPU++;
			break;

		// Instruction HALT
		case 'h':
			if(Processor_PSW_BitState(EXECUTION_MODE_BIT)) {
				Processor_ActivatePSW_Bit(POWEROFF_BIT);
			} else {
				Processor_RaiseInterrupt(EXCEPTION_BIT);
			}
			break;

		// Instruction OS
		case 'o': // Make a operating system routine in entry point indicated by operand1
			if(Processor_PSW_BitState(EXECUTION_MODE_BIT)) {
				// Show final part of HARDWARE message with CPU registers
				// Show message: " (PC: registerPC_CPU, Accumulator: registerAccumulator_CPU, PSW: registerPSW_CPU [Processor_ShowPSW()]\n
				Processor_ShowTime(HARDWARE);
				ComputerSystem_DebugMessage(3, HARDWARE,operationCode,operand1,operand2,registerPC_CPU,registerAccumulator_CPU,registerPSW_CPU,Processor_ShowPSW());
				// Not all operating system code is executed in simulated processor, but really must do it...
				OperatingSystem_InterruptLogic(operand1);
				registerPC_CPU++;
				// Update PSW bits (ZERO_BIT, NEGATIVE_BIT, ...)
				Processor_UpdatePSW();
				return; // Note: message show before... for operating system messages after...
			} else {
				Processor_RaiseInterrupt(EXCEPTION_BIT);
			}
			break;

		// Instruction IRET
		case 'y': // Return from a interrupt handle manager call
			if(Processor_PSW_BitState(EXECUTION_MODE_BIT)) {
			registerPC_CPU=Processor_CopyFromSystemStack(MAINMEMORYSIZE-1);
			registerPSW_CPU=Processor_CopyFromSystemStack(MAINMEMORYSIZE-2);
		} else {
			Processor_RaiseInterrupt(EXCEPTION_BIT);
		}
			break;

		// Unknown instruction
		default :
			registerPC_CPU++;
			break;
	}

	// Update PSW bits (ZERO_BIT, NEGATIVE_BIT, ...)
	Processor_UpdatePSW();

	Processor_ShowTime(HARDWARE);
	// Show final part of HARDWARE message with	CPU registers
	// Show message: " (PC: registerPC_CPU, Accumulator: registerAccumulator_CPU, PSW: registerPSW_CPU [Processor_ShowPSW()]\n
	ComputerSystem_DebugMessage(3, HARDWARE, operationCode,operand1,operand2,registerPC_CPU,registerAccumulator_CPU,registerPSW_CPU,Processor_ShowPSW());
}


// Hardware interrupt processing
void Processor_ManageInterrupts() {

	int i;

		for (i=0;i<INTERRUPTTYPES;i++)
			// If an 'i'-type interrupt is pending
			if (Processor_GetInterruptLineStatus(i)) {
				// Deactivate interrupt
				Processor_ACKInterrupt(i);
				// Copy PC and PSW registers in the system stack
				Processor_CopyInSystemStack(MAINMEMORYSIZE-1, registerPC_CPU);
				Processor_CopyInSystemStack(MAINMEMORYSIZE-2, registerPSW_CPU);
				// Activate protected excution mode
				Processor_ActivatePSW_Bit(EXECUTION_MODE_BIT);
				// Call the appropriate OS interrupt-handling routine setting PC register
				registerPC_CPU=interruptVectorTable[i];
				break; // Don't process another interrupt
			}
}

char * Processor_ShowPSW(){
	strcpy(pswmask,"----------------");
	int tam=strlen(pswmask)-1;
	if (Processor_PSW_BitState(EXECUTION_MODE_BIT))
		pswmask[tam-EXECUTION_MODE_BIT]='X';
	if (Processor_PSW_BitState(OVERFLOW_BIT))
		pswmask[tam-OVERFLOW_BIT]='F';
	if (Processor_PSW_BitState(NEGATIVE_BIT))
		pswmask[tam-NEGATIVE_BIT]='N';
	if (Processor_PSW_BitState(ZERO_BIT))
		pswmask[tam-ZERO_BIT]='Z';
	if (Processor_PSW_BitState(POWEROFF_BIT))
		pswmask[tam-POWEROFF_BIT]='S';
	if (Processor_PSW_BitState(INTERRUPT_MASKED_BIT))
		pswmask[tam-INTERRUPT_MASKED_BIT]='M';

	return pswmask;
}


/////////////////////////////////////////////////////////
//  New functions below this line  //////////////////////

// Show time messages
void Processor_ShowTime(char section) {
        ComputerSystem_DebugMessage(98,section,Processor_PSW_BitState(EXECUTION_MODE_BIT)?"":"");
        ComputerSystem_DebugMessage(Processor_PSW_BitState(EXECUTION_MODE_BIT)?5:4,section,Clock_GetTime());
}
