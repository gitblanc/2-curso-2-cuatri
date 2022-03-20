## Exercise 4-a
**OperatingSystem.c**
```C
int OperatingSystem_CreateProcess(int indexOfExecutableProgram) {
  ...
  // Obtain a process ID
  PID=OperatingSystem_ObtainAnEntryInTheProcessTable();

  // If NOFREEENTRY occurs return the NOFREEENTRY.
  if(PID==NOFREEENTRY){
    return PID;
  }
  ...
```

## Exercise 4-b
**OperatingSystem.c**
```C
switch (PID) {
  case NOFREEENTRY:
    ComputerSystem_DebugMessage(103,ERROR,programList[i]->executableName);
    break;
...
  default:
    numberOfSuccessfullyCreatedProcesses++;
}
```

**messagesSTD.txt**
```
103,@RERROR: There are not free entries in the process table for the program[%s]@@\n
```

## Exercise 5
**OperatingSystem.c**
```C
// LongTermScheduler
switch (PID) {
  case NOFREEENTRY:
    ComputerSystem_DebugMessage(103,ERROR,programList[i]->executableName);
    break;
  case PROGRAMDOESNOTEXIST:
    ComputerSystem_DebugMessage(104,ERROR,programList[i]->executableName,"it does not exists");
    break;
  case PROGRAMNOTVALID:
    ComputerSystem_DebugMessage(104,ERROR,programList[i]->executableName,"invalid priority or size");
    break;
  default:
    numberOfSuccessfullyCreatedProcesses++;
}

...
// CreateProcess function  
// If the program is not valid
if(processSize==PROGRAMNOTVALID){
  return processSize;
}
  
// If the program does not exists
if(processSize==PROGRAMDOESNOTEXIST){
  return processSize;
}
```

**messagesSTD.txt**
```
104, @RERROR: 104,@RERROR: Program [%s] is not valid [--- %s ---]@@\n
```
