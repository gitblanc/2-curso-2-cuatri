/**
 * 
 */
package levenshtein;

/**
 * @author UO285176
 *
 */
public class Levenshtein {
	int[][] d;
	String[] cad1;
	String[] cad2;

	public int distanciaLevenshtein(String cad1, String cad2) {
		int n = cad1.length();
		int m = cad2.length();
		if(n == 0 && m == 0) {
			return 0;
		}
		if(n == 0)
			return m;
		if(m == 0)
			return n;
		d = new int[n + 1][m + 1];
		this.cad1 = new String[cad1.length() + 1];
		this.cad2 = new String[cad2.length() + 1];
		this.cad1[0] = "";
		this.cad2[0] = "";
		int z = 1;
		for (String letra : cad1.split("")) {
			this.cad1[z] = letra;
			z++;
		}
		z = 1;
		for (String letra : cad2.split("")) {
			this.cad2[z] = letra;
			z++;
		}
		// Peor caso -> tener que insertar todas
		for (int x = 0; x <= n; x++)
			d[x][0] = x;

		for (int k = 0; k <= m; k++)
			d[0][k] = k;

		for (int i = 1; i <= n; i++) {
			for (int j = 1; j <= m; j++) {
				if (this.cad1[i].equals(this.cad2[j])) {
					d[i][j] = d[i - 1][j - 1];
				} else {
					int aux = Math.min(d[i - 1][j - 1], d[i][j - 1]);
					d[i][j] = 1 + Math.min(aux, d[i - 1][j]);
				}

			}
		}
		return d[n - 1][m - 1];
	}

	public void print() {
		System.out.print("\t");
		for(String s : this.cad2)
			System.out.print(s.toUpperCase() + "\t");
		System.out.println("\n");
		for (int i = 0; i < d.length; i++) {
			System.out.print(this.cad1[i].toUpperCase() + "\t");
			for (int j = 0; j < d[0].length; j++) {
				System.out.print(d[i][j] + "\t");
			}
			System.out.println("\n");
		}
	}
}
