#include "OperatingSystem.h"
#include "OperatingSystemBase.h"
#include "MMU.h"
#include "Processor.h"
#include "Buses.h"
#include "Heap.h"
#include <string.h>
#include <ctype.h>
#include <stdlib.h>
#include <time.h>
#include <limits.h>
#include "QueueFIFO.h"
#include "Device.h"

// File structure. To jump to a section just search for the followin comments
// in the code.
// - FUNCTION DEFINITIONS
// - VARIABLE DEFINITIONS
// - OS MANAGEMENT
// - MEMORY MANAGEMENT
// - PROCESSOR MANAGEMENT
// - I/O MANAGEMENT
// - PROCESSES MANAGEMENT
// - EXCEPTIONS / INTERRUPTS MANAGEMENT

// -----------------------------------------------------------------------------
// ------------------------- FUNCTION DEFINITIONS ------------------------------
// -----------------------------------------------------------------------------

// Functions prototypes
void OperatingSystem_PrepareDaemons();
void OperatingSystem_PCBInitialization(int, int, int, int, int);
void OperatingSystem_MoveToTheREADYState(int);
void OperatingSystem_Dispatch(int);
void OperatingSystem_RestoreContext(int);
void OperatingSystem_SaveContext(int);
void OperatingSystem_TerminateProcess();
int OperatingSystem_LongTermScheduler();
void OperatingSystem_PreemptRunningProcess();
int OperatingSystem_CreateProcess(int);
int OperatingSystem_ObtainMainMemory(int, int);
int OperatingSystem_ShortTermScheduler();
int OperatingSystem_ExtractFromReadyToRun();
void OperatingSystem_HandleException();
void OperatingSystem_HandleSystemCall();
void OperatingSystem_HandleClockInterrupt();
void OperatingSystem_PrintReadyToRunQueue();
void OperatingSystem_HandleIOEndInterrupt();
void OperatingSystem_IOScheduler();
void OperatingSystem_DeviceControlerStartIOOperation();
int OperatingSystem_DeviceControlerEndIOOperation();
int OperatingSystem_GetWhenToWakeUpTime();
int OperatingSystem_UpdateExecutingProcessor();
int OperatingSystem_UpdateExecutingProcess();
int OperatingSystem_GetMostImportantREADYProcessInfo();
int OperatingSystem_IncreseNumberOfClockInterrupts();
int OperatingSystem_CheckIfShutdown();
int OperatingSystem_ReleaseMainMemory(int PID);
int OperatingSystem_GetBiggestMemoryBlockSize();

// -----------------------------------------------------------------------------
// ------------------------- VARIABLE DEFINITIONS ------------------------------
// -----------------------------------------------------------------------------

// Table to store processes. It uses the defined structure process control block.
PCB processTable[PROCESSTABLEMAXSIZE];

// Address base for OS code in this version.
int OS_address_base = PROCESSTABLEMAXSIZE * MAINMEMORYSECTIONSIZE;

// Identifier of the current executing process.
int executingProcessID = NOPROCESS;

// Identifier of the System Idle Process.
int sipID;

// Begin indes for daemons in programList.
int baseDaemonsInProgramList;

// Variable containing the number of not terminated user processes.
int numberOfNotTerminatedUserProcesses = 0;

// States names.
char * statesNames[5] = {"NEW","READY","EXECUTING","BLOCKED","EXIT"};

// Exception names.
char * exceptionNames [4] = {"division by zero","invalid processor mode","invalid address","invalid instruction"};

// Ready to tun processes queues.
int readyToRunQueue[NUMBEROFQUEUES][PROCESSTABLEMAXSIZE];
int numberOfReadyToRunProcesses[NUMBEROFQUEUES] = {0,0};
char * queueNames [NUMBEROFQUEUES] = {"USER","DAEMONS"};

// The number of clock interrupts occured.
int numberOfClockInterrupts = 0;

// In OperatingSystem.c Exercise 5-b of V2
// Heap with blocked processes sorted by when to wakeup
int sleepingProcessesQueue[PROCESSTABLEMAXSIZE];
int numberOfSleepingProcesses = 0;

// The size of the partitions table.
int partitionsTableSize = 0;

// IORB FIFO queue.
int IOWaitingProcessesQueue[PROCESSTABLEMAXSIZE];
int numberOfIOWaitingProcesses=0;

// -----------------------------------------------------------------------------
// ----------------------------- OS MANAGEMENT ---------------------------------
// -----------------------------------------------------------------------------

