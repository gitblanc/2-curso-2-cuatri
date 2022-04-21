package procesadoDeImagenes;

public class PromediadoImagenBench {

	// Ajustes del banco de pruebas
	private static String REAL_IMG = "img/einstein_1_256.png";
	private static String BAD_IMG = "img/einstein_1_256.png";
	private static String OUT_DIR_G = "out_g";
	private static String OUT_DIR_B = "out_bt";
	private static int N_IMGS = 12;
	private static double PORCENTAJE_BAD = 25; // %
	private static double S_NOISE = 5.0;// PONER 5.0 // Nivel de ruido - desvici est疣dar de una distrubuci Gaussiana

	public static void main(String[] args) {

		int n_real, n_bad;
		PromediadoImagen img_avger;

		// Generaci y testeo de un conjunto de im疊enes
		n_bad = (int) ((PORCENTAJE_BAD / 100.) * N_IMGS);
		n_real = N_IMGS - n_bad;
		img_avger = new PromediadoImagen(REAL_IMG, BAD_IMG, n_real, n_bad, S_NOISE);

		System.out.print("TESTING VORAZ:\n");
		
		long t1 = System.currentTimeMillis();
		img_avger.splitSubsetsGreedy(N_IMGS);// num intentos = num imágenes
		long t2 = System.currentTimeMillis();
		
		System.out.printf("  -ZNCC: %f\n", img_avger.zncc());
		System.out.printf("  -Contador: %d\n", img_avger.getCounter());
		System.out.println("TIEMPO: " + (t2-t1));
		img_avger.saveResults(OUT_DIR_G);

		
		System.out.print("TESTING BACKTRACKING CON BALANCEO:\n");
		
		t1 = System.currentTimeMillis();
		img_avger.splitSubsetsBacktracking(1);
		t2 = System.currentTimeMillis();
		
		System.out.printf("  -ZNCC: %f\n", img_avger.zncc());
		System.out.printf("  -Contador: %d\n", img_avger.getCounter());
		System.out.println("TIEMPO: " + (t2-t1));
		img_avger.saveResults(OUT_DIR_B);
		
		
		
		System.out.print("TESTING BACKTRACKING SIN BALANCEO:\n");
		
		t1 = System.currentTimeMillis();
		img_avger.splitSubsetsBacktracking();
		t2 = System.currentTimeMillis();
		
		System.out.printf("  -ZNCC: %f\n", img_avger.zncc());
		System.out.printf("  -Contador: %d\n", img_avger.getCounter());
		System.out.println("TIEMPO: " + (t2-t1));
		img_avger.saveResults(OUT_DIR_B);

		// Medidas
		// TODO
	}

}
