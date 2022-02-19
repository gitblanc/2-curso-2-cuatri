/**
 * 
 */
package matriz;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;

/**
 * @author UO285176
 *
 */
public class MatrizOperaciones {
	private int[][] matrix;

	public MatrizOperaciones(int n, int min, int max) {
		this.matrix = new int[n][n];
		rellenarValores(min, max);
	}

	public MatrizOperaciones(String nomFich) {
		this.matrix = obtenerMatrizFichero(nomFich);
	}

	private int[][] obtenerMatrizFichero(String nomFich) {
		String linea;
		String[] lineas = new String[2];
		int i = 0;

		try {
			BufferedReader fichero = new BufferedReader(new FileReader(nomFich));
			while (fichero.ready()) {
				linea = fichero.readLine();
				lineas[i] = linea;
				i++;
			}
			fichero.close();
			
		} catch (FileNotFoundException fnfe) {
			System.out.println("El archivo no se ha encontrado.");
		} catch (IOException ioe) {
			new RuntimeException("Error de entrada/salida.");
		}
		return analizarLineas(lineas);
		
	}

	private int[][] analizarLineas(String[] lineas) {
		String[] datosMatriz = null;
		int n = Integer.parseInt(lineas[0]);
		int[][] nuevaMatriz = new int[n][n];
		datosMatriz = lineas[1].split("\t");
		int cont = 0;
		for(int i = 0; i < nuevaMatriz.length; i++) {
			for(int j = 0; j < nuevaMatriz.length; j++) {
				nuevaMatriz[i][j] = Integer.parseInt(datosMatriz[cont]);
				cont++;
			}
		}
		return nuevaMatriz;
	}

	private void rellenarValores(int min, int max) {
		for (int i = 0; i < this.matrix.length; i++) {
			for (int j = 0; j < this.matrix.length; j++) {
				this.matrix[i][j] = (int) (Math.random() * (max - min + 1) + min);
			}

		}

	}

	public int getTam() {
		return this.matrix.length;
	}

	public void escribir() {
		String cad = "";
		for (int i = 0; i < this.matrix.length; i++) {
			for (int j = 0; j < this.matrix.length; j++) {
				cad += this.matrix[i][j] + "\t";
			}
			cad += "\n";
		}
		System.out.println(cad);
	}

	public int sumarDiagonal1() {
		int suma = 0;
		for (int i = 0; i < this.matrix.length; i++) {
			for (int j = 0; j < this.matrix.length; j++) {
				if (i == j) {
					suma += this.matrix[i][j];
				}
			}
		}
		return suma;
	}

	public int sumarDiagonal2() {
		int[] diagonal = new int[getTam()];
		for (int i = 0; i < this.matrix.length; i++) {
			for (int j = 0; j < this.matrix.length; j++) {
				if (i == j) {
					diagonal[i] = this.matrix[i][j];
				}
			}
		}
		return sumarElementos(diagonal);
	}

	private int sumarElementos(int[] diagonal) {
		int suma = 0;
		for (int i = 0; i < diagonal.length; i++) {
			suma += diagonal[i];
		}
		return suma;
	}

	public void recorrerCamino(int i, int j) {
		int min = 0;
		int max = getTam();
		while ((i >= min && i < max) && (j >= min && j < max) && this.matrix[i][j] != -1) {
			int elemento = this.matrix[i][j];
			switch (elemento) {
			case 1:
				this.matrix[i][j] = -1;
				i -= 1;// se mueve arriba
				break;
			case 2:
				this.matrix[i][j] = -1;
				j += 1;// se mueve a la derecha
				break;
			case 3:
				this.matrix[i][j] = -1;
				i += 1;// se mueve abajo
				break;
			default:
				this.matrix[i][j] = -1;
				j -= 1;// se mueve a la izquierda
				break;
			}
		}
	}
}

//
//	@Override
//	public String toString() {
//		String cad = "";
//		for (int i = 0; i < getTam(); i++) {
//			for (int j = 0; j < getTam(); j++) {
//				cad += this.matrix[i][j] + "\t";
//			}
//			cad += "\n";
//		}
//		return cad;
//	}