// Initial set of tasks of the OS
void OperatingSystem_Initialize(int daemonsIndex) {

	int i, selectedProcess, numberOfSuccesfullyCreatedProcesses;
	FILE *programFile; // For load Operating System Code

	// Obtain the memory requirements of the program
	int processSize=OperatingSystem_ObtainProgramSize(&programFile, "OperatingSystemCode");

	// Load Operating System Code
	OperatingSystem_LoadProgram(programFile, OS_address_base, processSize);

	// Process table initialization (all entries are free)
	for (i=0; i<PROCESSTABLEMAXSIZE;i++) {
		processTable[i].busy = 0;
	}

	// Initialization of the interrupt vector table of the processor
	Processor_InitializeInterruptVectorTable(OS_address_base+1);

	// Initialization of the partition table
	partitionsTableSize = OperatingSystem_InitializePartitionTable();

	// Create all system daemon processes
	OperatingSystem_PrepareDaemons(daemonsIndex);

	// Function responsible of adding user programs to the program arrival
	// queue (the heap), ordered by arrival time.
	ComputerSystem_FillInArrivalTimeQueue();

	// Printing the status of the OS after fill the arrival time queue.
	OperatingSystem_PrintStatus();

	// Create all user processes from the information given in the command line
	numberOfSuccesfullyCreatedProcesses = OperatingSystem_LongTermScheduler();

	if(numberOfSuccesfullyCreatedProcesses <= 1 && OperatingSystem_IsThereANewProgram() == -1) {
		// Incapable of create any process, simulation must finish;
    OperatingSystem_ReadyToShutdown();
  }

	if (strcmp(programList[processTable[sipID].programListIndex]->executableName,"SystemIdleProcess")) {
		// Show message "ERROR: Missing SIP program!\n"
		OperatingSystem_ShowTime(SHUTDOWN);
		ComputerSystem_DebugMessage(21,SHUTDOWN);
		exit(1);
	}

	// At least, one user process has been created
	// Select the first process that is going to use the processor
	selectedProcess=OperatingSystem_ShortTermScheduler();

	// Assign the processor to the selected process
	OperatingSystem_Dispatch(selectedProcess);

	// Initial operation for Operating System
	Processor_SetPC(OS_address_base);
} // END OperatingSystem_Initialize.

// Prints the rtrQ.
void OperatingSystem_PrintReadyToRunQueue() {

	// rTRQ contains 2,3,4,5,1 (PDIs)
	// not all valid. numberOfReadyToRunProcesses
	// valid indexes. Priotiries are in the processTable
	OperatingSystem_ShowTime(SHORTTERMSCHEDULE);
	ComputerSystem_DebugMessage(106, SHORTTERMSCHEDULE);
	int i, processPID, queue;

	// for each queue.
	for(queue=0; queue < NUMBEROFQUEUES; queue++) {

		// Printing USERPROCESSQUEUE processes
		ComputerSystem_DebugMessage(112, SHORTTERMSCHEDULE, queueNames[queue]);

		// If the queue to print is the first one, add a line break.
		if(numberOfReadyToRunProcesses[queue] == 0) {
			ComputerSystem_DebugMessage(113,SHORTTERMSCHEDULE); // Message "\n"
		}

		// Then for each process in the corresponding queue.
		for(i = 0; i < numberOfReadyToRunProcesses[queue]; i++) {
			// Getting the PID of the process in the rTRQ.
			processPID = readyToRunQueue[queue][i];

			// If the identifier is the last, add a new line, else a ",".
			if(i == numberOfReadyToRunProcesses[queue]-1) {
				// Debug message for the computed PID.
				ComputerSystem_DebugMessage(107,SHORTTERMSCHEDULE,processPID,processTable[processPID].priority,"\n");
			} else {
				ComputerSystem_DebugMessage(107,SHORTTERMSCHEDULE,processPID,processTable[processPID].priority,", ");
			} // END if.
		} // END for.
	} // END for.
} // END OperatingSystem_PrintReadyToRunQueue.

// Increases in one unit the number of clock interrupts and returns the new value
int OperatingSystem_IncreseNumberOfClockInterrupts() {

	// Increase and return by means of the ++variable operator.
	return ++numberOfClockInterrupts;
}

// Returns 1 if there OS should shut down. 0 otherwise.
int OperatingSystem_CheckIfShutdown() {
	int arrivalTimeQueue = OperatingSystem_IsThereANewProgram();
	int sleepingQueue = Heap_getFirst(sleepingProcessesQueue,numberOfSleepingProcesses);
	int rTRQ = OperatingSystem_GetMostImportantREADYProcessInfo();

	if(arrivalTimeQueue == -1 && sleepingQueue == -1 && rTRQ == -1 && numberOfIOWaitingProcesses == 0) {
		return 1;
	} else {
		return 0;
	}
}

// -----------------------------------------------------------------------------
// --------------------------- MEMORY MANAGEMENT -------------------------------
// -----------------------------------------------------------------------------

// Main memory is assigned in chunks. All chunks are the same size. A process
// always obtains the chunk whose position in memory is equal to the processor identifier
int OperatingSystem_ObtainMainMemory(int processSize, int PID) {
	// BEST FIT ALGORITHM
	int blockIndex;
	int bestBlockIndex = MEMORYFULL, bestBlockSize = INT_MAX;
	for(blockIndex=0; blockIndex < partitionsTableSize; blockIndex++) {

		// Check for too big processes
		if(processSize > OperatingSystem_GetBiggestMemoryBlockSize()) {
			return TOOBIGPROCESS;
		}

		if(!partitionsTable[blockIndex].occupied && partitionsTable[blockIndex].size >= processSize && partitionsTable[blockIndex].size < bestBlockSize) {
			bestBlockIndex = blockIndex;
			bestBlockSize = partitionsTable[blockIndex].size;

			// Small optimization, if it founds a block that has the same size as the process there wont be a better one so stops.
			if(processSize == partitionsTable[blockIndex].size) {
				break;
			}
		}
	}

	return bestBlockIndex;
} // END OperatingSystem_ObtainMainMemory.

