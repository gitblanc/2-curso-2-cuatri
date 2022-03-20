#ifndef COMPUTERSYSTEMBASE_H
#define COMPUTERSYSTEMBASE_H

#include "ComputerSystem.h"

// Functions prototypes
int ComputerSystem_ObtainProgramList(int , char *[]);
void ComputerSystem_DebugMessage(int, char , ...);
void ComputerSystem_FillInArrivalTimeQueue();
void ComputerSystem_PrintArrivalTimeQueue();


// This "extern" declarations enables other source code files to gain access to the variables 
extern int endSimulationTime; // For end simulation forced by time
extern int numberOfProgramsInArrivalTimeQueue;
extern int arrivalTimeQueue[PROGRAMSMAXNUMBER];

#endif
