package clases;

/**
 * Este programa sirve para ordenar n elementos con un algoritmo de los "malos"
 * (cuadrático)· es la SELECCION
 */
public class Seleccion extends Vector {
	public Seleccion(int nElementos) {
		super(nElementos);
	}

	/**
	 * Ordenación por selección
	 */
	@Override
	public void ordenar() {
		int tam = elements.length;
		int posmin;
		for (int i = 0; i < (tam - 1); i++) {
			posmin = i;
			for (int j = i + 1; j < tam; j++) {
				if (elements[j] < elements[posmin])
					posmin = j;
			}
			intercambiar(i, posmin);
		}
	}

	@Override
	public String getNombre() {
		return "Selección";
	}
}