// Gets the size of the biggest block in the partitios table.
int OperatingSystem_GetBiggestMemoryBlockSize() {
	int blockIndex;
	int biggestBlockSize=-1;
	for(blockIndex=0; blockIndex < partitionsTableSize; blockIndex++) {
		if(partitionsTable[blockIndex].size > biggestBlockSize) {
			biggestBlockSize = partitionsTable[blockIndex].size;
		}
	}
	return biggestBlockSize;
}

// Releases the main memory for the given process.
// returns the index of the partition released,
int OperatingSystem_ReleaseMainMemory(int PID) {

	int blockIndex;
	for(blockIndex=0; blockIndex < partitionsTableSize; blockIndex++) {

		// If the pointer block is assigned to the current process...
		if(partitionsTable[blockIndex].occupied && partitionsTable[blockIndex].PID == PID) {

			// Set the occupied to false and remove the PID data its not necessary.
			partitionsTable[blockIndex].occupied = 0;
			return blockIndex;
		}
	}
	return -1;
}

// -----------------------------------------------------------------------------
// ------------------------- PROCESSOR MANAGEMENT ------------------------------
// -----------------------------------------------------------------------------

// Returns the PID of the executing process
int OperatingSystem_GetExecutingProcessID() {
	return executingProcessID;
}

// Function that assigns the processor to a process
void OperatingSystem_Dispatch(int PID) {

	// The process identified by PID becomes the current executing process
	executingProcessID=PID;

	// Save the last state of the process we're dispatching.
	int lastState = processTable[PID].state;

	// Change the state of the process to EXECUTING.
	processTable[PID].state=EXECUTING;

	// Print messages.
	OperatingSystem_ShowTime(SYSPROC);
	ComputerSystem_DebugMessage(110,SYSPROC,PID,programList[processTable[PID].programListIndex]->executableName,statesNames[lastState],statesNames[EXECUTING]);

	// Modify hardware registers with appropriate values for the process identified by PID
	OperatingSystem_RestoreContext(PID);
} // END OperatingSystem_Dispatch.

// Function invoked when the executing process leaves the CPU
void OperatingSystem_PreemptRunningProcess() {

	// Save in the process' PCB essential values stored in hardware registers and the system stack
	OperatingSystem_SaveContext(executingProcessID);
	// Change the process' state
	OperatingSystem_MoveToTheREADYState(executingProcessID);
	// The processor is not assigned until the OS selects another process
	executingProcessID=NOPROCESS;
} // END OperatingSystem_PreemptRunningProcess.

// Save in the process' PCB essential values stored in hardware registers and the system stack
void OperatingSystem_SaveContext(int PID) {

	// Load PC saved for interrupt manager
	processTable[PID].copyOfPCRegister=Processor_CopyFromSystemStack(MAINMEMORYSIZE-1);

	// Load PSW saved for interrupt manager
	processTable[PID].copyOfPSWRegister=Processor_CopyFromSystemStack(MAINMEMORYSIZE-2);

	// Load the value of the accumulator on the PCB.
	processTable[PID].copyOfAccumulator=Processor_GetAccumulator();
} // OperatingSystem_SaveContext

// Modify hardware registers with appropriate values for the process identified by PID
void OperatingSystem_RestoreContext(int PID) {

	// New values for the CPU registers are obtained from the PCB
	Processor_CopyInSystemStack(MAINMEMORYSIZE-1,processTable[PID].copyOfPCRegister);
	Processor_CopyInSystemStack(MAINMEMORYSIZE-2,processTable[PID].copyOfPSWRegister);
	Processor_SetAccumulator(processTable[PID].copyOfAccumulator);

	// Same thing for the MMU registers
	MMU_SetBase(processTable[PID].initialPhysicalAddress);
	MMU_SetLimit(processTable[PID].processSize);
} // END OperatingSystem_RestoreContext.

// Updates the processor with the most important process in the rTRQ.
// Returns 0 if no change was made and 1 if any change done.
int OperatingSystem_UpdateExecutingProcessor() {

	// Save information about the executing process.
	int executingProcessQueue = processTable[executingProcessID].queueID,
			executingProcessPriority = processTable[executingProcessID].priority;

	// Try to obtain the most important one from the user process queue.
	int mostImportantREADYProcess = OperatingSystem_GetMostImportantREADYProcessInfo();

	if(processTable[mostImportantREADYProcess].queueID == executingProcessQueue) {
		// Check the priority.
		if(processTable[mostImportantREADYProcess].priority < executingProcessPriority) {
			// Change always, update the running process.
			return OperatingSystem_UpdateExecutingProcess();
		} else {
			// Do not change.
			return 0;
		} // END if-else.

	} else if(processTable[mostImportantREADYProcess].queueID < executingProcessQueue) {
		// Change always, update the running process.
		return OperatingSystem_UpdateExecutingProcess();
	} else {
		// Do not change.
		return 0;
	} // END if-else.
} // END OperatingSystem_UpdateExecutingProcessor.

