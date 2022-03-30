import numpy as np


# Recibe un polinomio y devuelve los coeficientes del mismo
def derivada(p):
    n = len(p) - 1
    der = np.zeros(n)
    k = 0
    for i in range(n, 1, -1):
        der[k] = p[k] * i
        k = k + 1
    return der


p = np.zeros(7)
T4 = np.array([8., 0, -8, 0, 1])

print("Derivada de p = ", derivada(p))
print("Derivada de T4 = ", derivada(T4))
