// PROBLEMA 3: CÁLCULO DE LA SUMA DE 
// n ELEMENTOS (VECTOR)

// mediante 3 algoritmos diferentes

public class SumaVector
{
	static int []v;

	/** este método calcula de forma iterativa la 
		suma con una complejidad lineal O(n) */
	public static int suma1(int[]v)
	{
		int n= v.length;
		int s=0;
		for(int i=0;i<n;i++) s=s+v[i];
		return s; 
	}    

	/** este método calcula de una forma recursiva
		la suma con una complejidad lineal O(n)
		DyV POR SUSTRACCIÓN CON a=1;b=1;k=0 === O(n)*/
	public static int suma2 (int[]v)
	{
		return recSust (0);
	}   
	
	private static int recSust(int i)
	{
		if (i==v.length) return 0;
		else return v[i]+recSust(i+1);
	}  

	/** este método calcula de otra forma recursiva
		la suma con una complejidad lineal O(n) 
		DyV POR DIVISIÓN CON a=2;b=2;k=0 === O(n) */
	public static int suma3 (int[]v)
	{
		return recDiv (0,v.length-1);
	} 
	
	private static int recDiv(int iz,int de)
	{
		if (iz==de) 
			return v[iz];
		else
		{ 
			int m=(iz+de)/2;
			return recDiv(iz,m)+recDiv(m+1,de);
		}
	}  

	// PROBAR FUNCIONAMIENTO
	public static void main (String arg [] )
	{
		int n = 100;
		v=new int[n];
		for ( int i=0; i<n;i++) v[i]=i;
		System.out.println ("SOLUCION1 ="+suma1(v));
		System.out.println ("SOLUCION2 ="+suma2(v));
		System.out.println ("SOLUCION3 ="+suma3(v));
	}
}
