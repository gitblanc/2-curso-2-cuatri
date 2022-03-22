/**
 * 
 */
package grafos;

/**
 * @author UO285176
 *
 */
public class Main {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		@SuppressWarnings("unused")
		ColoracionMapa c;
		long t1;
		long t2;
		for (int k = 8; k <= 1000000; k *= 2) {
			t1 = System.currentTimeMillis();
			for (int i = 0; i < k; i++) {
				c = new ColoracionMapa();
				// c.imprimirSolucion();
			}
			t2 = System.currentTimeMillis();
			System.out.println("TAMAÑO = " + k + " TIEMPO = " + (t2 - t1));
		}

	}

}
