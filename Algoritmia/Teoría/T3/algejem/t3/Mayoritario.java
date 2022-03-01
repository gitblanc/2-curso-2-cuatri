// PROBLEMA 8: ¿EXISTE ELEMENTO 
// MAYORITARIO EN n ELEMENTOS?
// Un elemento se considera mayoritario cuando en un vector 
// de n elementos aparece al menos n/2+1 veces


// mediante 3 algoritmos diferentes

//import RapidoTraza;

public class Mayoritario
{
	static int []v;

	/** Este método calcula de forma iterativa 
		si hay o no elemento mayoritario.
		Es cuadrático O(n^2) */
	public static boolean mayoritario1 (int[]v)
	{
		int n=v.length;
		int mayoria=n/2+1;
		int cont=0;
		for (int i=0;i<=n/2;i++)
		{
			cont=0; 
			for (int j=i;j<n;j++)
				if (v[j]==v[i]) cont++;
			if (cont>=mayoria) return true;
		}
		return false;
	}    

	/** Este método ordena previamente el vector
		y despues mira si hay uno mayoritario, 
		sabiendo que si lo hay seguro es el v[n/2]
		Es O(nlogn)+O(n)=O(nlogn) */
	public static boolean mayoritario2 (int[]v)
	{
		RapidoTraza.rapido(v);
		int n=v.length;
		int mayoria=n/2+1;
		int cont=0;
		// si hay mayoritario, es el colocado en el centro
		for (int i=0;i<n;i++)
			if (v[i]==v[n/2]) cont++;
		if (cont>=mayoria) return true; 
		else return false; 
	}    


	/** Este método es recursivo. Ees lineal O(n)
		DyV POR DIVISIÓN CON a=1;b=2;k=1 === O(n) */
	public static boolean mayoritario3 (int[]v)
	{
		int n=v.length;
		int mayoria=n/2+1;
		int cont=0;
		int[]candi=new int[1];
		// el vector puede ser cambiado por candidatorec
		int[]w=new int[n];
		for (int i=0;i<n;i++) 
			w[i]=v[i]; 

		boolean b=(candidatorec(0,v.length-1,candi));

		//restituimos el vector
		for (int i=0;i<n;i++) 
			v[i]=w[i];

		if (b)
		{
			for (int i=0;i<n;i++)
				if (v[i]==candi[0]) cont++;
			if (cont>=mayoria) return true; 
		}  
		return false; 
	}

	private static boolean candidatorec (int iz,int de,int[]candi)
	{ 
		int t=de-iz+1;
		if (t<=0) return false;
		candi[0]=v[iz];
		if (t==1)return true;
		int j=iz;
		if (t%2==0) // es par
		{
			for (int i=iz+1;i<=de;i+=2)
				if (v[i-1]==v[i])
				{v[j]=v[i];j=j+1;}
			return candidatorec (iz,j-1,candi);
		}
		else // es impar
		{
			for (int i=iz+1;i<=de-1;i+=2)
				if (v[i-1]==v[i]) {v[j]=v[i];j=j+1;}
			if (!candidatorec (iz,j-1,candi))
				candi[0]=v[de];
			return true;
		}
	}

	// PROBAR FUNCIONAMIENTO

	public static void main (String arg [] )
	{
		int n=6;
		v=new int[n];

		v[0]=3;v[1]=8;v[2]=3;v[3]=1;v[4]=7;v[5]=3;
		System.out.println("ALGORITMO1="+mayoritario1(v));
		v[0]=3;v[1]=8;v[2]=3;v[3]=1;v[4]=3;v[5]=3;
		System.out.println("ALGORITMO1="+mayoritario1(v));

		v[0]=3;v[1]=8;v[2]=3;v[3]=1;v[4]=7;v[5]=3;
		System.out.println("ALGORITMO2="+mayoritario2(v));
		v[0]=3;v[1]=8;v[2]=3;v[3]=1;v[4]=3;v[5]=3;
		System.out.println("ALGORITMO2="+mayoritario2(v));

		v[0]=3;v[1]=8;v[2]=3;v[3]=1;v[4]=7;v[5]=3;
		System.out.println("ALGORITMO3="+mayoritario3(v));
		v[0]=8;v[1]=3;v[2]=3;v[3]=1;v[4]=3;v[5]=3;
		System.out.println("ALGORITMO3="+mayoritario3(v));
	}
}