// Updates the process that runs on the processor.
// Return 1 if everything ok.
int OperatingSystem_UpdateExecutingProcess() {
	int lastExecutingProcess = executingProcessID;
	int newExecutingProcess = OperatingSystem_ShortTermScheduler();

	OperatingSystem_ShowTime(SHORTTERMSCHEDULE);
	ComputerSystem_DebugMessage(121, SHORTTERMSCHEDULE,lastExecutingProcess,programList[processTable[lastExecutingProcess].programListIndex]->executableName,newExecutingProcess,programList[processTable[newExecutingProcess].programListIndex]->executableName);

	// Remove the executing process from the processor and load the new most important one.
	OperatingSystem_PreemptRunningProcess();
	OperatingSystem_Dispatch(newExecutingProcess);

	// Print required messages.
	OperatingSystem_PrintStatus();

	return 1;
}

// -----------------------------------------------------------------------------
// ---------------------------- I/O MANAGEMENT ---------------------------------
// -----------------------------------------------------------------------------

// Device independent Handler.
// This handler has to add a new IORB to the queue of the device and to notify the device dependent
// handler of this fact.
void OperatingSystem_IOScheduler() {

	// We add the IORB to the queue with the currst PID.
	if(QueueFIFO_add(executingProcessID, IOWaitingProcessesQueue, &numberOfIOWaitingProcesses, PROCESSTABLEMAXSIZE) == 0) {
		// Save in the process' PCB essential values stored in hardware registers and the system stack.
		OperatingSystem_SaveContext(executingProcessID);

		// Save the state of the process to block.
		int lastState = processTable[executingProcessID].state;

		// Change the state of the process to BLOCKED.
		processTable[executingProcessID].state=BLOCKED;

		// Print messages.
		OperatingSystem_ShowTime(SYSPROC);
		ComputerSystem_DebugMessage(110,SYSPROC,executingProcessID,programList[processTable[executingProcessID].programListIndex]->executableName,statesNames[lastState],statesNames[BLOCKED]);
	}

	// Devide dependent handler notification.
	OperatingSystem_DeviceControlerStartIOOperation();
}

// Devide dependent handler.
void OperatingSystem_DeviceControlerStartIOOperation() {
	int requestingProcessPID = QueueFIFO_getFirst(IOWaitingProcessesQueue, PROCESSTABLEMAXSIZE);
	if(Device_GetStatus() == FREE) {
		if(requestingProcessPID != NOPROCESS) {
			Device_StartIO(requestingProcessPID);
		}
	}
}

// Device dependent handler.
int OperatingSystem_DeviceControlerEndIOOperation() {
	int finishedProcessPID = QueueFIFO_poll(IOWaitingProcessesQueue, &numberOfIOWaitingProcesses);
	if(numberOfIOWaitingProcesses > 0) {
		OperatingSystem_DeviceControlerStartIOOperation();
	}
	return finishedProcessPID;
}

// -----------------------------------------------------------------------------
// -------------------------- PROCESS MANAGEMENT -------------------------------
// -----------------------------------------------------------------------------

// Daemon processes are system processes, that is, they work together with the OS.
// The System Idle Process uses the CPU whenever a user process is able to use it
void OperatingSystem_PrepareDaemons(int programListDaemonsBase) {

	// Include a entry for SystemIdleProcess at 0 position
	programList[0]=(PROGRAMS_DATA *) malloc(sizeof(PROGRAMS_DATA));

	programList[0]->executableName="SystemIdleProcess";
	programList[0]->arrivalTime=0;
	programList[0]->type=DAEMONPROGRAM; // daemon program

	sipID=INITIALPID%PROCESSTABLEMAXSIZE; // first PID for sipID

	// Prepare aditionals daemons here
	// index for aditionals daemons program in programList
	baseDaemonsInProgramList=programListDaemonsBase;
} // END OperatingSystem_PrepareDaemons.

