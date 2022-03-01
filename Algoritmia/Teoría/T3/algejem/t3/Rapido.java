// PROBLEMA 6: ORDENAR n ELEMENTOS
// RÁPIDO O QUICKSORT



public class Rapido
{
	static int []v;

	/** ALGORITMO DyV de complejidad:
		CASO MEJOR y MEDIO: 
		  POR DIVISION a=2;b=2;k=1 === O(nlogn)
		CASO PEOR:
		  POR SUSTRACCIÓN a=1;b=1;k=1 === O(n^2) 
	 */
	public static void rapido (int[] v) 
	{
		rapirec (0,v.length-1);
	}

	private static void rapirec (int iz,int de) 
	{
		if (de>iz) 
		{
			int m= particion (iz,de);

			rapirec (iz,m-1);
			rapirec (m+1,de);
		}
	}

	/** es lineal O(n), que al final deja el 
		pivote en una posicion tal que a su izquierda no
		hay ningún mayor, ni a la derecha ningún menor */
	private static int particion (int iz,int de) 
	{
		int i, pivote;
		intercambiar((iz+de)/2,iz); 		
		//el pivote es el de centro y se cambia con el primero
		pivote= v[iz]; 
		i=iz;
		for (int s=iz+1;s<=de;s++) 
			if (v[s]<=pivote) 
			{
				i++;
				intercambiar(i,s);
			}
		intercambiar (iz,i); 
		// se restituye el pivote donde debe estar
		return i;//retorna la posicion en que queda el pivote 
	}

	/** Intercambia 2 elementos. Complejidad es O(1)
	 */
	private static void intercambiar(int i,int j)
	{
		int t;
		t=v[i];v[i]=v[j];v[j]=t;
	}
	
	public static void imprimirVector(String etiqueta, int[] v, int izq, int der)
	{
		System.out.print(etiqueta);

		for (int i=izq; i<der; i++) 
			System.out.print (v[i]+" ");
		System.out.println("\tizq= "+izq+" der="+der);
	}

	// PROBAR FUNCIONAMIENTO

	public static void main (String arg [] )
	{
		int n=10;  
		v = new int [n];
		for (int i=0;i<n;i++) v[i]=n-i-1; //p.e. inverso
		imprimirVector("Vector original: ",v,0,v.length);
		rapido(v);
		for (int i=0;i<n;i++) System.out.print (v[i]+" ");
	}
} 
