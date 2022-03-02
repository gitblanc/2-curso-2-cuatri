import numpy as np
import matplotlib.pyplot as plt


# f funcion lambda
# intervalo a b
# numero de intervalos en el que dividiremos a y b -> n
def busquedaIncremental(f, a, b, n):
    matriz = np.zeros((n, 2))
    intervalo = np.zeros(2)
    i = 0
    while i < n:
        c = (a + b) / 2
        if f(a) * f(c) < 0:
            b = c
            matriz[i][0] = a
            matriz[i][1] = b
            i += 1
        elif f(c) * f(b) < 0:
            a = c
            matriz[i][0] = a
            matriz[i][1] = b
            i += 1
        else:
            break

    return matriz


f = lambda x: x ** 5 - 3 * x ** 2 + 1.6
print(busquedaIncremental(f, -1, 1.5, 25))
