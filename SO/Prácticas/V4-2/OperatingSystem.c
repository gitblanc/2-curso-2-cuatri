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

// Functions prototypes
void OperatingSystem_PCBInitialization(int, int, int, int, int,int);
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
int OperatingSystem_ExtractFromReadyToRun(int); // EJ 11 V1
void OperatingSystem_HandleException();
void OperatingSystem_HandleSystemCall();
void OperatingSystem_PrintReadyToRunQueue(); // EJ 9 V1
void OperatingSystem_HandleClockInterrupt();
char *statesNames[5] = {"NEW", "READY", "EXECUTING", "BLOCKED", "EXIT"}; // EJ 10 V1
void OperatingSystem_MoveToTheBLOCKEDState(int);						 // EJ 5d V2
int OperatingSystem_ExtractFromBLOCKEDToRun();							 // EJ 6 V2
void checkPriorityOfExecutingProcess();									 // EJ 6 V2
void sustituyeProceso(int);												 // EJ 6 V2
void OperatingSystem_ReleaseMainMemory();//Ej 8 V4

// EJ 11 V1
//  In OperatingSystem.c
heapItem readyToRunQueue[NUMBEROFQUEUES][PROCESSTABLEMAXSIZE];
int numberOfReadyToRunProcesses[NUMBEROFQUEUES] = {0, 0};
char *queueNames[NUMBEROFQUEUES] = {"USER", "DAEMONS"};

// EJ 11 V1

// EJ 4 V2
int numberOfClockInterrupts;
int numberOfPartitions; // Ej 5 V4
// The process table
PCB processTable[PROCESSTABLEMAXSIZE];

// Address base for OS code in this version
int OS_address_base = PROCESSTABLEMAXSIZE * MAINMEMORYSECTIONSIZE;

// Identifier of the current executing processF
int executingProcessID = NOPROCESS;

// Identifier of the System Idle Process
int sipID;

// Initial PID for assignation
// int initialPID = 0;
// EJ8 V1
int initialPID = PROCESSTABLEMAXSIZE - 1;

// Begin indes for daemons in programList
int baseDaemonsInProgramList;

// Array that contains the identifiers of the READY processes
// heapItem readyToRunQueue[PROCESSTABLEMAXSIZE];
// int numberOfReadyToRunProcesses = 0;

// Variable containing the number of not terminated user processes
int numberOfNotTerminatedUserProcesses = 0;

char DAEMONS_PROGRAMS_FILE[MAXIMUMLENGTH] = "teachersDaemons";

// In OperatingSystem.c Exercise 5b of V2
// Heap with blocked processes sort by when to wakeup
heapItem sleepingProcessesQueue[PROCESSTABLEMAXSIZE];
int numberOfSleepingProcesses = 0;

// Initial set of tasks of the OS
// EJ 15 V1 begin
void OperatingSystem_Initialize(int daemonsIndex)
{

	int i, selectedProcess, processCreated;
	FILE *programFile; // For load Operating System Code
	programFile = fopen("OperatingSystemCode", "r");
	if (programFile == NULL)
	{
		// Show red message "FATAL ERROR: Missing Operating System!\n"
		ComputerSystem_DebugMessage(99, SHUTDOWN, "FATAL ERROR: Missing Operating System!\n");
		exit(1);
	}

	// Obtain the memory requirements of the program
	int processSize = OperatingSystem_ObtainProgramSize(programFile);

	// Load Operating System Code
	OperatingSystem_LoadProgram(programFile, OS_address_base, processSize);

	// Process table initialization (all entries are free)
	for (i = 0; i < PROCESSTABLEMAXSIZE; i++)
	{
		processTable[i].busy = 0;
	}
	// Initialization of the interrupt vector table of the processor
	Processor_InitializeInterruptVectorTable(OS_address_base + 2);

	// Include in program list  all system daemon processes
	OperatingSystem_PrepareDaemons(daemonsIndex);

	// EJ 1 c) V3
	ComputerSystem_FillInArrivalTimeQueue();
	// Ej 5 V4
	numberOfPartitions = OperatingSystem_InitializePartitionTable();
	// // Ej 1 d) V3
	OperatingSystem_PrintStatus();
	// Ej 15 begin
	//  Create all user processes from the information given in the command line, PLP
	processCreated = OperatingSystem_LongTermScheduler();

	if (processCreated == 0 && OperatingSystem_IsThereANewProgram() == NOPROCESS) // eJ 5 v3
	{
		OperatingSystem_ReadyToShutdown();
	}
	// EJ 15 end

	if (strcmp(programList[processTable[sipID].programListIndex]->executableName, "SystemIdleProcess"))
	{
		// Show red message "FATAL ERROR: Missing SIP program!\n"
		ComputerSystem_DebugMessage(99, SHUTDOWN, "FATAL ERROR: Missing SIP program!\n");
		exit(1);
	}

	// At least, one user process has been created
	// Select the first process that is going to use the processor
	selectedProcess = OperatingSystem_ShortTermScheduler();

	// Assign the processor to the selected process
	OperatingSystem_Dispatch(selectedProcess);

	// Initial operation for Operating System
	Processor_SetPC(OS_address_base);
}
// EJ 15 V1 end

