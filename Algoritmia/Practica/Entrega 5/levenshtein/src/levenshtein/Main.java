package levenshtein;

public class Main {

	public static void main(String[] args) {
		Levenshtein l = new Levenshtein();
		System.out.println("Distancia: " + l.distanciaLevenshtein(args[0], args[1]));
		l.print();
		
		for (int i = 100; i < 100000000; i *= 2) {
			String n = l.generateRandomString();// se le pasa el tama�o que tendr� la cadena n
			String m = l.generateRandomString();// se le pasa el tama�o que tendr� la cadena m
			
			long tinicio = System.currentTimeMillis();
			l.distanciaLevenshtein(n, m);
			long tfinal = System.currentTimeMillis();
			System.out.println("TAMA�O= " + i + " , TIEMPO = " + (tfinal - tinicio));
		}
	}

}
