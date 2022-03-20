## Exercise 0
**Processor.c**
```C
void Processor_DecodeAndExecuteInstruction() {
...
  // Instruction  MEMADD operand1 memAdress
  case 'm':
    registerMAR_CPU = operand2;
    // Send to the main memory controller the address in which the reading has to take place: use the address bus for this
    Buses_write_AddressBus_From_To(CPU,MMU);
    // Tell the main memory controller to read
    MMU_readMemory();
    registerAccumulator_CPU = operand1 + registerMBR_CPU.cell;
    Processor_CheckOverflow(operand1, registerMBR_CPU.cell);
    registerPC_CPU++;
    break;
}
```

## Exercise 1
**ComputerSystem.c**
```C
void ComputerSystem_PrintProgramList();
...

void ComputerSystem_PrintProgramList() {
  ComputerSystem_DebugMessage(101, INIT);
  int i;
  for (i=1; programList[i]!=NULL && i<PROGRAMSMAXNUMBER ; i++) {
    ComputerSystem_DebugMessage(102,INIT,programList[i]->executableName,programList[i]->arrivalTime);
  }
}
```

**messagesSTD.txt**
```
101, User program list:\n
102, \t\t Program [@C%s@@] with arrival time [@C%d@@]\n
```

## Exercise 2
**ComputerSystem.c**
```C
void ComputerSystem_PowerOn(int argc, char *argv[]) {
  ...
  int daemonsBaseIndex = ComputerSystem_ObtainProgramList(argc, argv);
  void ComputerSystem_PrintProgramList(); // Added call to the Ex1 impl. func.
  ...
}
```