// The LTS is responsible of the admission of new processes in the system.
// Initially, it creates a process from each program specified in the
// 			command line and daemons programs
int OperatingSystem_LongTermScheduler()
{

	int PID, i,
		numberOfSuccessfullyCreatedProcesses = 0;
	// for (i = 0; programList[i] != NULL && i < PROGRAMSMAXNUMBER; i++)
	i = 0;
	while (OperatingSystem_IsThereANewProgram() == YES)
	{
		i = Heap_poll(arrivalTimeQueue, QUEUE_ARRIVAL, &numberOfProgramsInArrivalTimeQueue);
		PID = OperatingSystem_CreateProcess(i);
		switch (PID)
		{
		// EJ 4 V1
		case NOFREEENTRY:
			// EJ 1 V2
			OperatingSystem_ShowTime(ERROR);
			ComputerSystem_DebugMessage(103, ERROR, programList[i]->executableName);
			break;

		// EJ5 V1
		case PROGRAMDOESNOTEXIST:
			// EJ 1 V2
			OperatingSystem_ShowTime(ERROR);
			ComputerSystem_DebugMessage(104, ERROR, programList[i]->executableName, "-- it does not exist --");
			break;
		case PROGRAMNOTVALID:
			// EJ 1 V2
			OperatingSystem_ShowTime(ERROR);
			ComputerSystem_DebugMessage(104, ERROR, programList[i]->executableName, "invalid priority or size");
			break;
		// EJ 6 V1
		case TOOBIGPROCESS:
			// EJ 1 V2
			OperatingSystem_ShowTime(ERROR);
			ComputerSystem_DebugMessage(105, ERROR, programList[i]->executableName);
			break;
		// EJ 6 V1
		case MEMORYFULL:
			// EJ 6 V4
			OperatingSystem_ShowTime(ERROR);
			ComputerSystem_DebugMessage(144, ERROR, programList[processTable[executingProcessID].programListIndex]->executableName);
			break;
		default:
			numberOfSuccessfullyCreatedProcesses++;
			if (programList[i]->type == USERPROGRAM)
				numberOfNotTerminatedUserProcesses++;
			// Move process to the ready state
			OperatingSystem_MoveToTheREADYState(PID);
		}
	}
	if (numberOfSuccessfullyCreatedProcesses != 0)
	{
		OperatingSystem_PrintStatus(); // EJ 7 V2
	}
	// Return the number of succesfully created processes
	return numberOfSuccessfullyCreatedProcesses;
}

