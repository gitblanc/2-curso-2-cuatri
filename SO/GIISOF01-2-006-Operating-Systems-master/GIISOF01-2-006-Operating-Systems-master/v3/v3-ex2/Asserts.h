#ifndef CHECKASSERTS_H
#define CHECKASSERTS_H

// #define MAX_ASSERTS 500
#define MAXIMUMLENGTH 64
#define ASSERTS_CONF_FILE "asserts.conf"
#define E_SIZE 8 

enum assertList {
 RMEM_OP=0	// Relative MEMory OPeration code
,RMEM_O1=1	// Relative MEMory Operand 1
,RMEM_O2=2	// Relative MEMory Operand 2
,AMEM_OP=3	// Absolute MEMory OPeration code
,AMEM_O1=4	// Absolute MEMory Operand 1
,AMEM_O2=5	// Absolute MEMory Operand 2
,PC=6		// Program Counter
,ACC=7		// ACCumulator
,IR_OP=8	// Instruction Register OPeration code
,IR_O1=9	// Instruction Register Operator 1
,IR_O2=10	// Instruction Register OPerator 2
,PSW=11		// Processor State Word
,MAR=12		// Memory Address Register
,MBR_OP=13	// Memory Buffer Register OPeration code
,MBR_O1=14	// Memory Buffer Register Operand 1
,MBR_O2=15	// Memory Buffer Register Operand 2
,MMU_BS=16	// Memory Management Unit BaSe
,MMU_LM=17	// Memory Management Unit LiMit
,MMU_MAR=18	// Memory Management Unit Memory Address Register
,MMEM_MAR=19	// Main MEMory Memory Address Register
,MMBR_OP=20		// Main MEMory Memory Memory Buffer Register OPeration code
,MMBR_O1=21		// Main MEMory Memory Memory Buffers Register Operator 1
,MMBR_O2=22		// Main MEMory Memory Memory Buffer Register Operator 2
,XPID=23		// eXecuting PID
,RMEM=24		// Relative MEMory
,AMEM=25		// Absolute MEMory
};

typedef struct {
	int time;
	int value;
	char element[E_SIZE]; // Se almacenará lo arriba indicado o MEM si es un elemento de memoria (o RMEM y AMEM en su caso)
	int address;
} ASSERT_DATA;

// Functions prototypes
 int Asserts_LoadAsserts();
 void Asserts_CheckAsserts();
 void Asserts_TerminateAssertions();

extern ASSERT_DATA * asserts;

#endif