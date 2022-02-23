import numpy as np
import matplotlib.pyplot as plt


# p es un vector
def Horner(p, x0):
    q = np.zeros(len(p))  # vector de cociente y resto

    for i in range(len(p)):
        suma = p[i] + x0 * q[i - 1]
        q[i] = suma

    cociente = q[:-1]
    resto = q[-1]
    return cociente, resto


p0 = np.array([1., 2, 1])
x0 = 1.

p1 = np.array([1., -1, 2, -3,  5, -2])
x1 = 1.

p2 = np.array([1., -1, -1, 1, -1, 0, -1, 1])
x2 = -1.

c, r = Horner(p0, x0)
print("Cociente = ", c)
print("Resto = ", r)

c1, r1 = Horner(p1, x1)
print("Cociente = ", c1)
print("Resto = ", r1)

c2, r2 = Horner(p2, x2)
print("Cociente = ", c2)
print("Resto = ", r2)