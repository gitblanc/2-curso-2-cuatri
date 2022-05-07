package procesadoDeImagenes;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Random;

public class PromediadoImagen {

	private Imagen real_img, bad_img; // para almacenar las imagenes con patron bueno y malo (negativo del malo)
	private Imagen avg_img, half1_img, half2_img; // para almacenar los promedios del subconjunto 1 y 2
	private Imagen[] dataset; // almacena el conjunto de de imagenes generadas (buenas y malas)
	private int[] sol; // array que determina donde poner las imágenes 0->no asignada, 1->primer
						// subconjunto, 2->segundo subconjunto
	private int[] bestSol; // mejor solució
	private int width, height; // ancho y alto de las imágenes
	// backtracking variables
	private int counter; // contador de nodos en el arbol impl兤ito
	private double max_zncc; // donde almacenar el ZNCC final

	public final static int MAX_UNBALANCING = 1;

	/**
	 * Constructor
	 * 
	 * @real_path ruta del modelo de imagen "buena" (patró a encontrar) en disco
	 * @bad_path ruta del modelo de imagen "mala"
	 * @n_real numero de imagenes buenas (>= 1)
	 * @n_bad numero de imagenes "malas" (tiene que ser menor que las buenas)
	 * @s_noise standard deviation for noise
	 */
	public PromediadoImagen(String real_path, String bad_path, int n_real, int n_bad, double s_noise) {
		// crea el cojunto de imágenes a procesar
		assert (n_real >= 1) && (n_bad < n_real);

		// Cargando los patrones de referencia (buena y mala)
		this.real_img = new Imagen(real_path);
		this.bad_img = new Imagen(bad_path);
		this.width = this.real_img.getWidth();
		this.height = this.real_img.getHeight();

		// Se crean con conjunto de imagenes con un array ordenado aleatoriamente para
		// posicionar
		// las imagenes buenas y malas aleatoriamente
		int total_imgs = n_real + n_bad; // numero total de imágenes
		this.dataset = new Imagen[total_imgs];
		this.sol = new int[total_imgs]; // dóde se almacena la solució actual (combinació de asignaciones): 0->no
										// asignada, 1->primer subconjunto, 2->segundo subconjunto
		this.bestSol = new int[total_imgs]; // dóde se almacena la mejor solució
		int[] rand_index = this.randomIndexes(total_imgs); // array con las posiciones aleatorias
		Imagen hold_img; // imagen temporal
		int region = 0; // 0-arriba, 1-bajo, 2-izquierda, 3-derecha
		for (int i = 0; i < n_real; i++) { // imágenes buenas
			hold_img = new Imagen(this.width, this.height); // creació de la imagen
			hold_img.addSignal(this.real_img); // adir los valores de los p喆eles
			hold_img.suppressRegion(region); // suprimir una region
			hold_img.addNoise(s_noise); // adir ruido
			this.dataset[rand_index[i]] = hold_img; // incluir la imagne en una posció aleatorio de dataset
			if (region == 3)
				region = 0;
			else
				region++;
		}
		region = 0;
		for (int i = n_real; i < n_real + n_bad; i++) { // bucle para las imágenes malas
			hold_img = new Imagen(this.width, this.height);
			hold_img.addSignal(this.bad_img);
			hold_img.invertSignal();
			hold_img.suppressRegion(region);
			hold_img.addNoise(s_noise);
			this.dataset[rand_index[i]] = hold_img;
			if (region == 3)
				region = 0;
			else
				region++;
		}
	}

	/**
	 * Gener una array con valores de posiciones aleatorios
	 * 
	 * @param n longitud del array
	 * @return
	 */
	public int[] randomIndexes(int n) {
		List<Integer> list = new ArrayList<>();
		for (int i = 0; i < n; i++)
			list.add(i);
		Collections.shuffle(list);
		int[] array = new int[n];
		for (int i = 0; i < n; i++)
			array[i] = list.get(i);
		return array;
	}

