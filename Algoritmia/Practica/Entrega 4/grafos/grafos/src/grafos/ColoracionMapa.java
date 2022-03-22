/**
 * 
 */
package grafos;

import java.util.ArrayList;
import java.util.HashMap;

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
		colorear();
	}

	public void imprimirSolucion() {

		for (String pais : solucion.keySet()) {
			System.out.println(pais + "(" + solucion.get(pais) + ")\n");
			for (String adyacente : paises.get(pais)) {
				System.out.println("\t" + adyacente + "(" + solucion.get(adyacente) + ")\n");
			}
		}

	}

	private void sacarPaisesYFronteras() {
		FileUtil.readCountries("datos/fronteras.txt", paises);

	}

	private void sacarColores() {
		FileUtil.readColors("datos/colores.txt", this.colores);
	}

	private void colorear() {
		for (String pais : paises.keySet()) {//O(n)
			for (String color : colores) {//O(1)
				if (paises.get(pais).equals("NO")) {
					solucion.put(pais, color);
					break;
				} else if (colorUsado(pais, color)) {//O(1)
					solucion.put(pais, color);
					break;
				}
			}
		}
	}

	private boolean colorUsado(String pais, String color) {//O(1)
		for (String adyacente : paises.get(pais)) {
			if (solucion.get(adyacente) != null && solucion.get(adyacente).equals(color))
				return false;
		}
		return true;
	}
}
