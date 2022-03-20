#include <stdio.h>
#include <stdlib.h>
#include "Simulator.h"
#include "ComputerSystem.h"


int main(int argc, char *argv[]) {
  
	// We now have a multiprogrammed computer system with a possible arrival time for each program
	// No more than PROGRAMSMAXNUMBER in the command line
	if ((argc < 3) || (argc>(PROGRAMSMAXNUMBER*2+3))) {
		printf("USE: Simulator <sections to be debugged> <program1> [<arrival1>] [<program2> [<arrival2>] ....] \n");
		exit(-1);
	}

	// The simulation starts
	ComputerSystem_PowerOn(argc, argv);
	// The simulation ends
	ComputerSystem_PowerOff();
	return 0;
}
