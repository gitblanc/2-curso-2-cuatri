#ifndef PROCESSOR_H
#define PROCESSOR_H

#include "MainMemory.h"
#include "ProcessorBase.h"

#define INTERRUPTTYPES 10
#define CPU_SUCCESS 1
#define CPU_FAIL 0

// Enumerated type that connects bit positions in the PSW register with
// processor events and status
enum PSW_BITS {POWEROFF_BIT=0, ZERO_BIT=1, NEGATIVE_BIT=2, OVERFLOW_BIT=3, EXECUTION_MODE_BIT=7, INTERRUPT_MASKED_BIT=15};

// Enumerated type that connects bit positions in the interruptLines with
// interrupt types
enum INT_BITS {SYSCALL_BIT=2, EXCEPTION_BIT=6, CLOCKINT_BIT=9};

// Exception types from v4-ex1
enum EXCEPTIONS {DIVISIONBYZERO, INVALIDPROCESSORMODE, INVALIDADDRESS,
INVALIDINSTRUCTION};

// Functions prototypes
void Processor_InitializeInterruptVectorTable();
void Processor_InstructionCycleLoop();
void Processor_RaiseInterrupt(const unsigned int);
void Processor_RaiseException(int typeOfException);

char * Processor_ShowPSW();

#endif
