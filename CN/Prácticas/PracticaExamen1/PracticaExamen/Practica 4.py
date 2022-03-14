import numpy as np
import matplotlib.pyplot as plt
import scipy.optimize as op
import sympy as sym

# Ejercicio 1
"""
Secante
"""


def secante(f, x0, x1, tol=1.0e-6, n=200):
    for i in range(n):
        x2 = x1 - f(x1) * (x1 - x0) / (f(x1) - f(x0))
        if abs(x2 - x1) < tol:
            break
        x0 = x1
        x1 = x2

    return x2, i + 1  # i+1 porque el cero también cuenta


f = lambda x: x ** 5 - 3 * x ** 2 + 1.6  # definimos la función f
r = np.zeros(3)

tol = 1.e-6;
maxiter = 100

x0 = -0.7;
x1 = -0.6
r[0], i1 = secante(f, x0, x1, tol, maxiter)
print(r[0], i1)

x0 = 0.8;
x1 = 0.9
r[1], i2 = secante(f, x0, x1, tol, maxiter)
print(r[1], i2)

x0 = 1.2;
x1 = 1.3
r[2], i3 = secante(f, x0, x1, tol, maxiter)
print(r[2], i3)

plt.figure()
x = np.linspace(-1, 1.5)
plt.plot(x, f(x), x, 0 * x, 'k', r, r * 0, 'ro')
plt.show()

# Ejercicio 2
"""
Calcular los extremos relativos (máximos y mínimos), es decir, las raíces de f'
"""
# %%--------------------------------------------------------------------------
x = sym.Symbol('x', real=True)

f_sim = x ** 2 + sym.log(2 * x + 7) * sym.cos(3 * x) + 0.1
df_sim = sym.diff(f_sim, x)  # Derivada de f
d2f_sim = sym.diff(df_sim, x)  # Derivada segunda

f = sym.lambdify([x], f_sim, 'numpy')
df = sym.lambdify([x], df_sim, 'numpy')
d2f = sym.lambdify([x], d2f_sim, 'numpy')
# %%--------------------------------------------------------------------------
a = -1.
b = 3.
x = np.linspace(a, b, 1000)

plt.figure()
plt.plot(x, df(x))
plt.plot([a, b], [0, 0], 'k-')
plt.title('Función derivada de f')
plt.show()
# %%--------------------------------------------------------------------------
x0 = np.array([-1., 0., 1., 2., 3.])
x1 = op.newton(df, x0, tol=1.e-6, maxiter=100)
mini = x1[d2f(x1) > 0]
maxi = x1[d2f(x1) < 0]
# --------------------------------------------------------------------------
a = -2.
b = 4.
x = np.linspace(a, b, 1000)

plt.figure()
plt.plot(x, f(x), label='Función f')
plt.title('Función f')
plt.plot([a, b], [0, 0], 'k-')
plt.plot(mini, f(mini), 'go')
plt.plot(maxi, f(maxi), 'ro')
plt.show()
print('EXTREMOS')
print(x1)
# %%--------------------------------------------------------------------------
a = -1.
b = 4.
x = np.linspace(a, b, 1000)

plt.figure()
plt.plot(x, d2f(x))
plt.plot([a, b], [0, 0], 'k-')
plt.title('Función derivada segunda de f')
plt.show()
# %%--------------------------------------------------------------------------
x0 = np.array([-0.5, 0.5, 1.5, 2.5, 3.5])
x1 = op.newton(d2f, x0, tol=1.e-6, maxiter=100)

# --------------------------------------------------------------------------
a = -1.
b = 4.
x = np.linspace(a, b, 1000)

plt.figure()
plt.plot(x, f(x), label='Función f')
plt.plot([a, b], [0, 0], 'k-')
plt.plot(x1, f(x1), 'bo', label='Puntos de Inflexión')
plt.legend(loc='best')
plt.show()
print('PUNTOS DE INFLEXIÓN EN [-1,4]')
print(x1)
# %%--------------------------------------------------------------------------

# Ejercicio 3
"""
PuntoFijo
"""


def busquedaIncremental(f, a, b, n):
    x = np.linspace(a, b, n + 1)
    intervalos = np.zeros((n, 2))
    y = f(x)
    contador = 0

    for i in range(n):
        if y[i] * y[i + 1] < 0:
            intervalos[contador, :] = x[i:i + 2]
            contador += 1

    intervalos = intervalos[:contador, :]
    return intervalos


def punto_fijo(g, x0, tol, maxiter):
    x1 = g(x0)
    error = abs(x1 - x0)
    i = 1
    while error > tol and i < maxiter:
        i += 1
        x0 = x1
        x1 = g(x0)
        error = abs(x1 - x0)
    raiz = x1
    return raiz, i


f = lambda x: np.exp(-x) - x
g = lambda x: np.exp(-x)
# %%
a = 0;
b = 1;
n = 10
intervalos = busquedaIncremental(f, a, b, n)
x0 = intervalos[0, 0]

tol = 1.e-6
maxiter = 200

raiz, i = punto_fijo(g, x0, tol, maxiter)
# %%----------------------------------------------------------------------------
print("\nExiste una raiz en ", intervalos, '\n')
print(raiz, i)

x = np.linspace(a, b)

plt.figure()
plt.plot(x, g(x), 'r-', label='g')
plt.plot(x, x, 'b-', label='y = x')
plt.legend(loc='best')
plt.plot(raiz, raiz, 'bo', label='punto fijo')
plt.show()

# %%----------------------------------------------------------------------------
f = lambda x: x - np.cos(x)
g1 = lambda x: np.cos(x)
g2 = lambda x: 2 * x - np.cos(x)
g3 = lambda x: x - (x - np.cos(x)) / (1 + np.sin(x))
g4 = lambda x: (9 * x + np.cos(x)) / 10.
# %%
a = 0;
b = 1;
n = 10
intervalos = busquedaIncremental(f, a, b, n)
x0 = intervalos[0, 0]

tol = 1.e-6
maxiter = 200

raiz1, i1 = punto_fijo(g1, x0, tol, maxiter)
raiz2, i2 = punto_fijo(g2, x0, tol, maxiter)
raiz3, i3 = punto_fijo(g3, x0, tol, maxiter)
raiz4, i4 = punto_fijo(g4, x0, tol, maxiter)
# %%----------------------------------------------------------------------------

print("\nExiste una raiz en ", intervalos, '\n')

print("g1 ", raiz1, i1)
print("g2 ", raiz2, i2)
print("g3 ", raiz3, i3)
print("g4 ", raiz4, i4)

x = np.linspace(a, b)

plt.figure()
plt.plot(x, g1(x), 'r-', label='g1')
plt.plot(x, g2(x), 'm-', label='g2')
plt.plot(x, g3(x), 'g-', label='g3')
plt.plot(x, g4(x), 'y-', label='g4')
plt.plot(x, x, 'b-', label='y = x')
plt.legend(loc='best')
plt.plot(raiz1, raiz1, 'bo', label='punto fijo')
plt.show()

# %%----------------------------------------------------------------------------
