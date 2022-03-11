/**
 * 
 */
package tromino;

/**
 * @author UO285176
 *
 */
public class Tromino {

	private int[][] tablero;
	private int contTromino;// contador de trominos

	public void createTablero(int n, int holeX, int holeY) {
		tablero = new int[n][n];
		rellenarTablero(holeX, holeY);
		trominoRec(holeX, holeY, n);
	}

	private void rellenarTablero(int holeX, int holeY) {
		for (int i = 0; i < this.tablero.length; i++) {
			for (int j = 0; j < this.tablero.length; j++) {
				if (holeX == i && holeY == j) {
					tablero[i][j] = -1;// el hueco inicial
				} else {
					// sino
					tablero[i][j] = 0;// se inicializan todos los elementos
				}
			}
		}
	}

	public int trominoRec(int hx, int hy, int n) {
		if (n == 2) {// si el tamaño del cuadrante es 2
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < n; j++) {
					if (tablero[i][j] == 0)
						tablero[i][j] = 1;
				}
			}
			contTromino++;
			return 0;
		}

		// Checkeamos en qué cuadrante se encuentra el agujero

		// 1er cuadrante
		if (hx < n / 2 && hy < n / 2) {
			generarTromino(n / 2, (n / 2) - 1, n / 2, n / 2, n / 2 - 1, n / 2);
		}
		// 2 cuadrante
		if (hx < n / 2 && hy >= n / 2)
			generarTromino((n / 2) - 1, (n / 2) - 1, n / 2, n / 2, n / 2, (n / 2) - 1);
		// 3 cuadrante
		if (hx >= n / 2 && hy < n / 2)
			generarTromino((n / 2) - 1, (n / 2) - 1, (n / 2) - 1, n / 2, n / 2, n / 2);
		// 4 cuadrante
		if (hx >= n / 2 && hy >= n / 2)
			generarTromino((n / 2) - 1, (n / 2) - 1, (n / 2) - 1, n / 2, n / 2, (n / 2) - 1);

		// llamadas recursivas
		trominoRec(hx, hy, n / 2);
		trominoRec(hx, hy + 1, n / 2);
		trominoRec(hx + 1, hy, n / 2);
		trominoRec(hx + 1, hy + 1, n / 2);
		return 0;

	}

	private void generarTromino(int x1, int y1, int x2, int y2, int x3, int y3) {
		contTromino++;
		this.tablero[x1][y1] = 1;
		this.tablero[x2][y2] = 1;
		this.tablero[x3][y3] = 1;
	}

	public void ShowTablero() {
		for (int i = 0; i < this.tablero.length; i++) {
			for (int j = 0; j < this.tablero.length; j++) {
				System.out.print(tablero[i][j] + "\t");
			}
			System.out.println("\n");
		}
	}
}
