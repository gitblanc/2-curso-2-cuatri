// Don't change this file !!!
#include "Processor.h"
#include "ProcessorBase.h"
// #include "OperatingSystem.h"
#include "Buses.h"
// #include "MMU.h"
#include <stdio.h>
#include <string.h>

extern int registerPC_CPU; // Program counter
extern int registerAccumulator_CPU; // Accumulator
extern int registerAccumulator_CPU; // Accumulator
extern MEMORYCELL registerIR_CPU; // Instruction register
extern unsigned int registerPSW_CPU; // Processor state word, initially protected mode
extern int registerMAR_CPU; // Memory Address Register
extern MEMORYCELL registerMBR_CPU; // Memory Buffer Register
extern int registerA_CPU; // General purpose register
extern int interruptLines_CPU; // Processor interrupt lines
extern int interruptVectorTable[];
extern char pswmask []; 

// Update PSW state
void Processor_UpdatePSW(){
	// Update ZERO_BIT
	if (registerAccumulator_CPU==0){
		if (!Processor_PSW_BitState(ZERO_BIT))
			Processor_ActivatePSW_Bit(ZERO_BIT);
	}
	else {
		if (Processor_PSW_BitState(ZERO_BIT))
			Processor_DeactivatePSW_Bit(ZERO_BIT);
	}
	
	// Update NEGATIVE_BIT
	if (registerAccumulator_CPU<0) {
		if (!Processor_PSW_BitState(NEGATIVE_BIT))
			Processor_ActivatePSW_Bit(NEGATIVE_BIT);
	}
	else {
		if (Processor_PSW_BitState(NEGATIVE_BIT))
			Processor_DeactivatePSW_Bit(NEGATIVE_BIT);
	}
}

// Check overflow, receive operands for add (if sub, change operand2 sign)
void Processor_CheckOverflow(int op1, int op2) {
			if ((op1>0 && op2>0 && registerAccumulator_CPU<0)
				|| (op1<0 && op2<0 && registerAccumulator_CPU>0))
				Processor_ActivatePSW_Bit(OVERFLOW_BIT);
}

// Save in the system stack a given value
void Processor_CopyInSystemStack(int physicalMemoryAddress, int data) {

	// registerMBR_CPU.operationCode=registerMBR_CPU.operand1=registerMBR_CPU.operand2=data;
	registerMBR_CPU.cell=data;
	registerMAR_CPU=physicalMemoryAddress;
	Buses_write_AddressBus_From_To(CPU, MAINMEMORY);
	Buses_write_DataBus_From_To(CPU, MAINMEMORY);	
	MainMemory_writeMemory();
}

// Get value from system stack
int Processor_CopyFromSystemStack(int physicalMemoryAddress) {

	registerMAR_CPU=physicalMemoryAddress;
	Buses_write_AddressBus_From_To(CPU, MAINMEMORY);
	MainMemory_readMemory();
	return registerMBR_CPU.cell;
}


// Put the specified interrupt line to a high level 
void Processor_RaiseInterrupt(const unsigned int interruptNumber) {
	unsigned int mask = 1;

	mask = mask << interruptNumber;
	interruptLines_CPU = interruptLines_CPU | mask;
}

// Put the specified interrupt line to a low level 
void Processor_ACKInterrupt(const unsigned int interruptNumber) {
	unsigned int mask = 1;

	mask = mask << interruptNumber;
	mask = ~mask;

	interruptLines_CPU = interruptLines_CPU & mask;
}

// Returns the state of a given interrupt line (1=high level, 0=low level)
unsigned int Processor_GetInterruptLineStatus(const unsigned int interruptNumber) {
	unsigned int mask = 1;

	mask = mask << interruptNumber;
	return (interruptLines_CPU & mask) >> interruptNumber;
}

// Set a given bit position in the PSW register
void Processor_ActivatePSW_Bit(const unsigned int nbit) {
	unsigned int mask = 1;

	mask = mask << nbit;
	
	registerPSW_CPU = registerPSW_CPU | mask;
}

// Unset a given bit position in the PSW register
void Processor_DeactivatePSW_Bit(const unsigned int nbit) {
	unsigned int mask = 1;

	mask = mask << nbit;
	mask = ~mask;

	registerPSW_CPU = registerPSW_CPU & mask;
}

// Returns the state of a given bit position in the PSW register
unsigned int Processor_PSW_BitState(const unsigned int nbit) {
	unsigned int mask = 1;

	mask = mask << nbit;
	return (registerPSW_CPU & mask) >> nbit;
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

// Setter for the Accumulator
void Processor_SetAccumulator(int acc){
  registerAccumulator_CPU=acc;
}

// Setter for the PC
void Processor_SetPC(int pc){
  registerPC_CPU= pc;
}

// Getter for the Accumulator
int Processor_GetAccumulator() {
  return registerAccumulator_CPU;
}

int Processor_GetRegisterA() {
  return registerA_CPU;
}

// Setter for the PSW
void Processor_SetPSW(unsigned int psw){
	registerPSW_CPU=psw;
}

// Getter for the PSW
unsigned int Processor_GetPSW(){
	return registerPSW_CPU;
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

void Processor_GetCodedInstruction(char * result, MEMORYCELL memCell){
	sprintf(result,"%02X %03X %03X",((registerIR_CPU.cell>>24)&0xff),((registerIR_CPU.cell>>12)&0xfff),(registerIR_CPU.cell&0xfff));
	
}