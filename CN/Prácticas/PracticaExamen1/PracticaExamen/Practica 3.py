import numpy as np
import matplotlib.pyplot as plt

"""
Bolzano
"""
# Ejercicio 1
"""
Función para separar las raíces del teorema de Bolzano
"""


# f funcion lambda
# intervalo a b
# numero de intervalos en el que dividiremos a y b -> n
def busquedaIncremental(f, a, b, n):
    x = np.linspace(a, b, n + 1)
    intervalos = np.zeros((n, 2))
    y = f(x)
    contador = 0

    for i in range(n):
        if (y[i] * y[i + 1] < 0):
            intervalos[contador, :] = x[i:i + 2]
            contador += 1
    intervalos = intervalos[:contador, :]
    return intervalos


f1 = lambda x: x ** 5 - 3 * x ** 2 + 1.6
a = -1.
b = 1.5
n = 25

intervalos = busquedaIncremental(f1, a, b, n)
print('Intervalos que contienen raíces de f1\n')
print(intervalos)

# %% Ejemplo 2
f2 = lambda x: (x + 2) * np.cos(2 * x)
a = 0.
b = 10.
n = 100

intervalos = busquedaIncremental(f2, a, b, n)
print('\nIntervalos que contienen raíces de f2\n')
print(intervalos)

# Ejercicio 2
"""
Bisección
"""


def biseccion(f, a, b, tol=1.e-6, n=100):
    if f(a) * f(b) > 0.0:
        print('La función tiene el mismo signo en los extremos')

    for i in range(n):
        m = 0.5 * (a + b)

        if f(a) * f(m) < 0:
            b = m
        elif f(m) * f(b) < 0:
            a = m
        else:
            break

        if b - a < tol:
            break

    return m, i + 1  # i+1 porque el cero también cuenta


f = lambda x: x ** 5 - 3 * x ** 2 + 1.6  # definimos la función f

a = -0.7
b = -0.6
sol1, i1 = biseccion(f, a, b)
print(sol1, i1)

a = 0.8
b = 0.9
sol2, i2 = biseccion(f, a, b)
print(sol2, i2)

a = 1.2
b = 1.3
sol3, i3 = biseccion(f, a, b)
print(sol3, i3)

# import scipy.optimize as op
# r = [-0.7, 0.8, 1.2]
# sol = op.newton(f,r)
# print(sol)

# Ejercicio 3
"""
Newton
"""


def newton(f, df, x0, tol=1.e-6, n=100):
    for i in range(n):
        x1 = x0 - f(x0) / df(x0)

        if abs(x1 - x0) < tol:
            break
        x0 = x1

    return x1, i + 1  # i+1 porque el cero también cuenta


f = lambda x: x ** 5 - 3 * x ** 2 + 1.6  # definimos la función f
df = lambda x: 5 * x ** 4 - 6. * x  # definimos la derivada de la función f

x0 = -0.7
sol1, i1 = newton(f, df, x0)
print(sol1, i1)

x0 = 0.8
sol2, i2 = newton(f, df, x0)
print(sol2, i2)

x0 = 1.2
sol3, i3 = newton(f, df, x0)
print(sol3, i3)

# import scipy.optimize as op
# r = [-0.7, 0.8, 1.2]
# sol = op.newton(f,r)
# print(sol)
