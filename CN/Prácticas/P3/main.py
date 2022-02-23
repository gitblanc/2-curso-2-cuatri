import numpy as np
import matplotlib.pyplot as plt

p = np.array([1., -1, 2, -3, 5, -2])
p0, p1, p2, p3, p4, p5 = p

g = lambda x: p0 * x ** 5 + p1 * x ** 4 + p2 * x ** 3 + p3 * x ** 2 + p4 * x + p5
d1g = lambda x: 5 * p0 * x ** 4 + 4 * p1 * x ** 3 + 3 * p2 * x ** 2 + 2 * p3 * x + p4
d2g = lambda x: 20 * p0 * x ** 3 + 12 * p1 * x ** 2 + 6 * p2 * x + 2 * p3

a = -1.
b = 1.
x = np.linspace(a, b)

plt.figure()
plt.plot(x, 0 * x, 'k')
plt.plot(x, g(x), label='g')
plt.plot(x, d1g(x), label='d1g')
plt.plot(x, d2g(x), label='d2g')
plt.legend()
plt.show()
