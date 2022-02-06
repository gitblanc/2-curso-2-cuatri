package matriz;

public class Main {

	public static void main(String[] args) {
		MatrizOperaciones matrix = new MatrizOperaciones(10, 1, 4);
		matrix.escribir();
		System.out.println("Tamaño: " + matrix.getTam());
		matrix.escribir();
		System.out.println("Diagonal1: " + matrix.sumarDiagonal1());
		System.out.println("Diagonal2: " + matrix.sumarDiagonal2());
	    matrix.recorrerCamino(2, 2);
	    System.out.println("Post recorrido");
	    matrix.escribir();
	    System.out.println("Fin del camino");
	    
	}

}