// This function creates a process from an executable program
int OperatingSystem_CreateProcess(int indexOfExecutableProgram)
{

	int PID;
	int processSize;
	int loadingPhysicalAddress;
	int numberOfPartition;
	int priority;
	FILE *programFile;
	PROGRAMS_DATA *executableProgram = programList[indexOfExecutableProgram];

	// Obtain a process ID
	PID = OperatingSystem_ObtainAnEntryInTheProcessTable();
	// EJ 4 V1-x1
	if (PID < 0)
	{
		return NOFREEENTRY;
	}
	// Check if programFile exists

	// EJ 5 V1
	programFile = fopen(executableProgram->executableName, "r");
	if (programFile == NULL)
	{
		return PROGRAMDOESNOTEXIST;
	}

	// Obtain the memory requirements of the program
	// EJ 5 V1
	processSize = OperatingSystem_ObtainProgramSize(programFile);
	if (processSize == 0)
	{
		return PROGRAMNOTVALID;
	}

	// Obtain the priority for the process
	priority = OperatingSystem_ObtainPriority(programFile);
	if (PID == PROGRAMNOTVALID)
	{
		return PROGRAMNOTVALID;
	}
	// Obtain enough memory space
	// EJ 6 V4
	numberOfPartition = OperatingSystem_ObtainMainMemory(processSize, PID);
	OperatingSystem_ShowTime(SYSMEM);
	ComputerSystem_DebugMessage(142, SYSMEM, PID, executableProgram->executableName, processSize);
	if (numberOfPartition == TOOBIGPROCESS || numberOfPartition == MEMORYFULL)
	{
		return numberOfPartition;
	}
	// Ej 6 V4
	loadingPhysicalAddress = partitionsTable[numberOfPartition].initAddress;

	// Load program in the allocated memory
	if (OperatingSystem_LoadProgram(programFile, loadingPhysicalAddress, processSize) == TOOBIGPROCESS)
	{
		return TOOBIGPROCESS;
	}

	OperatingSystem_ShowPartitionTable("before allocating memory"); // Ej 7 V4

	OperatingSystem_ShowTime(SYSMEM);
	ComputerSystem_DebugMessage(143, SYSMEM, numberOfPartition, loadingPhysicalAddress, partitionsTable[numberOfPartition].size, PID, executableProgram->executableName);
	partitionsTable[numberOfPartition].PID = PID;					// Ej 7 v4
	// PCB initialization
	OperatingSystem_PCBInitialization(PID, loadingPhysicalAddress, processSize, priority, indexOfExecutableProgram, numberOfPartition);
	// EJ10 V1
	ComputerSystem_DebugMessage(111, SYSPROC, PID, programList[processTable[PID].programListIndex]->executableName, statesNames[NEW]);
	OperatingSystem_ShowPartitionTable("after allocating memory"); // Ej 7 V4

	OperatingSystem_ShowTime(INIT);
	// Show message "Process [PID] created from program [executableName]\n"
	ComputerSystem_DebugMessage(70, INIT, PID, executableProgram->executableName);

	return PID;
}

// Main memory is assigned in chunks. All chunks are the same size. A process
// always obtains the chunk whose position in memory is equal to the processor identifier
int OperatingSystem_ObtainMainMemory(int processSize, int PID)
{
	// if (processSize > MAINMEMORYSECTIONSIZE)
	// 	return TOOBIGPROCESS;

	// return PID * MAINMEMORYSECTIONSIZE;
	// Ej 6 V4
	int i;
	int bestPartition = MAINMEMORYSIZE;
	int numberPart = -1;
	int bestTam;

	for (i = 0; i < numberOfPartitions; i++)
	{
		if (partitionsTable[i].size > bestTam)
		{
			bestTam = partitionsTable[i].size;
		}
	}
	if(processSize > bestTam){
		return TOOBIGPROCESS;
	}
	for (i = 0; i < numberOfPartitions; i++)
	{
		if (partitionsTable[i].size >= processSize && partitionsTable[i].PID == NOPROCESS)
		{
			if (partitionsTable[i].size < bestPartition)
			{
				bestPartition = partitionsTable[i].size;
				numberPart = i;
			}
		}
	}
	if (processSize > bestPartition)
	{
		return TOOBIGPROCESS;
	}
	if (numberPart == -1)
	{
		return MEMORYFULL;
	}
	return numberPart;
}

