/**
 * 
 */
package grafos;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;

/**
 * @author UO285176
 *
 */
public class FileUtil {

	public static void readColors(String nombreFicheroEntrada, String[] colores) {

		String linea;
		int i = 0;

		try {
			BufferedReader fichero = new BufferedReader(new FileReader(nombreFicheroEntrada));
			while (fichero.ready()) {
				linea = fichero.readLine();
				colores[i] = linea;
				i++;
			}
			fichero.close();
		} catch (FileNotFoundException fnfe) {
			System.out.println("El archivo no se ha encontrado.");
		} catch (IOException ioe) {
			new RuntimeException("Error de entrada/salida.");
		}
	}
	
	public static void readCountries(String nombreFicheroEntrada, HashMap<String, ArrayList<String>> paises) {

		String linea;
		String[] datosPaises;
		String[] fronteras;
		ArrayList<String> paisesVecinos;

		try {
			BufferedReader fichero = new BufferedReader(new FileReader(nombreFicheroEntrada));
			while (fichero.ready()) {
				linea = fichero.readLine();
				datosPaises = linea.split(": ");
				fronteras = datosPaises[1].split(", ");
				paisesVecinos = new ArrayList<>();
				for(String f : fronteras) {
					paisesVecinos.add(f);
				}
				
				paises.put(datosPaises[0], paisesVecinos);
			}
			fichero.close();
		} catch (FileNotFoundException fnfe) {
			System.out.println("El archivo no se ha encontrado.");
		} catch (IOException ioe) {
			new RuntimeException("Error de entrada/salida.");
		}
	}
}