// This function creates a process from an executable program
int OperatingSystem_CreateProcess(int indexOfExecutableProgram) {

	int PID;
	int processSize;
	int mainMemoryBlockIndex, initAddress;
	int priority;
	FILE *programFile;
	PROGRAMS_DATA *executableProgram=programList[indexOfExecutableProgram];

	// Obtain a process ID
	PID=OperatingSystem_ObtainAnEntryInTheProcessTable();

	// If NOFREEENTRY occurs return the NOFREEENTRY.
	if(PID==NOFREEENTRY) {
		return PID;
	}

	// Obtain the memory requirements of the program
	processSize=OperatingSystem_ObtainProgramSize(&programFile, executableProgram->executableName);

	// If the program is not valid
	if(processSize==PROGRAMNOTVALID) {
		return PROGRAMNOTVALID;
	}

	// If the program does not exists
	if(processSize==PROGRAMDOESNOTEXIST) {
		return PROGRAMDOESNOTEXIST;
	}

	// Obtain the priority for the process
	priority=OperatingSystem_ObtainPriority(programFile);

	// If the program is not valid
  if(priority==PROGRAMNOTVALID) {
  	return PROGRAMNOTVALID;
  }

	OperatingSystem_ShowTime(SYSMEM);
	// Process [@G%d - %s@@] requests [@G%d@@] memory positions\n
	ComputerSystem_DebugMessage(142,SYSMEM,PID,executableProgram->executableName,processSize);

	// Before OperatingSystem_ObtainMainMemory.
	OperatingSystem_ShowPartitionTable("before allocating memory");

	// Obtain enough memory space
 	mainMemoryBlockIndex=OperatingSystem_ObtainMainMemory(processSize, PID);

	// If the process is to big
	if(mainMemoryBlockIndex == TOOBIGPROCESS) {
		return TOOBIGPROCESS;

	// If the memory is full
	} else if (mainMemoryBlockIndex == MEMORYFULL) {
		OperatingSystem_ShowTime(ERROR);
		ComputerSystem_DebugMessage(144,ERROR,executableProgram->executableName);
		return MEMORYFULL;

	// Otherwise set the block as occupied and set the PID.
	} else {
		OperatingSystem_ShowTime(SYSMEM);
		ComputerSystem_DebugMessage(143,SYSMEM,mainMemoryBlockIndex,partitionsTable[mainMemoryBlockIndex].initAddress, partitionsTable[mainMemoryBlockIndex].size,PID,executableProgram->executableName);
		partitionsTable[mainMemoryBlockIndex].occupied = 1;
		partitionsTable[mainMemoryBlockIndex].PID = PID;
		initAddress = partitionsTable[mainMemoryBlockIndex].initAddress;
	}

	// Printing the new state sentence from ex 10
	OperatingSystem_ShowTime(SYSPROC);
	// New process [@G%d - %s@@] moving to the [@G%s@@] state\n
	ComputerSystem_DebugMessage(111,SYSPROC,PID,executableProgram->executableName,statesNames[NEW]);

	// Load program in the allocated memory
	// If the process is too big
	if( OperatingSystem_LoadProgram(programFile, initAddress, processSize) == TOOBIGPROCESS) {
		return TOOBIGPROCESS;
	}
	// PCB initialization
	OperatingSystem_PCBInitialization(PID, initAddress, processSize, priority, indexOfExecutableProgram);

	// After OperatingSystem_ObtainMainMemory.
	OperatingSystem_ShowPartitionTable("after allocating memory");

	// Show message "Process [PID] created from program [executableName]\n"
	OperatingSystem_ShowTime(INIT);
	ComputerSystem_DebugMessage(22,INIT,PID,executableProgram->executableName);

	return PID;
} // END OperatingSystem_CreateProcess.

// Assign initial values to all fields inside the PCB
void OperatingSystem_PCBInitialization(int PID, int initialPhysicalAddress, int processSize, int priority, int processPLIndex) {

	processTable[PID].busy=1;
	processTable[PID].initialPhysicalAddress=initialPhysicalAddress;
	processTable[PID].processSize=processSize;
	processTable[PID].state=NEW;
	processTable[PID].priority=priority;
	processTable[PID].programListIndex=processPLIndex;

	// Daemons run in protected mode and MMU use real address
	if (programList[processPLIndex]->type == DAEMONPROGRAM) {
		processTable[PID].copyOfPCRegister=initialPhysicalAddress;
		processTable[PID].copyOfPSWRegister= ((unsigned int) 1) << EXECUTION_MODE_BIT;

		// Exercise 11
		processTable[PID].queueID = DAEMONSQUEUE;
	} else {
		processTable[PID].copyOfPCRegister=0;
		processTable[PID].copyOfPSWRegister=0;

		// Exercise 11
		processTable[PID].queueID = USERPROCESSQUEUE;
	}
} // END OperatingSystem_PCBInitialization.

// Gets the most important process in the rTRQ information without removing it from the rTRQ.
// Return the most important process in the rTRQ PID.
int OperatingSystem_GetMostImportantREADYProcessInfo() {
	// Try to obtain the most important one from the user process queue.
	int mostImportantREADYProcess = Heap_getFirst(readyToRunQueue[USERPROCESSQUEUE],numberOfReadyToRunProcesses[USERPROCESSQUEUE]);

	// If there is no user process has been waken up must be a daemon.
	if(mostImportantREADYProcess == -1) {
		mostImportantREADYProcess = Heap_getFirst(readyToRunQueue[DAEMONSQUEUE],numberOfReadyToRunProcesses[DAEMONSQUEUE]);
	}

	return mostImportantREADYProcess;
}

// Computes the value of when a process must wake up taking in to account the
// given formula.
// returns an integer with the corresponding value.
int OperatingSystem_GetWhenToWakeUpTime() {

	// Computed as the absolute value of the accumulator + the current number of clock interrupts + 1.
	return abs(Processor_GetAccumulator()) + numberOfClockInterrupts + 1;
}