// Assign initial values to all fields inside the PCB
void OperatingSystem_PCBInitialization(int PID, int initialPhysicalAddress, int processSize, int priority, int processPLIndex, int particion)
{

	processTable[PID].busy = 1;
	processTable[PID].initialPhysicalAddress = initialPhysicalAddress;
	processTable[PID].processSize = processSize;
	processTable[PID].copyOfAcumulatorRegister = 0;
	processTable[PID].partition = particion;
	// EJ 1 V2
	OperatingSystem_ShowTime(SYSPROC);

	processTable[PID].state = NEW;
	processTable[PID].priority = priority;
	processTable[PID].programListIndex = processPLIndex;
	// Daemons run in protected mode and MMU use real address
	if (programList[processPLIndex]->type == DAEMONPROGRAM)
	{
		processTable[PID].copyOfPCRegister = initialPhysicalAddress;
		processTable[PID].copyOfPSWRegister = ((unsigned int)1) << EXECUTION_MODE_BIT;
	}
	else
	{
		processTable[PID].copyOfPCRegister = 0;
		processTable[PID].copyOfPSWRegister = 0;
	}
	// EJ 11 V1
	processTable[PID].queueId = programList[processPLIndex]->type;
}

// Move a process to the READY state: it will be inserted, depending on its priority, in
// a queue of identifiers of READY processes
void OperatingSystem_MoveToTheREADYState(int PID)
{

	if (Heap_add(PID, readyToRunQueue[processTable[PID].queueId], QUEUE_PRIORITY, &(numberOfReadyToRunProcesses[processTable[PID].queueId]), PROCESSTABLEMAXSIZE) >= 0)
	{
		// EJ 1 V2
		OperatingSystem_ShowTime(SYSPROC);
		ComputerSystem_DebugMessage(110, SYSPROC, PID, programList[processTable[PID].programListIndex]->executableName, statesNames[processTable[PID].state], statesNames[READY]);
		processTable[PID].state = READY;
	}
	// OperatingSystem_PrintReadyToRunQueue(); EJ 8 V2
}

// The STS is responsible of deciding which process to execute when specific events occur.
// It uses processes priorities to make the decission. Given that the READY queue is ordered
// depending on processes priority, the STS just selects the process in front of the READY queue
int OperatingSystem_ShortTermScheduler()
{
	int selectedProcess = NOPROCESS; // EJ 11 BUG
	int i;
	// EJ11 V1 begin
	for (i = 0; i < NUMBEROFQUEUES && selectedProcess == NOPROCESS; i++)
	{
		selectedProcess = OperatingSystem_ExtractFromReadyToRun(i);
	}
	// ej11 end

	return selectedProcess;
}

// EJ11 V1
//  Return PID of more priority process in the READY queue
int OperatingSystem_ExtractFromReadyToRun(int queueId)
{

	int selectedProcess = NOPROCESS;

	selectedProcess = Heap_poll(readyToRunQueue[queueId], QUEUE_PRIORITY, &(numberOfReadyToRunProcesses[queueId]));

	// Return most priority process or NOPROCESS if empty queue
	return selectedProcess;
}

// Function that assigns the processor to a process
void OperatingSystem_Dispatch(int PID)
{

	// The process identified by PID becomes the current executing process
	executingProcessID = PID;
	// EJ 1 V2
	OperatingSystem_ShowTime(SYSPROC);
	// EJ10 V1
	ComputerSystem_DebugMessage(110, SYSPROC, PID, programList[processTable[PID].programListIndex]->executableName, statesNames[processTable[PID].state], statesNames[EXECUTING]);
	// Change the process' state
	processTable[PID].state = EXECUTING;
	// Modify hardware registers with appropriate values for the process identified by PID
	OperatingSystem_RestoreContext(PID);
}

// Modify hardware registers with appropriate values for the process identified by PID
void OperatingSystem_RestoreContext(int PID)
{

	// New values for the CPU registers are obtained from the PCB
	Processor_CopyInSystemStack(MAINMEMORYSIZE - 1, processTable[PID].copyOfPCRegister);
	Processor_CopyInSystemStack(MAINMEMORYSIZE - 2, processTable[PID].copyOfPSWRegister);
	Processor_CopyInSystemStack(MAINMEMORYSIZE - 3, processTable[PID].copyOfAcumulatorRegister);

	// Same thing for the MMU registers
	MMU_SetBase(processTable[PID].initialPhysicalAddress);
	MMU_SetLimit(processTable[PID].processSize);
}

