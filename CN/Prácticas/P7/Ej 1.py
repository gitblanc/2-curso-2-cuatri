import numpy as np
import matplotlib.pyplot as plt

def aprox1(f, g, a, b, n):
    x = np.linspace(a, b, n)
    y = f(x)
    v = np.ones((n, g + 1))
    for i in range(0, n):
        for j in range(1, g + 1):
            v[i][j] = (x[i]) ** j
    c = np.dot(v.T, v)
    d = np.dot(v.T, y)
    p = np.linalg.solve(c, d)
    p = p[::-1]

    xp = np.linspace(min(x), max(x))
    yp = np.polyval(p, xp)

    plt.figure()
    plt.plot(xp, yp, label='función aproximada')
    plt.plot(x, y, 'ro', label='puntos')
    plt.legend()
    plt.show()


f = lambda x: np.sin(x)
g = 2
n = 5
a = 0
b = 2
p = aprox1(f, g, a, b, n)

f = lambda x: np.cos(np.arctan(x)) - np.log(x+5)
g = 4
n = 10
a = -2
b = 0
p = aprox1(f, g, a, b, n)
