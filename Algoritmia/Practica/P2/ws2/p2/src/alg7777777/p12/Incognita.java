package alg7777777.p12;

public class Incognita {

	public static long bucle6(int n)
	{
		long cont = 0;
		for (int i = 1; i <= n; i++)
			for (int j = 1; j <= i; j++)
				for (int k = 1; k <= j; k++)
					cont++;
		return cont;
		//COMPLEJIDAD N^3	
	}

	public static void main(String arg[]) {
		long c = 0;
		long t1, t2;
		int nVeces = Integer.parseInt(arg[0]);

		for (int n = 8; n <= 100_000; n *= 2) {//log n
			t1 = System.currentTimeMillis();

			if(n >= 128) {
				nVeces = 1;
			}
			for (int repeticiones = 1; repeticiones <= nVeces; repeticiones++) {
				c += bucle6(n);
			}

			t2 = System.currentTimeMillis();

			System.out.println(c + "**n=" + n + "**TIEMPO=" + (t2 - t1)
					+ "**nVeces=" + nVeces);

		} // for

	} // main
} // class