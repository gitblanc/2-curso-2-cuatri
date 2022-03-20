#include "Processor.h"
#include "ComputerSystem.h"
#include "Buses.h"
#include "MainMemory.h"
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

// Local Functions prototypes
void Processor_FetchInstruction();
void Processor_DecodeAndExecuteInstruction();
void Processor_ManageInterrupts();
char Processor_DecodeOperationCode(MEMORYCELL);
int Processor_DecodeOperand1(MEMORYCELL);
int Processor_DecodeOperand2(MEMORYCELL);


// Processor registers
int registerPC_CPU; // Program counter
int registerAccumulator_CPU; // Accumulator
MEMORYCELL registerIR_CPU; // Instruction register
unsigned int registerPSW_CPU; // Processor state word
int registerMAR_CPU; // Memory Address Register
MEMORYCELL registerMBR_CPU; // Memory Buffer Register

// Initialize processor registers
void Processor_InitializeRegisters(int regPC, int regAcum, unsigned int regPSW) {
	registerPC_CPU=regPC;
	registerAccumulator_CPU=regAcum;
	registerPSW_CPU=regPSW;
}

// This is the instruction cycle loop (fetch, decoding, execution, etc.).
// The processor stops working when an POWEROFF signal is stored in its
// PSW register
void Processor_InstructionCycleLoop() {

	while (registerPSW_CPU!=POWEROFF) {
		Processor_FetchInstruction();
		Processor_DecodeAndExecuteInstruction();
		Processor_ManageInterrupts();
	}
}

// Fetch an instruction from main memory and put it in the IR register
void Processor_FetchInstruction() {

	// The instruction must be located at the memory address pointed by the PC register
	registerMAR_CPU=registerPC_CPU;
	// Send to the main memory controller the address in which the reading has to take place: use the address bus for this
	Buses_write_AddressBus_From_To(CPU, MAINMEMORY);
	// Tell the main memory controller to read
	MainMemory_readMemory();
	// All the read data is stored in the MBR register. Because it is an instruction
	// we have to copy it to the IR register
	memcpy((void *) (&registerIR_CPU), (void *) (&registerMBR_CPU), sizeof(MEMORYCELL));
	// Show initial part of HARDWARE message with Operation Code and operands
	char result[20]; // Coded instruction with separated fields to show
	sprintf(result,"%02X %03X %03X",((registerIR_CPU.cell>>24)&0xff),((registerIR_CPU.cell>>12)&0xfff),(registerIR_CPU.cell&0xfff));
	ComputerSystem_DebugMessage(1, HARDWARE, Processor_DecodeOperationCode(registerIR_CPU), Processor_DecodeOperand1(registerIR_CPU), Processor_DecodeOperand2(registerIR_CPU), result);

}

