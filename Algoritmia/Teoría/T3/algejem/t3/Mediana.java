// PROBLEMA 10:LA MEDIANA DE n ELEMENTOS
// El elemento central cuando los elementos están ordenados 
// mediante 2 algoritmos diferentes

//import RapidoTraza;

public class Mediana
{
	static int []v;

	/*  Este método ordena el vector (O(nlogn))
		y después en básico (O(1) su cálculo
		Es  O(nlogn)+ O(1)= O(nlogn) */
	public static int mediana1 (int[]v)
	{
		RapidoTraza.rapido(v);
		int posmed=v.length/2;
		return v[posmed]; 
	}    

	/**	Este método es recursivo y está basado en el
		concepto de partición visto en el rápido.
		DyV POR DIVISIÓN a=1; b=2; k=1 === O(n) */
	public static int mediana2(int[]v)
	{
		int med=medirec(0,v.length-1);
		return med;
	}

	private static int medirec(int iz,int de) 
	{ 
		int m=particion (iz,de);
		int posmed=v.length/2;
		if (m==posmed) 
			return v[m];
		else 
			if (m>posmed) 
				return medirec(iz,m-1);
			else 
				return medirec(m+1,de);
	}

	/** es O(n), y al final deja el pivote en una
		posicion tal que a su izquierda no hay ningún
		mayor, ni a la derecha ningún menor */
	private static int particion (int iz,int de) 
	{
		int i, pivote;
		permuta ((iz+de)/2,iz); 
		//el pivote es el de centro y se cambia con el primero
		pivote= v[iz]; 
		i=iz;
		for (int s=iz+1;s<=de;s++) 
			if (v[s]<=pivote) 
			{
				i++;
				permuta(i,s);
			}
		permuta(iz,i); 
		// se restituye el pivote donde debe estar
		return i;// retorna la posicion en que queda el pivote 
	}

	private static void permuta(int i,int j)
	//es O(1)
	{
		int t;
		t=v[i];v[i]=v[j];v[j]=t;
	}

	// PROBAR FUNCIONAMIENTO

	public static void main (String arg [] )
	{
		int n=7;
		v=new int[n];
		v[0]=5;v[1]=8;v[2]=3;v[3]=1;v[4]=18;v[5]=12;v[6]=10;
		System.out.println("ALGORITMO1="+mediana1(v));
		v[0]=5;v[1]=8;v[2]=3;v[3]=1;v[4]=18;v[5]=12;v[6]=10;
		System.out.println("ALGORITMO2="+mediana2(v));
	}
}