// Return PID of more priority process in the READY queue
int OperatingSystem_ExtractFromReadyToRun() {

	// Initially we set the selected process as NOPROCESS.
	int selectedProcess=NOPROCESS;

	// First select from the user queue
	selectedProcess=Heap_poll(readyToRunQueue[USERPROCESSQUEUE],QUEUE_PRIORITY,&numberOfReadyToRunProcesses[USERPROCESSQUEUE]);

	// Try to get a process from the daemons queue if no process found on the user process queue.
	if(selectedProcess == NOPROCESS) {
		selectedProcess=Heap_poll(readyToRunQueue[DAEMONSQUEUE],QUEUE_PRIORITY,&numberOfReadyToRunProcesses[DAEMONSQUEUE]);
	}

	// Return most priority process or NOPROCESS if empty queues.
	return selectedProcess;
} // END OperatingSystem_ExtractFromReadyToRun.

// The LTS is responsible of the admission of new processes in the system.
// Initially, it creates a process from each program specified in the
// command lineand daemons programs
int OperatingSystem_LongTermScheduler() {

	int PID, i,
		numberOfSuccessfullyCreatedProcesses=0;

	while(OperatingSystem_IsThereANewProgram() == 1) {

		// Extract the first element from the arrivalTimeQueue.
		i = Heap_poll(arrivalTimeQueue, QUEUE_ARRIVAL, &numberOfProgramsInArrivalTimeQueue);

		// Obtain the PID
		PID=OperatingSystem_CreateProcess(i);

		// For each progrem check possible errors.
		switch (PID) {

			case NOFREEENTRY:
				OperatingSystem_ShowTime(ERROR);
				ComputerSystem_DebugMessage(103,ERROR,programList[i]->executableName);
				break;

			case PROGRAMDOESNOTEXIST:
				OperatingSystem_ShowTime(ERROR);
				ComputerSystem_DebugMessage(104,ERROR,programList[i]->executableName,"it does not exists");
				break;

			case PROGRAMNOTVALID:
				OperatingSystem_ShowTime(ERROR);
    				ComputerSystem_DebugMessage(104,ERROR,programList[i]->executableName,"invalid priority or size");
				break;

			case TOOBIGPROCESS:
				OperatingSystem_ShowTime(ERROR);
				ComputerSystem_DebugMessage(105,ERROR,programList[i]->executableName);
				break;

			default:
				numberOfSuccessfullyCreatedProcesses++;

				// If it is a user program then we need to increae the number of not terminated user programs.
				if (programList[i]->type==USERPROGRAM) {
					numberOfNotTerminatedUserProcesses++;
				}

				// Move process to the ready state
				OperatingSystem_MoveToTheREADYState(PID);
		} // END switch
	} // END for

	// If there are any succesfully created processes then print the status.
	if(numberOfSuccessfullyCreatedProcesses) {
		OperatingSystem_PrintStatus();
	}

	// Return the number of succesfully created processes
	return numberOfSuccessfullyCreatedProcesses;
} // END OperatingSystem_LongTermScheduler.

// The STS is responsible of deciding which process to execute when specific events occur.
// It uses processes priorities to make the decission. Given that the READY queue is ordered
// depending on processes priority, the STS just selects the process in front of the READY queue
int OperatingSystem_ShortTermScheduler() {
	int selectedProcess;
	selectedProcess=OperatingSystem_ExtractFromReadyToRun();
	return selectedProcess;
}

// Move a process to the READY state: it will be inserted, depending on its priority, in
// a queue of identifiers of READY processes
void OperatingSystem_MoveToTheREADYState(int PID) {

	int queue = processTable[PID].queueID; // Type of the program [USER, DAEMON].

	if (Heap_add(PID,readyToRunQueue[queue],QUEUE_PRIORITY,&numberOfReadyToRunProcesses[queue],PROCESSTABLEMAXSIZE)==0) {
		int lastState = processTable[PID].state;
		processTable[PID].state=READY;
		OperatingSystem_ShowTime(SYSPROC);
		ComputerSystem_DebugMessage(110,SYSPROC,PID,programList[processTable[PID].programListIndex]->executableName,statesNames[lastState],statesNames[READY]);
	} // END if
} // END OperatingSystem_MoveToTheREADYState

// Move a process to the BLOCKED state: it will be inserted, depending on its whenToWakeUp, in
// a queue of identifiers of BLOCKED processes
void OperatingSystem_MoveToTheBLOCKEDState(int PID) {

	if (Heap_add(PID, sleepingProcessesQueue, QUEUE_WAKEUP, &numberOfSleepingProcesses, PROCESSTABLEMAXSIZE) == 0) {

		// Save in the process' PCB essential values stored in hardware registers and the system stack.
		OperatingSystem_SaveContext(executingProcessID);

		// Save the state of the process to block.
		int lastState = processTable[PID].state;

		// Change the state of the process to BLOCKED.
		processTable[PID].state=BLOCKED;

		// Compute the value of whenToWakeUp
		processTable[PID].whenToWakeUp = OperatingSystem_GetWhenToWakeUpTime();

		// Print messages.
		OperatingSystem_ShowTime(SYSPROC);
		ComputerSystem_DebugMessage(110,SYSPROC,PID,programList[processTable[PID].programListIndex]->executableName,statesNames[lastState],statesNames[BLOCKED]);
	} // END if
} // END OperatingSystem_MoveToTheBLOCKEDState

