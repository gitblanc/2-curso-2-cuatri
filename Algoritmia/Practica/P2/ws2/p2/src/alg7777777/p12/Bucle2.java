package alg7777777.p12;

public class Bucle2 {

	public static long bucle2(int n)
	{
		long cont = 0;
		for (int i = 1; i <= n; i++)//n
			for (int j = 1; j <= n; j++)//n
				cont++;//1
		return cont;
		//	COMPLEJIDAD = n^2
	}

	public static void main(String arg[]) {
		long t1, t2;
		int nVeces = Integer.parseInt(arg[0]);

		for (int n = 8; n <= 66000; n *= 2) {
			if(n > 128) {
				nVeces = 100000;
			}
			long c = 0;
			t1 = System.currentTimeMillis();

			for (int repeticiones = 1; repeticiones <= nVeces; repeticiones++) {
				c += bucle2(n);
			}

			t2 = System.currentTimeMillis();

			System.out.println(c + "**n=" + n + "**TIEMPO=" + (t2 - t1)
					+ "**nVeces=" + nVeces);
		} // for

	} // main
} // class