// Decode and execute the instruction in the IR register
void Processor_DecodeAndExecuteInstruction() {
	// Decode
	char operationCode=Processor_DecodeOperationCode(registerIR_CPU);
	int operand1=Processor_DecodeOperand1(registerIR_CPU);
	int operand2=Processor_DecodeOperand2(registerIR_CPU);

	// Execute
	switch (operationCode) {

		// Instruction ADD
		// case 'a': registerAccumulator_CPU= registerIR_CPU.operand1 + registerIR_CPU.operand2;
		case 'a': registerAccumulator_CPU= operand1 + operand2;
			  registerPC_CPU++;
			  break;

		// Instruction SHIFT (SAL and SAR)
		case 's':
			  operand1<0 ? (registerAccumulator_CPU <<= (-operand1)) : (registerAccumulator_CPU >>= operand1);
			  registerPC_CPU++;
			  break;


		// Instruction NOP
		case 'n': registerPC_CPU++;
			  break;

		// Instruction JUMP
		// case 'j': registerPC_CPU+= registerIR_CPU.operand1;
		case 'j': registerPC_CPU+= operand1;
			  break;

		// Instruction ZJUMP
		case 'z': if (registerAccumulator_CPU==0)
				  // registerPC_CPU+= registerIR_CPU.operand1;
				  registerPC_CPU+= operand1;
			  else
				  registerPC_CPU++;
			  break;

		// Instruction WRITE
		// case 'w': registerMBR_CPU.operationCode= registerMBR_CPU.operand1= registerMBR_CPU.operand2= registerAccumulator_CPU;
		case 'w': registerMBR_CPU.cell= registerAccumulator_CPU;
			  // registerMAR_CPU=registerIR_CPU.operand1;
			  registerMAR_CPU=operand1;
			  // Send to the main memory controller the data to be written: use the data bus for this
			  Buses_write_DataBus_From_To(CPU, MAINMEMORY);
			  // Send to the main memory controller the address in which the writing has to take place: use the address bus for this
			  Buses_write_AddressBus_From_To(CPU, MAINMEMORY);
			  // Tell the main memory controller to write
			  MainMemory_writeMemory();
			  registerPC_CPU++;
			  break;

		// Instruction READ
		// case 'r': registerMAR_CPU=registerIR_CPU.operand1;
		case 'r': registerMAR_CPU=operand1;
			  // Send to the main memory controller the address in which the reading has to take place: use the address bus for this
			  Buses_write_AddressBus_From_To(CPU, MAINMEMORY);
			  // Tell the main memory controller to read
			  MainMemory_readMemory();
			  // Copy the read data to the accumulator register
			  // registerAccumulator_CPU= registerMBR_CPU.operand1;
			  registerAccumulator_CPU= registerMBR_CPU.cell;
			  registerPC_CPU++;
			  break;

		// Instruction INC
		// case 'i': registerAccumulator_CPU+= registerIR_CPU.operand1;
		case 'i': registerAccumulator_CPU+= operand1;
			  registerPC_CPU++;
			  break;

		// Instruction HALT
		case 'h': registerPSW_CPU=POWEROFF;
			  break;

		// Unknown instruction
		default : registerPC_CPU++;
			  break;
	}
	// Show final part of HARDWARE message with	CPU registers
	ComputerSystem_DebugMessage(3,HARDWARE,registerPC_CPU,registerAccumulator_CPU,registerPSW_CPU); // Exercise 1
}

// Hardware interrup processing
// Our primitive processor DOES NOT SUPPORT interrupts
void Processor_ManageInterrupts() {

}

// Getter for the registerMAR_CPU
int Processor_GetMAR() {
  return registerMAR_CPU;
}

// Setter for the registerMAR_CPU
void Processor_SetMAR(int data) {
  registerMAR_CPU=data;
}

// pseudo-getter for the registerMBR_CPU
void Processor_GetMBR(MEMORYCELL *toRegister) {
  memcpy((void*) toRegister, (void *) (&registerMBR_CPU), sizeof(MEMORYCELL));
}

// pseudo-setter for the registerMBR_CPU
void Processor_SetMBR(MEMORYCELL *fromRegister) {
  memcpy((void*) (&registerMBR_CPU), (void *) fromRegister, sizeof(MEMORYCELL));
}

int Processor_Encode(char opCode, int op1, int op2) {
	int mask=0x7ff; // binary: 0111 1111 1111
	int sigOp1=op1<0;
	op1=sigOp1 ? ((-op1) & mask) : (op1 & mask);
	int sigOp2=op2<0;
	op2=sigOp2 ? ((-op2) & mask) : (op2 & mask);
	int cell=(opCode<<24);
	cell = cell | (sigOp1<<23) | (op1<<12);
	cell = cell | (sigOp2<<11) | op2;
	return cell;
}

char Processor_DecodeOperationCode(MEMORYCELL memCell) {
	return (memCell.cell>>24) & 0xff;
}

int Processor_DecodeOperand1(MEMORYCELL memCell) {
	int sigOp1=memCell.cell & (0x1<<23);
	int op1=(memCell.cell & (0x7ff<<12))>>12;
	op1=sigOp1?-op1:op1;
	return op1;
}

int Processor_DecodeOperand2(MEMORYCELL memCell) {
	int sigOp2= memCell.cell & (0x1<<11);
	int op2=(memCell.cell & (0x7ff));
	op2=sigOp2?-op2:op2;
	return op2;
}
