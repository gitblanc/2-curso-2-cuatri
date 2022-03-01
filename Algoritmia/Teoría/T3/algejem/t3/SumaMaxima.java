// PROBLEMA 11: SUMA MÁXIMA DE TODAS LAS
// SUBSECUENCIAS DE n ELEMENTOS



// mediante 3 algoritmos diferentes

public class SumaMaxima
{

	static int []v;

	/** este algoritmo es iterativo
		Es cúbico 0(n^3)  */
	public static int sumamax1 (int[]v)
	{
		int n=v.length;
		int max=0; 
		for(int i=0;i<n;i++)
			for(int j=i;j<n;j++)
			{
				int suma=0;
				for(int k=i;k<=j;k++)
					suma=suma+v[k];
				if(suma>max) max=suma;
			}
		return max;
	} 

	/** este algoritmo es iterativo
		Es cuadrático 0(n^2) */
	public static int sumamax2(int[]v)
	{
		int n=v.length;
		int max = 0;
		for(int i=0;i<n;i++)
		{
			int suma=0;
			for(int j=i;j<n;j++)
			{
				suma=suma+v[j];
				if(suma>max) max=suma;
			}
		}
		return max;
	} 

	/** este algoritmo es recursivo
		DyV POR DIVISIÓN con a=2;b=2;k=1 === O(nlogn)
	 */
	public static int sumamax3 (int[]v)
	{
		return sumarec (0,v.length-1);
	}

	private static int sumarec (int iz,int de)
	{	
		if (iz==de) 
			return v[iz];
		else
		{
			int m=(iz+de)/2;
			int maxiz=sumarec(iz,m);
			int maxde=sumarec(m+1,de);

			// calcular el máximo de la subsecuencia que pasa
			// de una mitad a la otra mitad 
			int s1=0;int maxs1=0;
			for(int i=m;i>=iz;i--)
			{
				s1=s1+v[i];
				if (s1>maxs1) maxs1=s1;
			}
			int s2=0;int maxs2=0;
			for(int i=m+1;i<=de;i++)
			{
				s2=s2+v[i];
				if (s2>maxs2) maxs2=s2;
			}
			return mayor (maxiz,maxde,maxs1+maxs2);
		}
	}

	/** devuelve el mayor de los tres
	 */
	private static int mayor(int a,int b,int c)
	{ if (a>=b && a>=c) return a;
	else if (b>=a && b>=c) return b;
	else return c;
	}

	// PROBAR FUNCIONAMIENTO

	public static void main (String arg [] )
	{
		int n=6;  
		v = new int [n];
		v[0]=5;v[1]=-4;v[2]=3;v[3]=2;v[4]=5;v[5]=-1;
		System.out.println("ALGORITMO1="+sumamax1(v));
		System.out.println("ALGORITMO2="+sumamax2(v));
		System.out.println("ALGORITMO3="+sumamax3(v));
	}
}