// Function invoked when the executing process leaves the CPU
void OperatingSystem_PreemptRunningProcess()
{

	// Save in the process' PCB essential values stored in hardware registers and the system stack
	OperatingSystem_SaveContext(executingProcessID);
	// Change the process' state
	OperatingSystem_MoveToTheREADYState(executingProcessID);
	// The processor is not assigned until the OS selects another process
	executingProcessID = NOPROCESS;
}

// Save in the process' PCB essential values stored in hardware registers and the system stack
void OperatingSystem_SaveContext(int PID)
{

	// Load PC saved for interrupt manager
	processTable[PID].copyOfPCRegister = Processor_CopyFromSystemStack(MAINMEMORYSIZE - 1);

	// Load PSW saved for interrupt manager
	processTable[PID].copyOfPSWRegister = Processor_CopyFromSystemStack(MAINMEMORYSIZE - 2);

	// Ej 12 V1
	processTable[PID].copyOfAcumulatorRegister = Processor_CopyFromSystemStack(MAINMEMORYSIZE - 3); // salvar el acumulador
}

// Exception management routine
void OperatingSystem_HandleException()
{

	// Show message "Process [executingProcessID] has generated an exception and is terminating\n"
	OperatingSystem_ShowTime(SYSPROC);
	// ComputerSystem_DebugMessage(71, SYSPROC, executingProcessID, programList[processTable[executingProcessID].programListIndex]->executableName);
	int registerB = Processor_GetRegisterB();
	char *tipoexcepcion;
	// EJ 2 V4
	switch (registerB)
	{
	case DIVISIONBYZERO:
		tipoexcepcion = "division by zero";
		break;
	case INVALIDPROCESSORMODE:
		tipoexcepcion = "invalid processor mode";
		break;
	case INVALIDADDRESS:
		tipoexcepcion = "invalid address";
		break;
	default:
		tipoexcepcion = "invalid instruction";
		break;
	}
	ComputerSystem_DebugMessage(140, INTERRUPT, executingProcessID, programList[processTable[executingProcessID].programListIndex]->executableName, tipoexcepcion); // Ej 2 V4
	OperatingSystem_TerminateProcess();
	OperatingSystem_PrintStatus(); // EJ 7 V2
}

// All tasks regarding the removal of the process
// regarding -> con respecto a la eliminacion del proceso
// EJ 14 V1
void OperatingSystem_TerminateProcess()
{

	int selectedProcess;
	// EJ 1 V2
	OperatingSystem_ShowTime(SYSPROC);
	ComputerSystem_DebugMessage(110, SYSPROC, executingProcessID, programList[processTable[executingProcessID].programListIndex]->executableName, statesNames[processTable[executingProcessID].state], statesNames[EXIT]);
	processTable[executingProcessID].state = EXIT; // se imprime un mensaje
	
	OperatingSystem_ShowPartitionTable("before releasing memory"); // Ej 7 V4
	OperatingSystem_ReleaseMainMemory();//Ej 8 V4
	if (programList[processTable[executingProcessID].programListIndex]->type == USERPROGRAM)
		// One more user process that has terminated
		numberOfNotTerminatedUserProcesses--; // decrementa el número de procesos de usuario por terminar

	if (numberOfNotTerminatedUserProcesses == 0) // Si no quedan procesos de usuario
	{
		if (OperatingSystem_IsThereANewProgram() == NOPROCESS) // ej 5 C) V3
		{
			if (executingProcessID == sipID) // si el id del proceso es el del Idle process
			{
				// finishing sipID, change PC to address of OS HALT instruction
				OperatingSystem_ShowTime(SHUTDOWN);
				OperatingSystem_TerminatingSIP();												 // se llama a la instruccion halt
				ComputerSystem_DebugMessage(99, SHUTDOWN, "The system will shut down now...\n"); // se apaga el sistema
				return;																			 // Don't dispatch any process
			}
			// Simulation must finish, telling sipID to finish
			OperatingSystem_ReadyToShutdown();
		}
	}
	OperatingSystem_ShowPartitionTable("after releasing memory"); // Ej 7 V4
	// Select the next process to execute (sipID if no more user processes)
	selectedProcess = OperatingSystem_ShortTermScheduler();

	// Assign the processor to that process
	OperatingSystem_Dispatch(selectedProcess);
}
// EJ 14 end