	/**
	 * Almacena las imágenes generadas según la mejor solució encontrada
	 * 
	 * @out_dir directorio donde se almacenan las imágenes
	 */
	public void saveResults(String out_dir) {
		this.avg_img.save(out_dir + "/img_avg.png");
		this.half1_img.save(out_dir + "/img_half1_avg.png");
		this.half2_img.save(out_dir + "/img_half2_avg.png");
		for (int i = 0; i < this.dataset.length; i++) {
			this.dataset[i].save(out_dir + "/img_" + i + "_klass_" + this.bestSol[i] + ".png");
		}
	}

	/**
	 * @return devuelve el número de pasos requeridos por el algoritmo
	 */
	public int getCounter() {
		return counter;
	}

	/**
	 * Calcula el ZNCC entre los promedios de los dos subconjuntos de imágenes
	 * 
	 * @return el valor de ZNCC
	 */
	public double zncc() {
		return this.half1_img.zncc(this.half2_img);
	}

	/**
	 * Greedy algorithm: random instances for each half, the best one is the final
	 * solution
	 * 
	 * @n_tries numero de intentos aleatorios
	 */
	public void splitSubsetsGreedy(int n_tries) {
		this.counter = 0;
		this.max_zncc = -1;
		Random r = new Random();
		int[] x = new int[this.dataset.length];
		Imagen group1_pd = new Imagen(this.width, this.height);
		Imagen group2_pd = new Imagen(this.width, this.height);
		for (int tries = 0; tries < n_tries; tries++) {
			this.counter++;
			group1_pd = new Imagen(this.width, this.height);
			group2_pd = new Imagen(this.width, this.height);
			for (int i = 0; i < x.length; i++) {
				x[i] = r.nextInt(3);
				if (x[i] == 1)
					group1_pd.addSignal(this.dataset[i]);
				if (x[i] == 2)
					group2_pd.addSignal(this.dataset[i]);
			}
			if (group1_pd.zncc(group2_pd) > this.max_zncc) {
				this.max_zncc = group1_pd.zncc(group2_pd);// se actualiza el valor mejor de la correlación
				this.half1_img = group1_pd.copy();// se guardan los promedios mejores
				this.half2_img = group2_pd.copy();
				this.bestSol = x.clone();// se guarda el vector con la mejor solución
			}
		}
		this.avg_img = new Imagen(this.width, this.height);
		this.avg_img.addSignal(half1_img);
		this.avg_img.addSignal(half2_img);

	}

	/**
	 * Algoritmo backtracking con condición balanceo
	 * 
	 * @max_unbalancing: (condición de poda) determina la diferencia máxima en el
	 *                   número de imágenes entre los dos subconjuntos
	 */
	public void splitSubsetsBacktracking(int max_unbalancing) {
		this.counter = 1;
		this.max_zncc = -1;
		int level = 0;
		backtrackingPodaAux(level, max_unbalancing, 0, 0, 0);
	}

	private void backtrackingPodaAux(int level, int max_unbalancing, int cont0, int cont1, int cont2) {
		if (level == dataset.length) {
			Random r = new Random();
			Imagen group1_pd = new Imagen(width, height);
			Imagen group2_pd = new Imagen(width, height);
			for (int i = 0; i < sol.length; i++) {
				this.counter++;
				sol[i] = r.nextInt(3);
				if (sol[i] == 1)
					group1_pd.addSignal(this.dataset[i]);
				if (sol[i] == 2)
					group2_pd.addSignal(this.dataset[i]);
			}
			if (group1_pd.zncc(group2_pd) > this.max_zncc) {
				this.max_zncc = group1_pd.zncc(group2_pd);// se actualiza el valor mejor de la correlación
				this.half1_img = group1_pd.copy();// se guardan los promedios mejores
				this.half2_img = group2_pd.copy();
				this.bestSol = sol.clone();// se guarda el vector con la mejor solución
			}

			this.avg_img = new Imagen(this.width, this.height);
			this.avg_img.addSignal(half1_img);
			this.avg_img.addSignal(half2_img);
		} else {
			if (Math.abs(cont0 - cont1) < max_unbalancing && Math.abs(cont0 - cont2) < max_unbalancing) {
				// se van poniendo las soluciones
				this.sol[level] = 0;
				this.counter++;
				backtrackingPodaAux(level + 1, MAX_UNBALANCING, cont0 + 1, cont1, cont2);// los tres nodos de cada
																							// nivel
			}
			if (Math.abs(cont1 - cont2) < max_unbalancing) {
				this.sol[level] = 1;
				this.counter++;
				backtrackingPodaAux(level + 1, MAX_UNBALANCING, cont0, cont1 + 1, cont2);
			}
			this.sol[level] = 2;
			this.counter++;
			backtrackingPodaAux(level + 1, MAX_UNBALANCING, cont0, cont1, cont2 + 1);
			level++;
			backtrackingPodaAux(level, MAX_UNBALANCING, cont0, cont1, cont2);
		}

	}