// All tasks regarding the removal of the process
void OperatingSystem_TerminateProcess() {

	// Save the alst state of the process to terminate.
	int lastState = processTable[executingProcessID].state;

	// Change the state of the program to EXIT.
	processTable[executingProcessID].state=EXIT;

	// Print messages.
	OperatingSystem_ShowTime(SYSPROC);
	ComputerSystem_DebugMessage(110,SYSPROC,executingProcessID,programList[processTable[executingProcessID].programListIndex]->executableName,statesNames[lastState],statesNames[EXIT]);

	// Before OperatingSystem_ReleaseMainMemory.
	OperatingSystem_ShowPartitionTable("before releasing memory");

	// Release memory associated to the process.
	int releasedBlockIndex = OperatingSystem_ReleaseMainMemory(executingProcessID);

	// Printing messages of releasing memory.
	OperatingSystem_ShowTime(SYSMEM);
	ComputerSystem_DebugMessage(145, SYSMEM, releasedBlockIndex,
															partitionsTable[releasedBlockIndex].initAddress,
	 														partitionsTable[releasedBlockIndex].size,
															executingProcessID,
															programList[processTable[executingProcessID].programListIndex]->executableName);

	// Before OperatingSystem_ReleaseMainMemory.
	OperatingSystem_ShowPartitionTable("after releasing memory");

	// If the program to terminate was a user program reduce the number of not terminated user programs.
	if (programList[processTable[executingProcessID].programListIndex]->type==USERPROGRAM) {
		// One more user process that has terminated
		numberOfNotTerminatedUserProcesses--;
	}

	// If the conditions to shut down are meet then...
	if (OperatingSystem_CheckIfShutdown()) {
		// Simulation must finish
		OperatingSystem_ReadyToShutdown();
	}

	// Select the next process to execute (sipID if no more user processes)
	// Assign the processor to that process
	OperatingSystem_Dispatch(OperatingSystem_ShortTermScheduler());
} // END OperatingSystem_TerminateProcess.

// -----------------------------------------------------------------------------
// -------------------- EXCEPTION/INTERRUPT MANAGEMENT -------------------------
// -----------------------------------------------------------------------------

//	Implement interrupt logic calling appropriate interrupt handle
void OperatingSystem_InterruptLogic(int entryPoint) {
	switch (entryPoint) {
		case SYSCALL_BIT: // SYSCALL_BIT=2
			OperatingSystem_HandleSystemCall();
			break;
		case EXCEPTION_BIT: // EXCEPTION_BIT=6
			OperatingSystem_HandleException();
			break;
		case CLOCKINT_BIT: // EXCEPTION_BIT=9
			OperatingSystem_HandleClockInterrupt();
			break;
		case IOEND_BIT: // EXCEPTION_BIT=8
			OperatingSystem_HandleIOEndInterrupt();
			break;
	}
} // END  OperatingSystem_InterruptLogic.

