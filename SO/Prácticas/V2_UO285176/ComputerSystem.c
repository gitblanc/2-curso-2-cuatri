#include <stdio.h>
#include <stdlib.h>
#include "ComputerSystem.h"
#include "OperatingSystem.h"
#include "ComputerSystemBase.h"
#include "Processor.h"
#include "Messages.h"
#include "Asserts.h"
#include "Wrappers.h"

// Functions prototypes
void ComputerSystem_PrintProgramList(int);

// Powers on of the Computer System.
void ComputerSystem_PowerOn(int argc, char *argv[], int paramIndex) {
	
	// Obtain a list of programs in the command line
	int daemonsBaseIndex = ComputerSystem_ObtainProgramList(argc, argv, paramIndex);
	// Load debug messages
	int nm=0;
	nm=Messages_Load_Messages(nm,TEACHER_MESSAGES_FILE);
	if (nm<0) {
		//Ex1-V2-Inicio
		ComputerSystem_ShowTime(SHUTDOWN);
		//Ex1-V2-Final
		ComputerSystem_DebugMessage(64,SHUTDOWN,TEACHER_MESSAGES_FILE);
		exit(2);
	}
	nm=Messages_Load_Messages(nm,STUDENT_MESSAGES_FILE);

	// Prepare if necesary the assert system
	Asserts_LoadAsserts();
	//v1-ex2-inicial
	ComputerSystem_PrintProgramList(daemonsBaseIndex);
	//v1-ex2_final
	// Request the OS to do the initial set of tasks. The last one will be
	// the processor allocation to the process with the highest priority
	OperatingSystem_Initialize(daemonsBaseIndex);
	
	// Tell the processor to begin its instruction cycle 
	Processor_InstructionCycleLoop();
	
}

// Powers off the CS (the C program ends)
void ComputerSystem_PowerOff() {
	// Show message in red colour: "END of the simulation\n" 
	//Ex1-V2-Inicio
	ComputerSystem_ShowTime(SHUTDOWN);
	//Ex1-V2-Final
	ComputerSystem_DebugMessage(99,SHUTDOWN,"END of the simulation\n"); 
	exit(0);
}

/////////////////////////////////////////////////////////
//  New functions below this line  //////////////////////
//v1-ex1-inicial
void ComputerSystem_PrintProgramList(int daemonsBaseIndexAux){
	char *name;
	int arrivalTime;

	//Ex1-V2-Inicio
	ComputerSystem_ShowTime(INIT);
	//Ex1-V2-Final
	ComputerSystem_DebugMessage(101,INIT);
	for (int i = 1; i < daemonsBaseIndexAux; i++){
		if (programList[i] != NULL){
			name = programList[i]->executableName;
			arrivalTime = programList[i]->arrivalTime;
			ComputerSystem_DebugMessage(102,INIT,name,arrivalTime);
		}
	}
}
//v1-ex1-final