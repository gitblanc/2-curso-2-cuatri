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


def mediaMovil(y):
    m1 = np.ones_like(y) * np.nan
    l = len(y)
    for i in range(1, l - 1):
        m1[i] = ((y[i - 1] + y[i] + y[i + 1]) / 3)
    return m1


np.random.seed(1)
x1 = np.arange(100)
y1 = np.sin(x1 / 10) + np.log(x1 + 10) + np.random.rand(len(x1)) * 0.5

np.random.seed(1)
x2 = np.arange(50)
y2 = x2 * np.sin(x2 / 10) + np.random.rand(len(x2)) * 10

m1 = mediaMovil(y1)
m2 = mediaMovil(y2)

import pandas as pd

df1 = pd.DataFrame({'y1': y1, 'm1': m1})
print(df1)

plt.figure()
plt.plot(m1, 'r')
plt.plot(y1, 'b')
plt.show()

plt.figure()
plt.plot(m2, 'r')
plt.plot(y2, 'b')
plt.show()

# %%
'''EJERCICIO 2'''


def mediaMoviln(y, n):
    num = n / 2
    long = int(np.floor(num))
    medio = long + 1
    m = np.ones_like(y) * np.nan
    lenglista = len(y)
    i = medio
    for i in range(medio - 1, lenglista - medio + 1):
        result = 0
        for pos in range(i + 1, i + long):
            result = result + y[pos]
        for negpos in range(i + 1, i - long - 1, -1):
            result = result + y[negpos]
        m[i] = result / n
    return m


np.random.seed(1)
x = np.arange(100)
y = np.sin(x / 10) + np.log(x + 10) + np.random.rand(len(x)) * 0.5
n = 3

np.random.seed(3)
x2 = np.arange(300)
y2 = np.sin(x / 50) + np.log(x + 10) + np.random.rand(len(x)) * 0.3
n2 = 9

np.random.seed(3)
x3 = np.arange(300)
y3 = np.sin(x / 50) + np.log(x + 10) + np.random.rand(len(x)) * 0.3
n3 = 9

np.random.seed(1)
x4 = np.arange(50)
y4 = x * np.sin(x / 10) + np.random.rand(len(x)) * 10
n4 = 5

m = mediaMoviln(y, n)
m2 = mediaMoviln(y2, n2)
m3 = mediaMoviln(y3, n3)
m4 = mediaMoviln(y4, n4)


df = pd.DataFrame({'y': y, 'm': m})
print(df)

plt.figure()
plt.plot(m, 'r')
plt.plot(y, 'b')
plt.show()

plt.figure()
plt.plot(m2, 'r')
plt.plot(y2, 'b')
plt.show()

plt.figure()
plt.plot(m3, 'r')
plt.plot(y3, 'b')
plt.show()

plt.figure()
plt.plot(m4, 'r')
plt.plot(m4, 'b')
plt.show()
