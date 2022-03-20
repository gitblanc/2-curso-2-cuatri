## Exercise 1
**messages.txt**
```
3,(PC: @R%d@@, Accumulator: @R%d@@, PSW: @R%x@@)\n
```

**Processor.c**
```C
// Powers off the CS (the C program ends)
void Processor_DecodeAndExecuteInstruction() {
  ...
  ComputerSystem_DebugMessage(3,HARDWARE,registerPC_CPU,
      registerAccumulator_CPU,registerPSW_CPU); // Exercise 1
}
```


## Exercise 2
**messages.txt**
```
2, @REND of the simulation@@\n 
```

**ComputerSystem.c**
```C
// Powers off the CS (the C program ends)
void ComputerSystem_PowerOff() {
  ComputerSystem_DebugMessage(2, HARDWARE); // Exercise 2
  exit(0);
}
```
