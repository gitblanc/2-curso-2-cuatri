import numpy as np
import matplotlib.pyplot as plt


def newton_raphson(f, df, x0, tol=1.e-6, n=100):
    for i in range(n):
        x1 = x0 - f(x0) / df(x0)

        if abs(x1 - x0) < tol:
            break
        x0 = x1

    return x1, i + 1  # i+1 porque el cero también cuenta


f = lambda x: np.sin(2 * x + 3) + np.cos(x)
df = lambda x: np.cos(2 * x + 3) * 2 - np.sin(x)
print("Newton Raphson con x0 = 0: ", newton_raphson(f, df, 0))
print("Newton Raphson con x0 = 2: ", newton_raphson(f, df, 2))

# Gráfica de f(x) = sen(2x + 3) + cos(x) en [0,2]
x = np.linspace(0, 2, 50)
plt.figure()
plt.plot(x, f(x), label='f')
plt.plot([0, 2], [0, 0], 'k-')
plt.legend(['f'])
plt.title('f')
plt.grid()
plt.show()
