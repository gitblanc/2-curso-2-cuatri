/**
 * 
 */
package tromino;

/**
 * @author blanc
 *
 */
public class TrominoTiempos {

	/**
	 * @param args
	 */
	public static void main(String[] arg) {
		int nVeces = Integer.parseInt(arg[0]);
		Tromino t = new Tromino();
//		t.createTablero(8, 3, 5);// funciona
//		t.ShowTablero();
		generarTromino(t, nVeces);
	}

	private static void generarTromino(Tromino t, int nVeces) {
		long t1;
		long t2;
		for (int i = 16; i < 1000000; i *= 2) {
			t1 = System.currentTimeMillis();
			for (int repeticiones = 1; repeticiones <= nVeces; repeticiones++) {
				
				t.createTablero(i, 3, 5);
			}
			t2 = System.currentTimeMillis();
			System.out.println("TAMAÑO = " + i + " TIEMPO = " + (t2 - t1) + " REPETICIONES = " + nVeces);
			System.out.println();
		}

	}

}
