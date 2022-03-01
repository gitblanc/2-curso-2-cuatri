// PROBLEMA 7: ORDENAR n ELEMENTOS
// MEZCLA o MERGESORT

public class Mezcla
{
	static int []v;

	/** ALGORITMO DyV **/
	public static void mezcla(int[]v) 
	{
		mezclarec (0,v.length-1);
	}

	private static void mezclarec(int iz,int de) 
	{
		if (de>iz) 
		{
			int m=(iz+de)/2;
			mezclarec(iz,m);
			mezclarec(m+1,de);
			combina(iz,m,m+1,de);
		}
	}

	/** Mezcla una secuencia ordenada 
		entre x1..x2 con otra entre y1..y2, llevando el
		resultado sobre el vector ordenado v.
		Tiene que utilizar para ello dos vectores auxiliares, 
		al ser imposible ir haciéndolo todo sobre v */
	private static void combina(int x1, int x2,int y1,int y2) 
	{
		int t1=x2-x1+1; 
		int t2=y2-y1+1;
		int[]x=new int[t1];
		int[]y=new int[t2];
		for (int i=0;i<t1;i++) x[i]=v[x1+i];
		for (int i=0;i<t2;i++) y[i]=v[y1+i];
		int l1=0;
		int l2=0;
		int c1=0;
		int c2=0;
		for (int i=0;i<t1+t2;i++)
		{ 
			if (l1<t1) c1=x[l1];
			else c1=Integer.MAX_VALUE;
			if (l2<t2) c2=y[l2];
			else c2=Integer.MAX_VALUE;
			if (c1<=c2)
			{
				v[x1+i]=c1;
				l1=l1+1;
			}
			else
			{
				v[x1+i]=c2;
				l2=l2+1;
			}
		}  
	} 

	// PROBAR FUNCIONAMIENTO

	public static void main (String arg [] )
	{
		int n=10;  
		v = new int [n];
		for (int i=0;i<n;i++) v[i]=n-i-1; // p.e. inverso
		mezcla(v);
		for (int i=0;i<n;i++) System.out.print (v[i]+"//");
	}
} 

