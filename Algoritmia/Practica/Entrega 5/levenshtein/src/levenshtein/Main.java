package levenshtein;

public class Main {

	public static void main(String[] args) {
		Levenshtein l = new Levenshtein();
		System.out.println("Distancia: " + l.distanciaLevenshtein(args[0], args[1]));
		l.print();

	}

}
