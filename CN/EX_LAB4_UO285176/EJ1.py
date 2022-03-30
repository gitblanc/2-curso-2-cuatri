import numpy as np


# Recibe un polinomio y devuelve el número de raíces
def cambios_signo(p):
    cont = 0
    for i in range(0, len(p) - 1):
        ceros = 1  # Hay que tener en cuenta los ceros
        while p[i + ceros] == 0:
            ceros = ceros + 1
            if i + ceros > len(p) - 1:
                return cont
        if p[i] * p[i + ceros] < 0:
            cont = cont + 1
    return cont


p0 = np.array([32., -32, -14, 17, -3])
p1 = np.array([32., -32, 0, 17, -3])
T4 = np.array([8, 0, -8, 0, 1])
T6 = np.array([32., 0, -48, 0, 18, 0, -1])

print("P0")
print(cambios_signo(p0), " raíces reales positivas como máximo")
print("P1")
print(cambios_signo(p1), " raíces reales positivas como máximo")
print("T4")
print(cambios_signo(T4), " raíces reales positivas como máximo")
print("T6")
print(cambios_signo(T6), " raíces reales positivas como máximo")