// System call management routine
void OperatingSystem_HandleSystemCall()
{

	int systemCallID;
	int actualQueue, actualPriority;
	int newProcessId, oldProcessId;

	// Register A contains the identifier of the issued system call
	systemCallID = Processor_GetRegisterA();

	switch (systemCallID)
	{
	case SYSCALL_PRINTEXECPID:
		// Show message: "Process [executingProcessID] has the processor assigned\n"
		OperatingSystem_ShowTime(SYSPROC);
		ComputerSystem_DebugMessage(72, SYSPROC, executingProcessID, programList[processTable[executingProcessID].programListIndex]->executableName);
		break;

	case SYSCALL_END:
		// Show message: "Process [executingProcessID] has requested to terminate\n"
		OperatingSystem_ShowTime(SYSPROC);
		ComputerSystem_DebugMessage(73, SYSPROC, executingProcessID, programList[processTable[executingProcessID].programListIndex]->executableName);
		OperatingSystem_TerminateProcess();
		OperatingSystem_PrintStatus(); // EJ 7 V2
		break;
	// EJ 12 V1 begin
	case SYSCALL_YIELD:
		// El anterior proceso le pasa el control al nuevo proceso
		actualQueue = processTable[executingProcessID].queueId;
		actualPriority = processTable[executingProcessID].priority;
		if (numberOfReadyToRunProcesses[actualQueue] > 0 && processTable[readyToRunQueue[actualQueue][0].info].priority == actualPriority)
		{
			newProcessId = readyToRunQueue[actualQueue][0].info;			// se asigna el nuevo
			oldProcessId = executingProcessID;								// se guarda el antiguo
			OperatingSystem_PreemptRunningProcess();						// paramos el proceso actual
			OperatingSystem_Dispatch(OperatingSystem_ShortTermScheduler()); // se ejecuta el nuevo proceso pasandole ShortTerm...
																			// EJ 1 V2
			OperatingSystem_ShowTime(SHORTTERMSCHEDULE);
			ComputerSystem_DebugMessage(115, SHORTTERMSCHEDULE, oldProcessId,
										programList[processTable[oldProcessId].programListIndex]->executableName,
										newProcessId, programList[processTable[newProcessId].programListIndex]->executableName);
			OperatingSystem_PrintStatus(); // EJ 7 V2
		}
		break;
		// EJ 12 V1 end
	// Ej 5d V2 begin
	case SYSCALL_SLEEP:
		OperatingSystem_MoveToTheBLOCKEDState(executingProcessID);
		OperatingSystem_Dispatch(OperatingSystem_ShortTermScheduler());
		OperatingSystem_PrintStatus();
		break;

	default: // Ej 4 V4
		OperatingSystem_ShowTime(INTERRUPT);
		ComputerSystem_DebugMessage(141, INTERRUPT, executingProcessID, programList[processTable[executingProcessID].programListIndex]->executableName, systemCallID);
		OperatingSystem_TerminateProcess();
		OperatingSystem_PrintStatus();
		break;
	}
}

// EJ 5 d V2
void OperatingSystem_MoveToTheBLOCKEDState(int PID)
{
	OperatingSystem_SaveContext(PID);
	processTable[PID].whenToWakeUp = abs(Processor_GetAccumulator()) + abs(numberOfClockInterrupts) + 1; // GUARDAR EL CONTEXTO
	if (Heap_add(PID, sleepingProcessesQueue, QUEUE_WAKEUP, &numberOfSleepingProcesses, PROCESSTABLEMAXSIZE) >= 0)
	{
		OperatingSystem_ShowTime(SYSPROC);
		ComputerSystem_DebugMessage(110, SYSPROC, PID, programList[processTable[PID].programListIndex]->executableName, statesNames[processTable[PID].state], statesNames[BLOCKED]);
		processTable[PID].state = BLOCKED;
	}
	// Ej 5d V2 end
}

