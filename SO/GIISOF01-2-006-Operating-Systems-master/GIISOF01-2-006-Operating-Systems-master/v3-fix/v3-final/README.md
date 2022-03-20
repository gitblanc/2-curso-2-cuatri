## Exercise 8
**OperatingSystem.h**
```C
INITIALPID = 3;
  ...
```

## Exercise 9-a
**OperatingSystem.c**
```C
// Exercise 9 function, print the ready-to-run queue
void OperatingSystem_PrintReadyToRunQueue(){
  // rTRQ contains 2,3,4,5,1 (PDIs)
  // not all valid. numberOfReadyToRunProcesses
  // valid indexes. Priotiries are in the processTable
  ComputerSystem_DebugMessage(106, SHORTTERMSCHEDULE);
  int i, processPID;
  for(i=0; i<numberOfReadyToRunProcesses; i++) {
    // Getting the PID of the process in the rTRQ.
    processPID = readyToRunQueue[i];

    if(i == numberOfReadyToRunProcesses-1) {
      // Debug message for the computed PID.
      ComputerSystem_DebugMessage(107,SHORTTERMSCHEDULE,processPID,processTable[processPID].priority,"\n");
    } else {
      ComputerSystem_DebugMessage(107,SHORTTERMSCHEDULE,processPID,processTable[processPID].priority,", ");
    }
  }
}
```

**messagesSTD.txt**
```
106,Ready-to-run processes queue:\n\t
107,[@G%d@@,%d]%s
```

## Exercise 9-b
**OperatingSystem.c**
```C
// Move a process to the READY state: it will be inserted, depending on its priority, in
// a queue of identifiers of READY processes
void OperatingSystem_MoveToTheREADYState(int PID) {

  if (Heap_add(PID, readyToRunQueue,QUEUE_PRIORITY ,&numberOfReadyToRunProcesses ,PROCESSTABLEMAXSIZE)>=0) {
    processTable[PID].state=READY;
    OperatingSystem_PrintReadyToRunQueue();
    }
}

```
