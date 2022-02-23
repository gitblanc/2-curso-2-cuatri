import numpy as np
import matplotlib.pyplot as plt

def Horner(p, x0):
    q = np.zeros(len(p))  # vector de cociente y resto

    for i in range(len(p)):
        suma = p[i] + x0 * q[i - 1]
        q[i] = suma

    cociente = q[:-1]
    resto = q[-1]
    return cociente, resto

# p es un vector
def derPol(p, x0):
    q = np.zeros(len(p))
    d = np.zeros(len(p))
    fact = 1
    for i in range(len(p)):
        q, resto = Horner(p,x0)
        d[i] = resto*fact
        p = q
        fact *= (i + 1)
    return d


p = np.array([1., -1, 2, -3,  5, -2])
x0 = 1.

derivadas = derPol(p, x0)
print("Las derivadas sucesivas son: ", derivadas)