import numpy as np
import matplotlib.pyplot as plot


#Ej 1
def horner_inv(q, r, x0):
    n = len(q)
    p = np.zeros(n + 1)
    p[0] = q[0]
    for i in range(1, n):
        p[i] = q[i] - q[i - 1] * x0
    p[-1] = r - q[-1] * x0
    return p


x0 = 1.
q0 = np.array([1., 3])
r0 = 4.
p0 = horner_inv(q0, r0, x0)
print(p0)

x1 = 1.
q1 = np.array([1., 0, 2, -1, 4])
r1 = 2.
p1 = horner_inv(q1, r1, x1)
print(p1)

x2 = -1.
q2 = np.array([1., -2, 1, 0, -1, 1, -2])
r2 = 3.
p2 = horner_inv(q2, r2, x2)
print(p2)

# Ejercicio 2
def horner(p, x0):
    n = len(p)
    q = np.zeros(n)
    q[0] = p[0]
    for i in range(1, n):
        q[i] = q[i - 1] * x0 + p[i]
    cociente = q[:-1]
    resto = q[-1]
    return cociente, resto


def pol_a_partir_de_der(der, x0):
    p = np.zeros_like(der)

    c = np.copy(der)
    factorial = len(der)-1
    for k in range(len(c)-1, 0, -1):
        r = c[k]/factorial
        p = horner_inv(c, r, x0)
        factorial *= k - 1
    return p


x0 = 1.
d0 = np.array([2., 6, 14, 48, 96, 120])
p0 = pol_a_partir_de_der(d0, x0)
print(p0)
