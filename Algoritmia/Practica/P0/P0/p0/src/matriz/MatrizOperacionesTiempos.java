package matriz;

public class MatrizOperacionesTiempos {

	public static void main(String[] args) {

		for (int i = 3; i < 24577; i *= 2) {
			MatrizOperaciones matrix = new MatrizOperaciones(i, 1, 4);
			
			long inicioTiempo = System.currentTimeMillis();
			
			matrix.sumarDiagonal1();
			long finTiempo1 = System.currentTimeMillis();
			
			matrix.sumarDiagonal2();
			long finTiempo2 = System.currentTimeMillis();
			
			System.out.println("Suma diagonal1: " + (finTiempo1 -inicioTiempo));
			System.out.println("---");
			System.out.println("Suma diagonal2: " + (finTiempo2 -inicioTiempo));
			System.out.println(i);
			System.out.println("--------------------");
			
		}
	}

}
