#include <string.h>
#include <stdarg.h>
#include <stdlib.h>
#include <ctype.h>
#include "ComputerSystem.h"
#include "ComputerSystemBase.h"
#include "Processor.h"
#include "Heap.h"
#include "OperatingSystemBase.h"
#include "Messages.h"

// Functions prototypes

int GEN_ASSERTS=0;
int LOAD_ASSERTS_CONF=0;

// String specified in the command line to tell the simulator which of its
// sections are interesting for the user so it must show debug messages
// related to them
char *debugLevel;

int endSimulationTime=-1; // For end simulation forced by time

// Only one colour messages. Set to 1 for more colours checking uppercase in debugLevel
int COLOURED = 0 ;

#ifdef ARRIVALQUEUE
	extern char * queueNames []; 
#endif

  
// Fill in the array named userProgramsList with the information given
// by the user in the command line
// IT IS NOT NECESSARY TO COMPLETELY UNDERSTAND THIS FUNCTION
int ComputerSystem_ObtainProgramList(int argc, char *argv[]) {
	
	int i;
	int paramIndex=1; // argv index
	int count=1;  // 0 reserved for sipid
	PROGRAMS_DATA *progData;

	// Initialization of the programList
	for (i=0; i<PROGRAMSMAXNUMBER;i++)
	      programList[i]=NULL;

	if (sscanf(argv[paramIndex], "%d", &endSimulationTime) == 1)
		// An end simulation time has been read. Increment paramIndex
		paramIndex++;
		
	// To remember the simulator sections to be message-debugged
	debugLevel = argv[paramIndex++];
	for (i=0; i< strlen(debugLevel);i++)
	  if (isupper(debugLevel[i])){
		COLOURED = 1;
		debugLevel[i]=tolower(debugLevel[i]);
	  }

	if (strchr(debugLevel,'g')) // Generate asserts in standard output
		GEN_ASSERTS=1; 

	if (strchr(debugLevel,'z')) // Load assert information from configuration file "asserts.conf"
		LOAD_ASSERTS_CONF=1;
	
	// Store the names of the programs
	for (i = paramIndex; i < argc && count<PROGRAMSMAXNUMBER;) { // check number of programs < PROGRAMSMAXNUMBER
		progData=(PROGRAMS_DATA *) malloc(sizeof(PROGRAMS_DATA));
		// Save file name
		progData->executableName = (char *) malloc((strlen(argv[i])+1)*sizeof(char));
		strcpy(progData->executableName,argv[i]);
		i++;
		// Default arrival time: 0  
		progData->arrivalTime = 0;
		// Try to store the arrival time if exists
		if ((i < argc)
			 && (sscanf(argv[i], "%d", &(progData->arrivalTime)) == 1))
				// An arrival time has been read. Increment i
				i++;

		// Defaul user programs
		progData->type=USERPROGRAM;

		// Store the structure in the list
		programList[count]=progData;
		
 		count++; // There is one program more
	}
	return count; // Next place for new programs
}