//	Implement interrupt logic calling appropriate interrupt handle
void OperatingSystem_InterruptLogic(int entryPoint)
{
	switch (entryPoint)
	{
	case SYSCALL_BIT: // SYSCALL_BIT=2
		OperatingSystem_HandleSystemCall();
		break;
	case EXCEPTION_BIT: // EXCEPTION_BIT=6
		OperatingSystem_HandleException();
		break;
	// EJ 2 V2
	case CLOCKINT_BIT:
		OperatingSystem_HandleClockInterrupt();
		break;
	}
}

// //EJ9 V1 begin
// void OperatingSystem_PrintReadyToRunQueue()
// {
// 	int i = 0;
// 	ComputerSystem_DebugMessage(106, SHORTTERMSCHEDULE);
// 	for (i; i < numberOfReadyToRunProcesses - 1; i++)
// 	{
// 		//readytorunqueue[i] -> es el pid del proceso
// 		ComputerSystem_DebugMessage(107, SHORTTERMSCHEDULE, readyToRunQueue[i].info, processTable[readyToRunQueue[i].info].priority);
// 	}
// 	ComputerSystem_DebugMessage(108, SHORTTERMSCHEDULE, readyToRunQueue[i].info, processTable[readyToRunQueue[i].info].priority);
// }
// //EJ9 V1 end

// EJ 11 V1
void OperatingSystem_PrintReadyToRunQueue()
{
	int i;
	// EJ 1 V2
	OperatingSystem_ShowTime(SYSPROC);
	ComputerSystem_DebugMessage(106, SHORTTERMSCHEDULE);
	// EJ 1 V2
	ComputerSystem_DebugMessage(112, SHORTTERMSCHEDULE);
	for (i = 0; i < numberOfReadyToRunProcesses[USERPROCESSQUEUE] - 1; i++)
	{
		// EJ 1 V2
		// readytorunqueue[i] -> es el pid del proceso

		ComputerSystem_DebugMessage(114, SHORTTERMSCHEDULE, readyToRunQueue[USERPROCESSQUEUE][i].info, processTable[readyToRunQueue[USERPROCESSQUEUE][i].info].priority);
	}
	if (numberOfReadyToRunProcesses[0] != 0)
	{
		ComputerSystem_DebugMessage(108, SHORTTERMSCHEDULE, readyToRunQueue[USERPROCESSQUEUE][i].info, processTable[readyToRunQueue[USERPROCESSQUEUE][i].info].priority);
	}
	else
	{
		ComputerSystem_DebugMessage(114, SHORTTERMSCHEDULE);
	}
	// EJ 1 V2
	ComputerSystem_DebugMessage(113, SHORTTERMSCHEDULE);
	for (i = 0; i < numberOfReadyToRunProcesses[DAEMONSQUEUE] - 1; i++)
	{
		// EJ 1 V2
		// readytorunqueue[i] -> es el pid del proceso
		ComputerSystem_DebugMessage(107, SHORTTERMSCHEDULE, readyToRunQueue[DAEMONSQUEUE][i].info, processTable[readyToRunQueue[DAEMONSQUEUE][i].info].priority);
	}
	if (numberOfReadyToRunProcesses[1] != 0)
	{
		ComputerSystem_DebugMessage(108, SHORTTERMSCHEDULE, readyToRunQueue[DAEMONSQUEUE][i].info, processTable[readyToRunQueue[DAEMONSQUEUE][i].info].priority);
	}
	else
	{
		ComputerSystem_DebugMessage(114, SHORTTERMSCHEDULE);
	}
}
// EJ 11 V1

// // In OperatingSystem.c Exercise 2-b of V2
// void OperatingSystem_HandleClockInterrupt()
// {
// 	numberOfClockInterrupts += 1;
// 	OperatingSystem_ShowTime(INTERRUPT);
// 	ComputerSystem_DebugMessage(120, INTERRUPT, numberOfClockInterrupts);
// 	return;
// }

// EJ 6 a) V2
int OperatingSystem_ExtractFromBLOCKEDToRun()
{

	int selectedProcess = NOPROCESS;

	selectedProcess = Heap_poll(sleepingProcessesQueue, QUEUE_PRIORITY, &numberOfSleepingProcesses);

	// Return most priority process or NOPROCESS if empty queue
	return selectedProcess;
}

