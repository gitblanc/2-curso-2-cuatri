// PROBLEMA 1: CÁLCULO DEL FACTORIAL DE n>=0



// mediante 2 algoritmos diferentes

public class Factorial
{

	/** Este método calcula de forma iterativa el 
		factorial con una complejidad lineal O(n) 
	 */
	public static int fact1(int n)
	{
		int f=1;
		for(int i=1;i<=n;i++) f=f*i;
		return f; 
	}    


	/** Este método calcula de forma recursiva
		el factorial con una complejidad lineal O(n)
		DyV POR SUSTRACCIÓN CON a=1;b=1;k=0 ==> O(n)
	 */
	public static int fact2 (int n)
	{ 
		if (n==0) return 1;
		else return n*fact2(n-1);
	}   


	// PROBAR FUNCIONAMIENTO
	public static void main (String arg [] )
	{
		System.out.println ("SOLUCION1 ="+fact1(7));
		System.out.println ("SOLUCION2 ="+fact2(7));
	}
}
