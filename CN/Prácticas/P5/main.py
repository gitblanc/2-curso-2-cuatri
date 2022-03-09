import scipy.optimize as op
import numpy as np
import matplotlib.pyplot as plt


# Ejercicio1
# argumentos de entrada
# f, x0, x1, tol-cota de error, iteraciones máxima
def secante(f, x0, x1, tol, maxiter=100):
    r = np.zeros(3)  # array que guarda las raíces
    i = 0
    while i < maxiter and abs(x1 - x0) >= tol:
        x2 = x1 - (f(x1) * (x1 - x0)) / (f(x1) - f(x0))
        r[0] = x0
        r[1] = x1
        r[2] = x2
        x0 = x1
        x1 = x2
        i += 1
    return r, i






f1 = lambda x: x ** 5 - 3 * x ** 2 + 1.6
a = -1.
b = 1.5
maxiter = 100
tol = 1.e-6
raices, k = secante(f1, a, b, tol, maxiter)
for i in range(len(raices)):
    print(raices[i] + k)
    i += 1