// EJ 6 begin V2
void OperatingSystem_HandleClockInterrupt()
{
	numberOfClockInterrupts += 1;
	OperatingSystem_ShowTime(INTERRUPT);
	ComputerSystem_DebugMessage(120, INTERRUPT, numberOfClockInterrupts);
	int desbloqueados = 0;
	int process = NOPROCESS;
	int prioritariodaemons = 0;
	// Mientras haya procesos que necesiten ser desbloqueados/despertados y
	// el whentoWakeUp sea igual al número de interrupciones
	while (numberOfSleepingProcesses > 0 && processTable[Heap_getFirst(sleepingProcessesQueue, numberOfSleepingProcesses)].whenToWakeUp == numberOfClockInterrupts)
	{
		process = OperatingSystem_ExtractFromBLOCKEDToRun();
		desbloqueados++;							  // se aumentan los desbloqueados
		OperatingSystem_MoveToTheREADYState(process); // se mueve a READY
	}
	// Ej 4 a) V3
	OperatingSystem_LongTermScheduler();
	// Ej 5 V3
	if (numberOfNotTerminatedUserProcesses == 0)
	{
		if (OperatingSystem_IsThereANewProgram() == NOPROCESS)
		{
			OperatingSystem_ReadyToShutdown();
		}
	}

	// if (desbloqueados >= 1) // EJ 6 b) V2
	//{

	// EJ 6 c) V2
	checkPriorityOfExecutingProcess();
	//}
	if (numberOfReadyToRunProcesses[DAEMONSQUEUE] > 0)
	{																// SI NO SE HAN DESBLOQUEADO PROCESOS Y HAY DAEMONS
		prioritariodaemons = readyToRunQueue[DAEMONSQUEUE][0].info; // devuelve el más prioritario DAEMONS
		if (processTable[executingProcessID].queueId == DAEMONSQUEUE && (processTable[executingProcessID].priority > processTable[prioritariodaemons].priority))
		{
			sustituyeProceso(prioritariodaemons);
		}
	}
	return;
}
// EJ 6 end

void checkPriorityOfExecutingProcess()
{
	int prioritariouser = readyToRunQueue[USERPROCESSQUEUE][0].info; // devuelve el más prioritario USERS

	if (numberOfReadyToRunProcesses[USERPROCESSQUEUE] > 0 && processTable[executingProcessID].queueId == USERPROCESSQUEUE) // el proceso en ejecución es de usuario y hay procesos de usuario
	{
		if (processTable[executingProcessID].priority > processTable[prioritariouser].priority) // dos user
		{
			OperatingSystem_ShowTime(SHORTTERMSCHEDULE);
			ComputerSystem_DebugMessage(121, SHORTTERMSCHEDULE, executingProcessID, programList[processTable[executingProcessID].programListIndex]->executableName, prioritariouser, programList[processTable[prioritariouser].programListIndex]->executableName);
			sustituyeProceso(prioritariouser);
		}
	}
	else if (numberOfReadyToRunProcesses[USERPROCESSQUEUE] > 0) // el proceso en ejecución es de daemons y hay procesos daemons
	{
		sustituyeProceso(prioritariouser);
	}
}

void sustituyeProceso(int pidProceso)
{
	OperatingSystem_PreemptRunningProcess(); // sustituir el proceso
	OperatingSystem_Dispatch(OperatingSystem_ShortTermScheduler());
	OperatingSystem_PrintStatus();
}

// Ej 2 V3
int OperatingSystem_GetExecutingProcessID()
{
	return executingProcessID;
}

void OperatingSystem_ReleaseMainMemory(){//Ej 8 V4
	OperatingSystem_ShowTime(SYSMEM);
 	ComputerSystem_DebugMessage(145, SYSMEM, processTable[executingProcessID].partition, processTable[executingProcessID].initialPhysicalAddress, partitionsTable[processTable[executingProcessID].partition].size, executingProcessID, programList[processTable[executingProcessID].programListIndex]->executableName);
	 partitionsTable[processTable[executingProcessID].partition].PID = NOPROCESS;
}