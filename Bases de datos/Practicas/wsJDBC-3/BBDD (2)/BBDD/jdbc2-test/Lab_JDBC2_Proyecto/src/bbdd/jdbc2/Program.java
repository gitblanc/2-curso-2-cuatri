package bbdd.jdbc2;
import java.util.Scanner;

public class Program {
	
	public static void main(String[] args) {
		//Ejemplos para leer por teclado
		System.out.println("Read an integer by keyboard");	
		int integ = ReadInt();
		System.out.println("Read a string by keyboard");	
		String str = ReadString();
	}

	
	/*
    2. En JAVA:
    b. Realizar un listado en el que, para los cines de una determinada localidad, se indique la recaudacion total
	obtenida en cada cine, asi como la obtenida por cada una de las peliculas en el proyectadas
    	Cine 1 - Recaudacion_total
			Codpelicula1 - Titulo1- Recaudacion_total_pelicula_1_en_cine1
			Codpelicula 2 - Titulo2 - Recaudacion_total_pelicula_2_en_cine1
		Cine 2 – Recaudacion_total
			Codpelicula1 – Titulo1 – Recaudacion_total_pelicula_1_en_cine2
			Codpelicula 2 – Titulo2 – Recaudacion_total_pelicula_2_en_cine2
			Codpelicula 3 – Titulo3 – Recaudacion_total_pelicula_3_en_cine2 
    */
	public static void exercise2_b() {

	}
	
	/*
	3. En JAVA:
	a. Realizar un listado en el que se indique la siguiente informacion para cada pelicula:
		Titulo_Pelicula 1
			Cine 1
				Sala - Sesion - Numero de espectadores
				Sala - Sesion - Numero de espectadores
			Cine 2
				Sala - Sesion - Numero de espectadores
				Sala - Sesion - Numero de espectadores
	*/
	public static void exercise3_a() {
		
	}
		
	@SuppressWarnings("resource")
	private static String ReadString(){
		return new Scanner(System.in).nextLine();		
	}
	
	@SuppressWarnings("resource")
	private static int ReadInt(){
		return new Scanner(System.in).nextInt();			
	}	
}