	/**
	 * Algoritmo backtracking sin condición de balanceo.
	 */
	public void splitSubsetsBacktracking() {
		this.counter = 1;
		this.max_zncc = -1;
		int level = 0;
		backtrackingAux(level);

	}

	private void backtrackingAux(int level) {
		if (level == dataset.length) {
			Imagen group1_pd = new Imagen(width, height);
			Imagen group2_pd = new Imagen(width, height);

			for (int i = 0; i < sol.length; i++) {
				if (sol[i] == 1)
					group1_pd.addSignal(this.dataset[i]);
				if (sol[i] == 2)
					group2_pd.addSignal(this.dataset[i]);
			}
			if (group1_pd.zncc(group2_pd) > this.max_zncc) {
				this.max_zncc = group1_pd.zncc(group2_pd);// se actualiza el valor mejor de la correlación
				this.half1_img = group1_pd.copy();// se guardan los promedios mejores
				this.half2_img = group2_pd.copy();
				this.bestSol = sol.clone();// se guarda el vector con la mejor solución
			}

			this.avg_img = new Imagen(this.width, this.height);
			this.avg_img.addSignal(half1_img);
			this.avg_img.addSignal(half2_img);
		} else {
			// se van poniendo las soluciones
			this.sol[level] = 0;
			this.counter++;
			backtrackingAux(level + 1);// los tres nodos de cada nivel
			this.sol[level] = 1;
			this.counter++;
			backtrackingAux(level + 1);
			this.sol[level] = 2;
			this.counter++;
			backtrackingAux(level + 1);
			level++;
			// Complejidad
			// a = 3, b = 1, k = 1(para la mayoria de casos) -> a > b^k, a^(n/b) -> O(n) =
			// 3^n
		}
	}

	// -------------EJERCICIOS EXAMEN--------------
	/**
	 * Algoritmo backtracking sin condición de balanceo.
	 */
	public void splitSubsetsBacktrackingExamen() {
		this.counter = 1;
		this.max_zncc = -1;
		int level = 0;
		backtrackingAux(level);

	}

	private void backtrackingAuxExamen(int level) {
		if (level == dataset.length) {
			Imagen group1_pd = new Imagen(width, height);
			Imagen group2_pd = new Imagen(width, height);

			for (int i = 0; i < sol.length; i++) {
				if (sol[i] == 1)
					group1_pd.addSignal(this.dataset[i]);
				if (sol[i] == 2)
					group2_pd.addSignal(this.dataset[i]);
				if (sol[i] == 3) {
					group1_pd.addSignal(this.dataset[i]);
					group2_pd.addSignal(this.dataset[i]);
				}

			}
			if (group1_pd.zncc(group2_pd) > this.max_zncc) {
				this.max_zncc = group1_pd.zncc(group2_pd);// se actualiza el valor mejor de la correlación
				this.half1_img = group1_pd.copy();// se guardan los promedios mejores
				this.half2_img = group2_pd.copy();
				this.bestSol = sol.clone();// se guarda el vector con la mejor solución
			}

			this.avg_img = new Imagen(this.width, this.height);
			this.avg_img.addSignal(half1_img);
			this.avg_img.addSignal(half2_img);
		} else {
			// se van poniendo las soluciones
			this.sol[level] = 0;
			this.counter++;
			backtrackingAuxExamen(level + 1);// los tres nodos de cada nivel
			this.sol[level] = 1;
			this.counter++;
			backtrackingAuxExamen(level + 1);
			this.sol[level] = 2;
			this.counter++;
			backtrackingAuxExamen(level + 1);
			this.sol[level] = 3;
			this.counter++;
			backtrackingAuxExamen(level + 1);
			level++;
			// Complejidad
			// a = 3, b = 1, k = 1(para la mayoria de casos) -> a > b^k, a^(n/b) -> O(n) =
			// 3^n
		}
	}

