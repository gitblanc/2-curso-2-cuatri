/*
	JAVA es sensible a minúsculas y mayusculas (minúscula != mayúscula)
	Es norma que una clase comience por letra mayúscula.
	Los métodos y todo tipo de variables comienzan por minúscula.

	Las clases JAVA se guardan en ficheros con el mismo nombre
	al que se añade la extensión Nombre.java.

	Los paquetes deben estar en directorio del mismo nombre, esto es,
	el paquete alg77777777.p1 debe estar en el directorio
	alg77777777\p1. 
 */

package p1;

import java.util.Random; //es la clase que genera números aleatorios

/** Esta clase permite trabajar con vectores */
public class Vector3 {
	static int[] v;

	public static void main(String arg[]) {

		for (int i = 10; i < 143489071; i *= 3) { //con la i = 143489071 el programa peta(es el ultimo que puede ejecutar)
			long inicioTiempoSumaConRellena = System.currentTimeMillis();
			v = new int[i];
			rellena(v);
			long inicioTiempoSumaSinRellena = System.currentTimeMillis();
			int s = suma(v);
			long finTiempoS = System.currentTimeMillis();
			long resultadoSumaConRellena = finTiempoS - inicioTiempoSumaConRellena;
			long resultadoSumaSinRellena = finTiempoS - inicioTiempoSumaSinRellena;

			System.out.println("Suma de los elementos del vector = " + s);
			System.out.println("La suma (con rellena) tard�: " + resultadoSumaConRellena + " milisegundos");

			System.out.println("La suma (sin rellena) tard�: " + resultadoSumaSinRellena + " milisegundos");
			System.out.println(i);
			System.out.println("---------");
			
		}
		System.out.println("------------------------------------------");

		int[] m = new int[2];
		for (int i = 10; i < 143489071; i *= 3) {//con la i = 143489071 el programa peta
			long inicioTiempoMConRellena = System.currentTimeMillis();
			v = new int[i];
			rellena(v);
			long inicioTiempoMSinRellena = System.currentTimeMillis();
			maximo(v, m);
			long finTiempoM = System.currentTimeMillis();
			long resultadoMaxConRellena = finTiempoM - inicioTiempoMConRellena;
			long resultadoMaxSinRellena = finTiempoM - inicioTiempoMSinRellena;

			System.out.println("Valor del m�ximo del vector = " + m[1]);
			System.out.println("Posici�n del m�ximo del vector = " + m[0]);
			System.out.println("El m�ximo (con rellena) tard�: " + resultadoMaxConRellena+ " milisegundos");
			System.out.println("El m�ximo (sin rellena) tard�: " + resultadoMaxSinRellena+ " milisegundos");
			System.out.println(i);
			System.out.println("---------");

		}

	} // fin de main

	/**
	 * Este método da valores aleatorios a un vector de enteros, utiliza para ello
	 * la clase Random del paquete java.util
	 **/
	public static void rellena(int[] a) {
		Random r = new Random();
		int n = a.length;
		for (int i = 0; i < n; i++)
			a[i] = r.nextInt(199) - 99;// valores entre -99 y 99

	} // fin de rellena

	/**
	 * Este método saca el contenido por pantalla
	 **/
	public static void escribe(int[] a) {
		int n = a.length;
		for (int i = 0; i < n; i++)
			System.out.println("Elemento " + i + " = " + a[i]);
		System.out.println();

	} // fin de escribe

	/**
	 * Este método suma los elementos de un vector y devuelve el resultado
	 */
	public static int suma(int[] a) {
		int s = 0;
		int n = a.length;
		for (int i = 0; i < n; i++)
			s = s + a[i];
		return s;

	} // fin de suma

	/**
	 * Este método calcula el máximo y su posición y devuelve los dos valores.
	 */
	public static void maximo(int[] a, int[] m) {
		int n = a.length;
		m[0] = 0; // posición inicial
		m[1] = a[0]; // primer elemento como referencia
		for (int i = 1; i < n; i++)
			if (a[i] > m[1]) {
				m[0] = i;
				m[1] = a[i];
			}

	} // fin de maximo

} // fin de clase
