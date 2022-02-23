package alg7777777.p12;

public class Bucle1 {

	public static long bucle1(int n) {
		long cont = 0;
		for (int i = 1; i <= n; i++)// n
			for (int j = 1; j <= n; j *= 2)// log n
				cont++;
		return cont;
	}

	public static void main(String arg[]) {
		long t1, t2;
		int nVeces = Integer.parseInt(arg[0]);

		System.out.println("n\ttiempo\tcontador");
		for (int n = 8; n <= 100_000; n *= 2) {// log n
			if (n >= 128) {
				nVeces = 100000;
			}
			long c = 0;
			t1 = System.currentTimeMillis();

			for (int repeticiones = 1; repeticiones <= nVeces; repeticiones++) {// n
				c += bucle1(n);// ---llama bucle
			}

			t2 = System.currentTimeMillis();

			System.out.println(n + "\t" + (t2 - t1) + "\t" + c);

		} // for

	} // main
} // class