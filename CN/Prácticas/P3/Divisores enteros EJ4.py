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

def divisores(m):
    div = np.zeros(2*m)
    cont = 0
    for i in range(m + 1):
        if np.remainder(m, i) == 0:
            div[cont] = i
            div[cont + 1] = -i
            cont += 2
    return div[:cont]

def raices(p):
    r = np.zeros_like(p)
    m = abs(int(p[-1]))
    div = divisores(m)
    cont = 0
    for i in div:
        q, resto = Horner(p,i)
        if resto == 0:
            r[cont] = i
            cont += 1
            p = q
    return r[:cont]


p0 = np.array([1., 0, -1])
p1 = np.array([1., -3, -6, 8])
p2 = np.array([1., 2, -16, -2, 15])
p3 = np.array([1.,-5, -13, 53, 60])
p4 = np.array([1., 4, -56, -206, 343, 490])

print(divisores(6))

print("Raíces de p0 = ",raices(p0))