// PROBLEMA 5:CÁLCULO DE LA POSICIóN DE UN 
// ELEMENTO x EN UN VECTOR ORDENADO
// DE n ENTEROS DIFERENTES
// BUSQUEDA BINARIA O DICOTÓMICA



// mediante 2 algoritmos diferentes

public class BusqBin
{
	static int []v;
	static int x;

	/** Este método calcula de forma iterativa la 
		posición de x en el vector  ordenado. Es O(log n)
		DyV con programación iterativa */
	public static int busqbin1 (int[]v,int x)
	{
		int iz=0;
		int de=v.length-1;
		int m;
		while (iz<=de)
		{
			m=(iz+de)/2;
			if (v[m]==x) return m;
			else if (v[m]>x) de=m-1;
			else iz=m+1;	 	 	       
		}
		return -1;
	}

	/** Este método calcula de forma recursiva la 
		posición de x en el vector  ordenado. 
		DyV POR DIVISIÓN CON a=1;b=2;k=0 === O(logn) 
	 */
	public static int busqbin2 (int[]v,int x)
	{
		return busqrec (0,v.length-1);
	}

	private static int busqrec(int iz,int de)
	{
		if (iz>de) return -1;  // no existe x
		else
		{
			int m=(iz+de)/2;
			if (v[m]==x) return m;
			else if (v[m]>x)
				return busqrec(iz,m-1);
			else return busqrec(m+1,de);	 	 	       
		}
	}

	// PROBAR FUNCIONAMIENTO
	public static void main (String arg [] )
	{
		int n=10;
		v=new int[n];
		for (int i=0;i<n;i++) v[i]=i;
		x=3;
		System.out.println (busqbin1 (v,x));
		System.out.println (busqbin2 (v,x));
		x=18;
		System.out.println (busqbin1 (v,x));
		System.out.println (busqbin2 (v,x));
	} 
}