	/**
	 * Algoritmo backtracking con condición balanceo
	 * 
	 * @max_unbalancing: (condición de poda) determina la diferencia máxima en el
	 *                   número de imágenes entre los dos subconjuntos
	 */
	public void splitSubsetsBacktrackingExamen(int max_unbalancing) {
		this.counter = 1;
		this.max_zncc = -1;
		int level = 0;
		backtrackingPodaAux(level, max_unbalancing, 0, 0, 0);
	}

	private void backtrackingPodaAuxExamen(int level, int max_unbalancing, int cont0, int cont1, int cont2, int cont3) {
		if (level == dataset.length) {
			Imagen group1_pd = new Imagen(width, height);
			Imagen group2_pd = new Imagen(width, height);
			for (int i = 0; i < sol.length; i++) {
				this.counter++;
				if (sol[i] == 1)
					group1_pd.addSignal(this.dataset[i]);
				if (sol[i] == 2)
					group2_pd.addSignal(this.dataset[i]);
				if (sol[i] == 3) {
					group1_pd.addSignal(this.dataset[i]);
					group2_pd.addSignal(this.dataset[i]);
				}
			}
			if (group1_pd.zncc(group2_pd) > this.max_zncc) {
				this.max_zncc = group1_pd.zncc(group2_pd);// se actualiza el valor mejor de la correlación
				this.half1_img = group1_pd.copy();// se guardan los promedios mejores
				this.half2_img = group2_pd.copy();
				this.bestSol = sol.clone();// se guarda el vector con la mejor solución
			}

			this.avg_img = new Imagen(this.width, this.height);
			this.avg_img.addSignal(half1_img);
			this.avg_img.addSignal(half2_img);
		} else {
			if (Math.abs(cont0 - cont1) < max_unbalancing && Math.abs(cont0 - cont2) < max_unbalancing
					&& Math.abs(cont0 - cont3) < max_unbalancing) {
				// se van poniendo las soluciones
				this.sol[level] = 0;
				this.counter++;
				backtrackingPodaAuxExamen(level + 1, MAX_UNBALANCING, cont0 + 1, cont1, cont2, cont3);// los tres nodos
																										// de cada
				// nivel
			}
			if (Math.abs(cont1 - cont2) < max_unbalancing && Math.abs(cont1 - cont3) < max_unbalancing) {
				this.sol[level] = 1;
				this.counter++;
				backtrackingPodaAuxExamen(level + 1, MAX_UNBALANCING, cont0, cont1 + 1, cont2, cont3);
			}
			if (Math.abs(cont2 - cont3) < max_unbalancing) {
				this.sol[level] = 2;
				this.counter++;
				backtrackingPodaAuxExamen(level + 1, MAX_UNBALANCING, cont0, cont1, cont2 + 1, cont3);
			}
			this.counter++;
			this.sol[level] = 3;
			backtrackingPodaAuxExamen(level, MAX_UNBALANCING, cont0, cont1, cont2, cont3 + 1);
			level++;
		}

	}

	/**
	 * Greedy algorithm: random instances for each half, the best one is the final
	 * solution
	 * 
	 * @n_tries numero de intentos aleatorios
	 */
	public void splitSubsetsGreedySinBalanceoExamen(int n_tries) {
		this.counter = 0;
		this.max_zncc = -1;
		Random r = new Random();
		int[] x = new int[this.dataset.length];
		Imagen group1_pd = new Imagen(this.width, this.height);
		Imagen group2_pd = new Imagen(this.width, this.height);
		for (int tries = 0; tries < n_tries; tries++) {
			this.counter++;
			group1_pd = new Imagen(this.width, this.height);
			group2_pd = new Imagen(this.width, this.height);
			for (int i = 0; i < x.length; i++) {
				x[i] = r.nextInt(4);// del 0 al 3
				if (x[i] == 1)
					group1_pd.addSignal(this.dataset[i]);
				if (x[i] == 2)
					group2_pd.addSignal(this.dataset[i]);
				if (x[i] == 3) {
					group1_pd.addSignal(this.dataset[i]);
					group2_pd.addSignal(this.dataset[i]);
				}
			}
			if (group1_pd.zncc(group2_pd) > this.max_zncc) {
				this.max_zncc = group1_pd.zncc(group2_pd);// se actualiza el valor mejor de la correlación
				this.half1_img = group1_pd.copy();// se guardan los promedios mejores
				this.half2_img = group2_pd.copy();
				this.bestSol = x.clone();// se guarda el vector con la mejor solución
			}
		}
		this.avg_img = new Imagen(this.width, this.height);
		this.avg_img.addSignal(half1_img);
		this.avg_img.addSignal(half2_img);

	}

