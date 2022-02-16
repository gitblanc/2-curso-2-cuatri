import numpy as np
import matplotlib.pyplot as plt

# ----------FUNCIONA PARA NÚMEROS Y PARA VECTORES------------
x0 = 0.5
polinomio = 0.
factorial = 1.

for i in range(4):
    sumando = x0 ** i / factorial
    polinomio += sumando
    factorial *= i + 1

print("----Sin funcion----")
print('P3(0.5)     = ', polinomio)
print('np.exp(0.5) = ', np.exp(x0))
print("----Con funcion----")


def P(x0, grado):
    polinomio = 0.
    factorial = 1.

    for i in range(grado + 1):
        sumando = x0 ** i / factorial
        polinomio += sumando
        factorial *= i + 1

    return polinomio


print('P(0.5, 10)  = ', P(0.5, 10))
print('np.exp(0.5) = ', np.exp(0.5))

# también funciona con vectores

a = -1.
b = 1.
x = np.linspace(a, b, 5)
print("----Vectores----")
print('x         = ', x)
print('P(x, 10)  = ', P(x, 10))
print('np.exp(x) = ', np.exp(x))

# Para pintar un polinomio
a = -1.
b = 1.
f = lambda x: np.exp(x)
x = np.linspace(a, b)  # si no le ponemos un 3 parámetro, pinta 50
ox = 0 * x

plt.figure()
plt.plot(x, f(x), label='f')
plt.plot(x, ox, 'k')
plt.plot(x, P(x, 2), 'r', label='P2')
plt.title('Ejemplo dibujo de la función y el polinomio')
plt.legend()
plt.show()

a = -3.
b = 3.
f = lambda x: np.exp(x)
x = np.linspace(a, b)
ox = 0 * x

plt.figure()
plt.plot(x, f(x), label='f')
plt.plot(x, ox, 'k')

for grado in range(1, 5):
    plt.plot(x, P(x, grado), label='P' + str(grado))

plt.title('Ejemplo dibujo función y polinomios')
plt.legend()
plt.show()

# --------------------------------------
# Definimos función f
f = lambda x: np.exp(x)


# --------------------------------------
# Definimos polinomios
def P(x0, grado):
    polinomio = 0.
    factorial = 1.

    for i in range(grado + 1):
        sumando = x0 ** i / factorial
        polinomio += sumando
        factorial *= i + 1

    return polinomio


# --------------------------------------
a = -3.
b = 3.
x = np.linspace(a, b)
OX = 0 * x

plt.figure()
plt.plot(x, f(x), label='f')
plt.plot(x, OX, 'k')

for grado in range(1, 7):
    plt.plot(x, P(x, grado), label='P' + str(grado))
    plt.title('Ejemplo dibujo de la función y los polinomios')
    plt.legend()
    plt.pause(2)  # para ir viéndolo aparecer
plt.show()
