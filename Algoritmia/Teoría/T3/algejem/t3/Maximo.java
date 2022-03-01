

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;

public class Maximo
{
	static int v[]= {29, 14, 15, 1,	6, 10, 32, 12};
	
	public static int calcularMax(int[] v, int izq, int der)
	{
		if ((der-izq)<=1)  // tamaño adecuado para la resolución directa
			return (v[izq]>=v[der])?v[izq]:v[der];  // se comparan los dos elementos
		else
			{
				int m= (izq+der)/2;
				int maxIzq= calcularMax(v,izq,m-1);
				int maxDer= calcularMax(v,m,der);
				return (maxIzq>=maxDer)?maxIzq:maxDer;
			}
	}

	public static void main(String[] args)
	{
		if (args.length==1)
		{
			v= carga(args[0]);
		}
			
		int maximo= calcularMax(v,0,v.length-1);
		System.out.println("El máximo del vector es: "+maximo);
	}
	
	/** El método carga los valores del vector desde un fichero.
		Primera línea, el número de valores que contiene, resto de las líneas, cada una un valor
	**/
	public static int[] carga(String nomFich)
	{
		BufferedReader fich= null;
		String linea;
		int[] elementos= null;
		int numElementos;
	
		try {
			// Se abre el fichero de texto
			fich= new BufferedReader(new FileReader(nomFich));
			linea= fich.readLine();
			// La primera línea contiene el número de elementos
			numElementos= Integer.parseInt(linea);
			/**/System.out.println("Número elementos del fichero: "+nomFich+" = "+numElementos);
	
			// Creo el array del tamaño adecuado
			elementos= new int[numElementos];
			for (int i= 0; i<numElementos; i++)
				elementos[i]= Integer.parseInt(fich.readLine());
	
		} catch (FileNotFoundException e) {
			System.out.println("No se encuentra el fichero: "+nomFich);
		} catch (IOException e) {
			System.out.println("Error en la lectura del fichero: "+nomFich);
		} finally {
			if (fich!=null)
				try {
					fich.close();
				} catch (IOException e) {
					System.out.println("Error al cerrar el fichero: "+nomFich);
					e.printStackTrace();
				}
		}
	
		return elementos;
	}

	
}

