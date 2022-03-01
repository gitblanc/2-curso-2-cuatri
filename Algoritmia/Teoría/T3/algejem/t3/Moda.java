// PROBLEMA 9: MODA DE n ELEMENTOS

// mediante 3 (en realidad 2) algoritmos diferentes

//import RapidoTraza;

public class Moda
{
	static int []v;

	/**  Este método calcula de forma iterativa 
		el elemento que predomina (moda).
		Es cuadrático =(n^2)  */
	public static void moda1(int[]v,int[]mo)
	{
		int n=v.length;
		mo[1]=0;
		int cont=0;
		for (int i=0;i<n;i++)
		{
			cont=0; 
			for (int j=i;j<n;j++)
				if (v[j]==v[i]) cont++;
			if (cont>mo[1]) 
			{mo[0]=v[i];
			mo[1]=cont;
			}     
		}
	}    

	/** Este método ordena O(nlogn) previamente
		el vector y después calcula la moda (O(n)) 
		Es O(nlogn)+O(n)=O(nlogn)
		Es DyV, en cuanto es ordenación por rápido
	 */
	public static void moda2(int[]v,int[]mo)
	{
		RapidoTraza.rapido(v);
		int n=v.length;
		mo[0]=v[0];
		mo[1]=1;
		int cont=1;
		for (int i=1;i<n;i++)
		{
			if (v[i]==v[i-1])
			{
				cont++;
				if (cont>mo[1])
				{
					mo[0]=v[i];
					mo[1]=cont;
				}     
			}
			else cont=1;  
		}  
	}    

	/* Hay un método DyV muy avanzado de alta dificultad
	  que mejora al anterior en muy poco y que está 
	  explicado en el libro base en el apartado 3.11
	 */

	// PROBAR FUNCIONAMIENTO

	public static void main (String arg [] )
	{
		int n=6;
		v=new int[n];
		v[0]=3;v[1]=7;v[2]=7;v[3]=1;v[4]=7;v[5]=3;
		int[] mo=new int[2]; //moda y veces que está
		moda1(v,mo);
		System.out.println("ALGORITMO1="+mo[0]+"**"+mo[1]);
		moda2(v,mo);
		System.out.println("ALGORITMO2="+mo[0]+"**"+mo[1]);
	}
}