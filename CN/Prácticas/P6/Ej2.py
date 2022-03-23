import numpy as np
import matplotlib.pyplot as plt


def lagrange_fund(k, x, z):
    x0 = x[0]
    xk = x[k]
    if k != 0:
        lk = (z - x0) / (xk - x0)
    else:
        lk = 1
    for i in range(1, len(x)):
        if i == k:
            lk = lk
        else:
            lk = lk * (z - x[i]) / (xk - x[i])
    return lk


def lagrange_fundamental(k, x, z):
    n = np.zeros(len(z))
    for j in range(len(z)):
        n[j] = lagrange_fund(k, x, z[j])
    return n


def polinomio_lagrange(x, y, z):
    pn = 0
    for i in range(len(x)):
        pn = lagrange_fundamental(i, x, z) * y[i] + pn
    return pn


x = np.array([-1., 0, 2, 3, 5])
y = np.array([1., 3, 4, 3, 1])
z = np.array([1.3, 2.1, 3.2])

j = np.linspace(-1, 5)

plt.figure()
plt.plot(polinomio_lagrange(x, y, j))
plt.title('[-1,5]')
plt.show()

x1 = np.array([-1., 0, 2, 3, 5, 6, 7])
y1 = np.array([1., 3, 4, 3, 2, 2, 1])

w = np.linspace(-1, 7)
plt.figure()
plt.plot(polinomio_lagrange(x1, y1, w))
plt.title('[-1,7]')
plt.show()