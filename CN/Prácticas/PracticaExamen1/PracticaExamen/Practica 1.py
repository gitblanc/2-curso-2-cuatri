import matplotlib.pyplot as plt
import numpy as np

# Ejercicio 1
"""
Polinomio de Taylor

x0 = 0.5
polinomio = 0.
factorial = 1.

for i in range(4):
    sumando = x0**i/factorial
    polinomio += sumando
    factorial *= i+1


print('P3(0.5)     = ', polinomio)
print('np.exp(0.5) = ', np.exp(x0))
"""


def mclaurin(x0, polinomio, tolerancia):
    i = 0
    factorial = 1
    cota = tolerancia + 1  # para que entre en el bucle
    while i < 100 and cota >= tolerancia:
        sumando = x0 ** i / factorial
        polinomio += sumando
        cota = abs(sumando)
        factorial *= i + 1
        i += 1
    return polinomio, i


f = lambda x: np.exp(x)

polinomio = 0.
x = -0.4
tol = 1.e-8
pol, ix = mclaurin(x, polinomio, tol)
print("El valor de la función en ", x, " = ", f(x))
print("El valor de la aproximación en ", x, " = ", pol)
print("Número de iteraciones = ", ix)

print("---------------------------------")


# Ejercicio 2
def mclaurin2(x0, polinomio, tolerancia, max):
    i = 0
    factorial = 1
    cota = tolerancia + 1  # para que entre en el bucle
    while i < max and cota >= tolerancia:
        sumando = x0 ** i / factorial
        polinomio += sumando
        cota = np.max(abs(sumando))
        factorial *= i + 1
        i += 1
    return polinomio, i

f = lambda x: np.exp(x)
tol = 1.e-8
x = np.linspace(-1,1)
pol = 0.
y = mclaurin2(x, pol, tol,100)
y0 = y[0]
plt.figure()
plt.plot(x, f(x), "y", linewidth = 4, label = "f")
plt.plot(x, y0, "b--", label = "Aproximación de f")
plt.plot(x, 0*x, "k")
plt.title("Aproximación de f con el polinomio de McLaurin")
plt.grid()
plt.show()
