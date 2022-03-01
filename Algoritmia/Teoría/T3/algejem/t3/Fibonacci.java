// PROBLEMA 2: CÁLCULO DEL NÚMERO DE 
// FIBONACCI DE ORDEN n
// fib(n)= fib(n-1)+fib(n-2) si n>1, fib(n)= n si n<=1

// Serie Fibonacci=0,1,1,2,3,5,8,13,21,34,55,89,...
// p.e el 0 es el n=0 y el 89 es el n=11

// Se proponen 5 algoritmos diferentes



public class Fibonacci
{

	/** Primera solución, iterativa con 
		complejidad temporal O(n)  */
	public static int fib1 (int n) 
	{
		int n1= 0;
		int n2= 1;
		for (int i= 1; i <= n; i++) 
		{
			int s= n1+n2;
			n1= n2;
			n2= s;
		}
		return n1;
	}  

	/** Segunda solución. iterativa con complejidad
		temporal O(n) y que utiliza un vector 
		caso sencillo de PROGRAMACION DINÁMICA */
	public static int fib2 (int n, int[]v) 
	{
		v[0] =0;
		v[1]=1;
		for (int i=2; i <= n; i++) 
			v[i]=v[i-1]+v[i-2];
		return v[n];
	}

	/** Primera Solución recursiva lineal O(n)
		DyV POR SUSTRACCIÓN CON a=1;b=1;k=0 === O(n) */
	public static int fib3 (int n) 
	{
		return aux(0,1,n);
	}
	
	private static int aux (int n1,int n2,int n) 
	{
		if (n < 1) return n1;
		return aux(n2,n1+n2,n-1);
	}

	/** Segunda versión recursiva, de ecuación 
		T(n)=T(n-1)+T(n-2)+O(1),que una vez resuelta
		es exponencial O(1.6^n), 
		En DEFINITIVA, ESTA ES UNA SOLUCIÓN INTRATABLE */
	public static int fib4 (int n) 
	{
		if (n<=1) return n;
		return fib4(n-1) + fib4(n-2);
	}

	/** solución sofisticada DyV que es O(log n)
		es DyV POR DIVISIÓN CON a=1;b=2;k=0 y está
		programada en forma iterativa */
	public static int fib5 (int n) 
	{
		int i=1;int j=0;int k=0;int h=1;int t=0;
		while (n>0)
		{
			if (n%2==1)
			{ 
				t=j*h;
				j=i*h+j*k+t;
				i=i*k+t;
			}
			t=h*h;
			h=2*k*h+t;
			k=k*k+t;
			n=n/2;
		}
		return j;
	}  

	// PROBAR FUNCIONAMIENTO 

	public static void main (String[] arg) 
	{ 
		int n=11;
		System.out.println ("ALGORITMO1="+fib1(n));
		int[]v = new int[60];//podría calcular hasta n=60  
		System.out.println ("ALGORITMO2="+fib2(n,v));
		System.out.println ("ALGORITMO3="+fib3(n));
		System.out.println ("ALGORITMO4="+fib4(n));
		System.out.println ("ALGORITMO5="+fib5(n));
	} 
}
