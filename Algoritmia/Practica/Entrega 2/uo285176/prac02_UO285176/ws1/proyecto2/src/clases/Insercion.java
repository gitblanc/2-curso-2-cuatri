package clases;

/**
 * Este programa sirve para ordenar n elementos con un algoritmo de los "malos"
 * (cuadrático)· Es la INSERCIÓN DIRECTA
 */
public class Insercion extends Vector {

	public Insercion(int nElementos) {
		super(nElementos);
	}

	/**
	 * Ordenación por inserción directa
	 */
	@Override
	public void ordenar() {
		for (int i = 0; i < elements.length; i++) {
			int current = elements[i];
			int j = i - 1;
			while (j >= 0 && current < elements[j]) {
				elements[j+1] = elements[j];
				j -=1;
			}
			elements[j+1] = current;
		}
	}

	@Override
	public String getNombre() {
		return "Inserci�n directa";
	}
}