// System call management routine
void OperatingSystem_HandleSystemCall() {

	// Specific local variables definition.
	int systemCallID;
	int currentQueue, currentPriority;
	int oldProcessID, newProcessID;

	// Register A contains the identifier of the issued system call.
	systemCallID=Processor_GetRegisterA();

	// Identification of the system call.
	switch (systemCallID) {

		case SYSCALL_PRINTEXECPID:
			// Show message: "Process [executingProcessID] has the processor assigned\n"
			OperatingSystem_ShowTime(SYSPROC);
			ComputerSystem_DebugMessage(24,SYSPROC,executingProcessID,programList[processTable[executingProcessID].programListIndex]->executableName);
			break;

		case SYSCALL_END:
			// Show message: "Process [executingProcessID] has requested to terminate\n"
			OperatingSystem_ShowTime(SYSPROC);
			ComputerSystem_DebugMessage(25,SYSPROC,executingProcessID,programList[processTable[executingProcessID].programListIndex]->executableName);
			OperatingSystem_TerminateProcess();
			OperatingSystem_PrintStatus();
			break;

		case  SYSCALL_YIELD:
			// If a process with the same priority and placed at the front of the same priority queue appears relinquish the CPU to that process.

			// 1. Get the queue and priority of the executing process.
			currentQueue = processTable[executingProcessID].queueID;
			currentPriority = processTable[executingProcessID].priority;

			// 2. Check that the rTRQ contains at least one rTR process and that it has the same priority as the executing one.
			if(numberOfReadyToRunProcesses[currentQueue] > 0 && processTable[readyToRunQueue[currentQueue][0]].priority == currentPriority) {
				oldProcessID = executingProcessID; // Store old process PID.
				newProcessID = readyToRunQueue[currentQueue][0]; // Store new process PID.

				// Finally print the corresponding debug message.
				OperatingSystem_ShowTime(SHORTTERMSCHEDULE);
				ComputerSystem_DebugMessage(115,SHORTTERMSCHEDULE,oldProcessID,
					programList[processTable[oldProcessID].programListIndex]->executableName,
					newProcessID,programList[processTable[newProcessID].programListIndex]->executableName);

				// Stop current process.
				OperatingSystem_PreemptRunningProcess();

				// Start new process. It is the first opne dispached by the ShortTermScheduler.
				OperatingSystem_Dispatch(OperatingSystem_ShortTermScheduler());

				OperatingSystem_PrintStatus();
			}

			break;

		case SYSCALL_SLEEP:
			oldProcessID = executingProcessID;
			// Move the executing proces to the BLOCKED state.
			OperatingSystem_MoveToTheBLOCKEDState(executingProcessID);

			// Start new process. It is the first one dispached by the ShortTermScheduler.
			OperatingSystem_Dispatch(OperatingSystem_ShortTermScheduler());

			// Finally print the system status.
			OperatingSystem_PrintStatus();
			break;

		case SYSCALL_IO:
			oldProcessID = executingProcessID;
			// Call to the device independent handler.
			OperatingSystem_IOScheduler();

			// Start new process. It is the first one dispached by the ShortTermScheduler.
			OperatingSystem_Dispatch(OperatingSystem_ShortTermScheduler());

			// Finally print the system status.
			OperatingSystem_PrintStatus();
			break;

		default: // That means the system call id is not valid
			OperatingSystem_ShowTime(INTERRUPT);
			ComputerSystem_DebugMessage(141, INTERRUPT, executingProcessID,programList[processTable[executingProcessID].programListIndex]->executableName, systemCallID);
			OperatingSystem_TerminateProcess();
			OperatingSystem_PrintStatus();
			break;
	} // END switch.
} // END OperatingSystem_HandleSystemCall.

// Exception management routine
void OperatingSystem_HandleException() {

	// Exercise v4-2
	OperatingSystem_ShowTime(INTERRUPT);
	ComputerSystem_DebugMessage(140, INTERRUPT, executingProcessID,programList[processTable[executingProcessID].programListIndex]->executableName, exceptionNames[Processor_GetRegisterB()]);

	// Terminate the process and print the system status.
	OperatingSystem_TerminateProcess();
	OperatingSystem_PrintStatus();
} // END OperatingSystem_SaveContext.

// Function to be executed each time a clock interrupt is raised.
// Time between interrupts defined at Clock.h INTERVALBETWEENINTERRUPS. Initial value 5.
void OperatingSystem_HandleClockInterrupt() {

	// Increase the number of clock interrupts.
	int currentNumberOfClockInterrupts = OperatingSystem_IncreseNumberOfClockInterrupts();

	// Finally print the the corresponding debug message.
	OperatingSystem_ShowTime(INTERRUPT);
	ComputerSystem_DebugMessage(120, INTERRUPT, currentNumberOfClockInterrupts);

	// Check the sleepingProcessesQueue.
	int unBLOCKEDProcesses = 0;

	// While there are more sleeping processes that need to be waken up...
	// A process wakes up when the current clock interrupts is greater or equal to its whenToWakeUpTime.
	while(Heap_getFirst(sleepingProcessesQueue,numberOfSleepingProcesses) != NOPROCESS && processTable[Heap_getFirst(sleepingProcessesQueue,numberOfSleepingProcesses)].whenToWakeUp <= currentNumberOfClockInterrupts) {
		// Move to the READY state that process that meets the condition.
		OperatingSystem_MoveToTheREADYState(Heap_poll(sleepingProcessesQueue,QUEUE_WAKEUP,&numberOfSleepingProcesses));
		// increase the number of unblockedProcesses.
		unBLOCKEDProcesses++;
	} // END while.

	// If we unblocked any process we must check if some of them have more priority than the executing one
	if(unBLOCKEDProcesses || OperatingSystem_LongTermScheduler()) {
		OperatingSystem_UpdateExecutingProcessor();
	} else if (OperatingSystem_CheckIfShutdown()) {
		// Simulation must finish
		OperatingSystem_ReadyToShutdown();
	}
} // END OperatingSystem_HandleClockInterrupt.

// Function to handle an IOENDInterrupt.
void OperatingSystem_HandleIOEndInterrupt() {

	// Obtain the finished process and process the next IO operation.
	int finishedProcessPID = OperatingSystem_DeviceControlerEndIOOperation();

	// Unblock the process whose IO operation has finished.
	if(finishedProcessPID != NOPROCESS) {
		// Move to the ready state.
		OperatingSystem_MoveToTheREADYState(finishedProcessPID);
		OperatingSystem_PrintStatus();

		// As we unblock a process we must check if the executing process must be updated.
		OperatingSystem_UpdateExecutingProcessor();
	}
}
