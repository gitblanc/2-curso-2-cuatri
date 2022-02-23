import numpy as np
import matplotlib.pyplot as plt


# p es un vector
# ahora sólo se almacena el resto para cada elemento de x (un vector)
def HornerV(p, x):
    restos = np.zeros(len(x))
    for k in range(len(x)):
        q = np.zeros(len(p))  # vector de cociente y resto
        for i in range(len(p)):
            suma = p[i] + x[k] * q[i - 1]
            q[i] = suma
        restos[k] = q[-1]
    return restos


p = np.array([1., -1, 2, -3, 5, -2])
x = np.linspace(-1, 1)  # la x
r = np.array([1., -1, -1, 1, -1, 0, -1, 1])

restos1 = HornerV(p, x)
print("Restos = ", restos1)

restos2 = HornerV(r, x)
print("Restos = ", restos2)

plt.figure()
plt.plot(x,restos1)
plt.plot(x,0*x,'k')
plt.title('Polinomio P')
plt.grid()
plt.show()