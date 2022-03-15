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
def factorial(num):
    if num < 0:
        print("Factorial of negative num does not exist")

    elif num == 0:
        return 1

    else:
        fact = 1
        while(num > 1):
            fact *= num
            num -= 1
        return fact

def funcion(der, x0):
    n = len(der)
    pol = np.zeros(n)
    resta = np.zeros(n)
    r = []
    r.reverse()
    contador = 0
    for i in range(0,n):
        r.append(der[n-i-1]/factorial(n-i-1))
        for k in range(0,len(r)-1):
            resta[k+1] = x0 * r[k]
        for j in range(0, len(r)):
            pol[j] = r[j] - resta[j]
        for l in range(0,len(r)):
            r[l] = pol[l]
    return pol

x0 = 1
d0 = np.array([2.,6,14,48,96,120])
p0 = funcion(d0,x0)
print(p0)

x1 = -1
d1 = np.array([3.,0,-34,240,-1056,3120,-5760,5040])
print(funcion(d1,x1))
