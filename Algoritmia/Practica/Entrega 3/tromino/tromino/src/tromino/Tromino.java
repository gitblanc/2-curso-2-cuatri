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
		trominoRec(holeX, holeY, n, 0, 0);
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

	public void trominoRec(int hx, int hy, int n, int origenX, int origenY) {
		// Checkeamos en qué cuadrante se encuentra el agujero
		// Asignamos las coordenadas que variarán en función del cuadrante
		int pos1 = origenX + (n / 2) - 1;
		int pos2 = origenY + (n / 2) - 1;
		if (n <= 2) {
			int hueco = this.tablero[hx][hy];
			generarTromino(pos1, pos2, pos1, pos2 + 1, pos1 + 1, pos2, pos1 + 1, pos2 + 1);
			this.tablero[hx][hy] = hueco;
		} else if (n >= 4) {
			int x1 = 0;
			int y1 = 0;
			int x2 = 0;
			int y2 = 0;
			int x3 = 0;
			int y3 = 0;
			int x4 = 0;
			int y4 = 0;

			int cuadrante1X = pos1;
			int cuadrante1Y = pos2;
			int cuadrante2X = pos1;
			int cuadrante2Y = pos2 + 1;
			int cuadrante3X = pos1 + 1;
			int cuadrante3Y = pos2;
			int cuadrante4X = pos1 + 1;
			int cuadrante4Y = pos2 + 1;
			// 1er cuadrante
			if (hx <= pos1 && hy <= pos2) {
				cuadrante1X = hx;
				cuadrante1Y = hy;
				x1 = pos1;
				y1 = pos2 + 1;
				x2 = pos1 + 1;
				y2 = pos2;
				x3 = pos1 + 1;
				y3 = pos2 + 1;
			}
			// 2 cuadrante
			else if (hx <= pos1 && hy > pos2) {
				cuadrante2X = hx;
				cuadrante2Y = hy;
				x1 = pos1;
				y1 = pos2;
				x2 = pos1 + 1;
				y2 = pos2;
				x3 = pos1 + 1;
				y3 = pos2 + 1;
			}
			// 3 cuadrante
			else if (hx > pos1 && hy <= pos2) {
				cuadrante3X = hx;
				cuadrante3Y = hy;
				x1 = pos1;
				y1 = pos2;
				x2 = pos1;
				y2 = pos2 + 1;
				x3 = pos1 + 1;
				y3 = pos2 + 1;
			}
			// 4 cuadrante
			else {
				cuadrante4X = hx;
				cuadrante4Y = hy;
				x1 = pos1;
				y1 = pos2;
				x2 = pos1 + 1;
				y2 = pos2;
				x3 = pos1;
				y3 = pos2 + 1;
			}
			generarTromino(x1, y1, x2, y2, x3, y3, x4, y4);

			// llamadas recursivas
			trominoRec(cuadrante1X, cuadrante1Y, n / 2, origenX, origenY);
			trominoRec(cuadrante2X, cuadrante2Y, n / 2, origenX, origenY + (n / 2));
			trominoRec(cuadrante3X, cuadrante3Y, n / 2, origenX + (n / 2), origenY);
			trominoRec(cuadrante4X, cuadrante4Y, n / 2, origenX + (n / 2), origenY + (n / 2));
		}

	}

	private void generarTromino(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4) {
		contTromino++;
		this.tablero[x1][y1] = 1;
		this.tablero[x2][y2] = 1;
		this.tablero[x3][y3] = 1;
		this.tablero[x4][y4] = 1;
	}

	public void ShowTablero() {
		for (int i = 0; i < this.tablero.length; i++) {
			for (int j = 0; j < this.tablero.length; j++) {
				System.out.print(tablero[i][j] + "\t");
			}
			System.out.println("\n");
		}
		System.out.println("Número total de trominos = " + contTromino);
	}
}
