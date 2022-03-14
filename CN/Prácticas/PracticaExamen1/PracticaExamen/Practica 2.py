import numpy as np
import matplotlib.pyplot as plt

"""
np.polyval nos permite calcular el valor de un polinomio en uno o varios puntos
"""

# Ejercicio 1
"""
Algoritmo de Horner (Ruffini) para un punto como parámetro
"""


def horner(p, x0):
    n = len(p)
    q = np.zeros(n)
    q[0] = p[0]
    for i in range(1, n):
        q[i] = q[i - 1] * x0 + p[i]
    cociente = q[:-1]
    resto = q[-1]
    return cociente, resto


p0 = np.array([1., 2, 1])
p1 = np.array([1, -1, 2, -3, 5, -2])
p2 = np.array([1., -1, -1, 1, -1, 0, -1, 1])
x0 = 1
x1 = 1.
x2 = -1.

c, r = horner(p0, x0)
print('Coeficientes de Q = ', c)
print('P0(1) = ', r)

print('\n')
c, r = horner(p1, x1)
print('Coeficientes de Q = ', c)
print('P1(1) = ', r)

print('\n')

c, r = horner(p2, x2)
print('Coeficientes de Q = ', c)
print('P2(-1) = ', r)

# Ejercicio 2
"""
Algoritmo de Horner (Ruffini) para un vector como parámetro
"""


def hornerVect(p, vect):
    n = len(p)
    q = np.zeros(n)
    y = np.zeros_like(vect)

    for i in range(len(vect)):
        x0 = vect[i]
        q[0] = p[0]
        for j in range(1, n):  # como el otro horner(1)
            q[j] = q[j - 1] * x0 + p[j]
        y[i] = q[-1]
    return y


p = np.array([1, -1, 2, -3, 5, -2])

x = np.linspace(-1, 1)

y = hornerVect(p, x)
plt.plot(x, y)
plt.plot(x, 0 * x, 'k')
plt.title('P')
plt.show()

r = np.array([1., -1, -1, 1, -1, 0, -1, 1])

x = np.linspace(-1, 1)

y = hornerVect(r, x)
plt.plot(x, y)
plt.plot(x, 0 * x, 'k')
plt.title('R')
plt.show()

# Ejercicio 3
"""
Horner para calcular las derivadas sucesivas de un polinomio en un punto
"""


def dersuc(p, x0):
    derivadas = np.zeros_like(p)

    c = np.copy(p)
    factorial = 1.
    for k in range(len(c)):
        c, r = horner(c, x0)  # usamos la función del ej 1
        derivadas[k] = r * factorial
        factorial *= k + 1
    return derivadas


p = np.array([1., -1, 2, -3, 5, -2])
r = np.array([1., -1, -1, 1, -1, 0, -1, 1])
x0 = 1.
x1 = -1.

print('Derivadas sucesivas de P en x0 = 1')
print(dersuc(p, x0))
print('\n')
print('Derivadas sucesivas de R en x1 = -1')
print(dersuc(r, x1))
