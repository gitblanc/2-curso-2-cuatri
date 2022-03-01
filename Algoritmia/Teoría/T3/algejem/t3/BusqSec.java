// PROBLEMA 4:CÁLCULO DE LA POSICIóN DE UN 
// ELEMENTO x EN UN VECTOR DESORDENADO 
// (u ORDENADO) DE n ELEMENTOS DIFERENTES
// BÚSQUEDA SECUENCIAL

// mediante 2 algoritmos diferentes

public class BusqSec
{
	static int []v;
	static int x;

	public static int busqsec1 (int[]v,int x)
	/*  Este método calcula de forma iterativa la 
posición de x en el vector.Es O(n)*/
	{
		int n=v.length;
		int i=0;
		while (i<n)
		{ if (v[i]==x) return i;
		i=i+1;
		}
		return -1;
	}

	public static int busqsec2 (int[]v,int x)
	/*  Este método calcula de forma recursiva la 
posición de x en el vector  ordenado. 
DyV POR SUSTRACCIÓN CON a=1;b=1;k=0 === O(n)*/
	{
		return busqrec (0);
	}

	private static int busqrec(int i)
	{
		if (i==v.length) return -1;// no existe x
		else
		{ if (v[i]==x) return i;
		else return busqrec(i+1);
		}
	}

	// PROBAR FUNCIONAMIENTO
	public static void main (String arg [] )
	{
		int n=10;
		v=new int[n];
		for (int i=0;i<n;i++) v[i]=i;
		x=3;
		System.out.println (busqsec1 (v,x));
		System.out.println (busqsec2 (v,x));
		x=18;
		System.out.println (busqsec1 (v,x));
		System.out.println (busqsec2 (v,x));
	} 
}