	// también se le pasa el parámetro maximo balanceo
//	public void splitSubsetsGreedyConBalanceoExamen(int n_tries, int max_unbalancing, int cont3, int cont1, int cont2) {
//		this.counter = 0;
//		this.max_zncc = -1;
//		Random r = new Random();
//		int[] x = new int[this.dataset.length];
//		Imagen group1_pd = new Imagen(this.width, this.height);
//		Imagen group2_pd = new Imagen(this.width, this.height);
//		for (int tries = 0; tries < n_tries; tries++) {
//			this.counter++;
//			group1_pd = new Imagen(this.width, this.height);
//			group2_pd = new Imagen(this.width, this.height);
//			for (int i = 0; i < x.length; i++) {
//				x[i] = r.nextInt(4);// del 0 al 3
//				if (x[i] == 1 && cont1 < max_unbalancing) {
//					group1_pd.addSignal(this.dataset[i]);
//					cont1++;
//				}
//				if (x[i] == 2 && cont2 < max_unbalancing) {
//					group2_pd.addSignal(this.dataset[i]);
//					cont2++;
//				}
//				if (x[i] == 3 && cont3 < max_unbalancing) {
//					group1_pd.addSignal(this.dataset[i]);
//					group2_pd.addSignal(this.dataset[i]);
//					cont3++;
//				}
//			}
//			if (group1_pd.zncc(group2_pd) > this.max_zncc) {
//				this.max_zncc = group1_pd.zncc(group2_pd);// se actualiza el valor mejor de la correlación
//				this.half1_img = group1_pd.copy();// se guardan los promedios mejores
//				this.half2_img = group2_pd.copy();
//				this.bestSol = x.clone();// se guarda el vector con la mejor solución
//			}
//		}
//		this.avg_img = new Imagen(this.width, this.height);
//		this.avg_img.addSignal(half1_img);
//		this.avg_img.addSignal(half2_img);
//		//System.out.println(cont1 + "" + cont2 + "" + cont3);
//	}

	public void splitSubsetsGreedyConBalanceoExamen(int n_tries, int max_unbalancing) {
		this.counter = 0;
		this.max_zncc = -1;
		Random r = new Random();
		int[] x = new int[this.dataset.length];
		Imagen group1_pd = new Imagen(this.width, this.height);
		Imagen group2_pd = new Imagen(this.width, this.height);
		for (int tries = 0; tries < n_tries; tries++) {
			this.counter++;
			group1_pd = new Imagen(this.width, this.height);
			group2_pd = new Imagen(this.width, this.height);
			for (int i = 0; i < x.length; i++) {
				x[i] = r.nextInt(4);
				if (x[i] == 1)
					group1_pd.addSignal(this.dataset[i]);
				if (x[i] == 2)
					group2_pd.addSignal(this.dataset[i]);
				if (x[i] == 3) {
					group1_pd.addSignal(this.dataset[i]);
					group2_pd.addSignal(this.dataset[i]);
				}
			}
			if (group1_pd.zncc(group2_pd) < max_unbalancing && group1_pd.zncc(group2_pd) > this.max_zncc) {
				this.max_zncc = group1_pd.zncc(group2_pd);// se actualiza el valor mejor de la correlación
				this.half1_img = group1_pd.copy();// se guardan los promedios mejores
				this.half2_img = group2_pd.copy();
				this.bestSol = x.clone();// se guarda el vector con la mejor solución
			}
		}
		this.avg_img = new Imagen(this.width, this.height);
		this.avg_img.addSignal(half1_img);
		this.avg_img.addSignal(half2_img);

	}
}
