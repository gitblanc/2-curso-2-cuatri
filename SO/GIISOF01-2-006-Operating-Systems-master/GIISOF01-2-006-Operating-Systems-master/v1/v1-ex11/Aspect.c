
struct JoinPoint { 
	void** (*fp) (struct JoinPoint *);
	void ** args;
	int argsCount;
	const char ** argsType;
	 void * (*arg)(int, struct JoinPoint *); 
	 const char * (*argType)(int , struct JoinPoint *); 
	void ** retValue;
	const char * retType;
	const char * funcName ;
	const char * targetName ;
	const char * fileName ;
	const char * kind ;
	void * excep_return ;
};

 struct __UTAC__EXCEPTION {
	void * jumpbuf ;
	unsigned long long int prtValue ;
	int pops;
	struct __UTAC__CFLOW_FUNC {
		int (*func)(int,int) ;
		int val ;
		struct __UTAC__CFLOW_FUNC * next; 
	} * cflowfuncs; 
}; 

extern void __utac__exception__cf_handler_set(void * exception, int (*cflow_func)(int, int), int val) ; 
extern void __utac__exception__cf_handler_free(void * exception);
extern void __utac__exception__cf_handler_reset(void * exception) ; 
extern void * __utac__error_stack_mgt(void * env , int mode, int count) ;

# 1 "MyAspect.c" 
# 1 "<built-in>" 
# 1 "<command-line>" 
# 1 "/usr/include/stdc-predef.h" 1 3 4
# 1 "<command-line>" 2
# 1 "MyAspect.c" 
# 5 "Clock.h" 1
void Clock_Update(); 
#line 6 "Clock.h"
int Clock_GetTime(); 
# 2 "MyAspect.c" 2
# 1 "Asserts.h" 1
# 34 "Asserts.h" 
enum assertList {RMEM_OP=0,RMEM_O1=1,RMEM_O2=2,AMEM_OP=3,AMEM_O1=4,AMEM_O2=5,PC=6,ACC=7,IR_OP=8,IR_O1=9,IR_O2=10,PSW=11,MAR=12,MBR_OP=13,MBR_O1=14,MBR_O2=15,MMU_BS=16,MMU_LM=17,MMU_MAR=18,MMEM_MAR=19,MMBR_OP=20,MMBR_O1=21,MMBR_O2=22,XPID=23}; 
#line 41 "Asserts.h"
typedef struct {int time; int value; char element[8]; int address; 
}ASSERT_DATA; 
#line 44 "Asserts.h"
int Asserts_LoadAsserts(); 
#line 45 "Asserts.h"
void Asserts_CheckAsserts(); 
#line 46 "Asserts.h"
void Asserts_TerminateAssertions(); 
#line 48 "Asserts.h"
extern  ASSERT_DATA asserts[500]; 
#line 5 "MyAspect.c"
 inline void __utac_acc__Aspect__1(void) { 



#line 6 "MyAspect.c"
Clock_Update(); }

 
#line 9 "MyAspect.c"
 inline void __utac_acc__Aspect__2(void) { 



#line 10 "MyAspect.c"
Asserts_CheckAsserts(); }

 
#line 13 "MyAspect.c"
 inline void __utac_acc__Aspect__3(void) { 



#line 14 "MyAspect.c"
Clock_Update(); }

 