// Function used to show messages with details of the internal working of
// the simulator
// IT IS NOT NECESSARY TO UNDERSTAND ALL THE DETAILS OF THIS FUNCTION
void ComputerSystem_DebugMessage(int msgNo, char section, ...) {

	va_list lp;
	char c, *format;
	int count, youHaveToContinue, colour=0;
 
	int pos;
	
        pos=Messages_Get_Pos(msgNo);
        if (pos==-1) {
         printf("Debug Message %d not defined\n",msgNo);
         return;
        }
        format=DebugMessages[pos].format;
        	
	va_start(lp, section);
	
	if ((strchr(debugLevel, ALL) != NULL) 		// Print the message because the user specified ALL
	  || section == ERROR  						//  Always print ERROR section
	  || (strchr(debugLevel,section)) != NULL){ //  or the section argument is included in the debugLevel string
		
		for (count = 0, youHaveToContinue = 1; youHaveToContinue == 1; count++) {
			//printf("(%c)",format[count]);
			switch (format[count]) {
				case '\\':count++;
				 	 switch (format[count]) {
						case 'n': printf("\n");
							  break;
						case 't': printf("\t");	
							  break;
						default: printf("\%c", format[count]);
					} // switch Control Chars
					break;
				// case '%':			
				case '@':	// Next color char		
		 			count++;
					switch (format[count]) {
			 			case 'R': // Text in red
							if (COLOURED){
							  printf("%c[%d;%dm", 0x1B, 1, 31);
							  if (!colour) colour=1;
							}
						break;
						case 'G': // Text in green
							if (COLOURED){
						  	printf("%c[%d;%dm", 0x1B, 1, 32);
						  	if (!colour) colour=1;
							}
							break;
						case 'Y': // Text in yellow
		  					if (COLOURED){
							  printf("%c[%d;%dm", 0x1B, 1, 33);
							  if (!colour) colour=1;
							}
							break;
						case 'B': // Text in blue
							if (COLOURED){
							  printf("%c[%d;%dm", 0x1B, 1, 34);
						  	if (!colour) colour=1;
							}
							break;
						case 'M': // // Text in magenta
							if (COLOURED){
						  	printf("%c[%d;%dm", 0x1B, 1, 35);
						  	if (!colour) colour=1;
							}
							break;
						case 'C': // Text in cyan
							if (COLOURED){
							  printf("%c[%d;%dm", 0x1B, 1, 36);
							  if (!colour) colour=1;
							}
							break;		
						case 'W': // Text in white
							if (COLOURED){
							  printf("%c[%d;%dm", 0x1B, 1, 37);
							  if (!colour) colour=1;
							}
							break;
						case '@': // Text without color
							if (COLOURED && colour)
							    printf("%c[%dm", 0x1B, 0);
							break;	
					}	// switch colors chars
					break;
				case '%':			
		 			count++;
					switch (format[count]) {
						case 's':
							printf("%s",va_arg(lp, char *));
							break;
						case 'd':
							printf("%d",va_arg(lp, int));
							break;
						case 'f':
							printf("%f",va_arg(lp, double));
							break;
						case 'c':
							c = (char) va_arg(lp, int);
							printf("%c", c);
							break;
						case 'x':
							printf("%04X", va_arg(lp, int));
							break;
						default:
							youHaveToContinue = 0;
						} // switch format chars
					break;
				default:if (format[count]==0)
						youHaveToContinue=0;
					else
						printf("%c",format[count]);				
			} // switch
		} // for
		va_end(lp);
	} // if
	if (COLOURED && colour)
	    printf("%c[%dm", 0x1B, 0);
} // ComputerSystem_DebugMessage()

// Fill ArrivalTimeQueue heap with user program from parameters and daemons 
void ComputerSystem_FillInArrivalTimeQueue() {
#ifdef ARRIVALQUEUE

  int arrivalIndex = 0; 

	while (programList[arrivalIndex]!=NULL && arrivalIndex<PROGRAMSMAXNUMBER) {
	  Heap_add(arrivalIndex,arrivalTimeQueue,QUEUE_ARRIVAL,&arrivalIndex,PROGRAMSMAXNUMBER);
	}
	numberOfProgramsInArrivalTimeQueue=arrivalIndex;
#endif
}

// Print arrivalTiemQueue program information
void ComputerSystem_PrintArrivalTimeQueue(){
#ifdef ARRIVALQUEUE
  int i;
  
  if (numberOfProgramsInArrivalTimeQueue>0) {
	OperatingSystem_ShowTime(LONGTERMSCHEDULE);
	// Show message "Arrival Time Queue: "
	ComputerSystem_DebugMessage(31,LONGTERMSCHEDULE);
	for (i=0; i< numberOfProgramsInArrivalTimeQueue; i++) {
	  // Show message [executableName,arrivalTime]
	  ComputerSystem_DebugMessage(32,LONGTERMSCHEDULE,programList[arrivalTimeQueue[i]]->executableName,
			programList[arrivalTimeQueue[i]]->arrivalTime,
			queueNames[programList[arrivalTimeQueue[i]]->type]);
	}
  }
 #endif
}
  
