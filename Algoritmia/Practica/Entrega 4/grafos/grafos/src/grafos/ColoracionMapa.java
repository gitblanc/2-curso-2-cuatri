/**
 * 
 */
package grafos;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 * @author UO285176
 *
 */
public class ColoracionMapa {
	private HashMap<String, ArrayList<String>> paises = new HashMap<>();// num paises-> tamaño del problema
	private HashMap<String, String> solucion = new HashMap<>();// lo que se devuelve, países y colores
	private String[] colores = new String[12];

	public ColoracionMapa() {
		sacarColores();
		sacarPaisesYFronteras();
	}

	private void sacarPaisesYFronteras() {
		FileUtil.readCountries("datos/fronteras.txt", paises);

	}

	private void sacarColores() {
		FileUtil.readColors("datos/colores.txt", this.colores);
	}

	public void colorear() {
		int i = 0;
		ArrayList<String> adyacentes;
		for (String key : paises.keySet()) {
			adyacentes = paises.get(key);
			for (String adyacente : adyacentes) {
				if (adyacente == "NO") {
					solucion.put(key, colores.get(i));
				}
				buscarColorNodo();
			}
		}
	}

	public int buscarColorNodo(int i) {
		colores[i] = colores[i]+1;
	}
}
