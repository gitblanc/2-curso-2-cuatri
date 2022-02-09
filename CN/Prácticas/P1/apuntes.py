import numpy as np
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D # para la visualización tridimensional

print("-----Numpy-----")
matriz = np.ones((3, 2))
matriz2 = np.array([(2,2), (3,5)])
matriz3 = np.array([(3,3), (5, 7)])
matriz4 = np.array([[1, 2, 4], [3, -1, 2]])

a = np.arange(1, 10, 2)  # generación de vectores de forma automática separados por dos como enteros
b = np.arange(1, 10, 4)
c = np.arange(10)
d = np.linspace(1, 9, 5)  # generación de 5 vectores igualmente separadas como flotantes

e = np.copy(a)  # para copiar no se hace un =, ya que se modificaría el original

print(matriz)
print(matriz2)
print(matriz2+4)  # realizar una operación a cada elemento de la matriz
print(matriz2*matriz3)
print(np.dot(matriz2, matriz3))  # Multiplicación de matrices matemática
print(a)
print(b)
print(c)
print(d)

print(a[0])  # primer elemento
print(a[-1])  # último elemento

print(c[2:8])  # para coger determinados elementos del vector
print(c[1:8:2])  # con paso
print(c[:-1])  # hasta el ultimo
print(c[4:])  # desde el elemento hasta el final

print(matriz4[:2, :2])  # sacar elementos o partes de una matriz
print(matriz4[:, 1:])

print(e)
e[0] = 908
print(e)
print(a)

print(np.exp(a))  # exponencial
print(np.sin(np.pi/2))  # trigonometría

f = lambda x : x**2 # funciones
print(f(4))
print(f(a))  # tambien se puede hacer sobre vectores

k = lambda s,y : s**2+y**2  # funciones con varias variables
print(k(2,3))
print("-----Numpy-----\n")

print("-----MatplotLib-----")
plt.plot(a, f(a))  # construir grafica
plt.grid()  # añadirle rejillas
plt.title('Función')
plt.xlabel('x')
plt.ylabel('y')
plt.plot(a, 0*a, 'r')
plt.show()  # mostrar la grafica

# representación de mallas
xgrid = np.linspace(-1,1,20)
ygrid = np.linspace(-1,1,20)

fx, fy = np.meshgrid(xgrid,ygrid)  # creamos dos matrices que contienen la malla bidimensional

g = lambda fx,fy: fx**2 + fy**2  # se define la función

plt.figure()  # se crea la figura
plt.contourf(fx,fy,g(fx,fy), cmap='jet')
plt.colorbar()
plt.show()  # bidimensional

fig1  = plt.figure(figsize=(10,5))
ax1   = fig1.gca(projection='3d')
ax1.plot_surface(fx, fy, g(fx,fy), cmap='jet')
plt.show()  # tridimensional

fig2  = plt.figure(figsize=(10,5))
ax2   = fig2.gca(projection='3d')
ax2.plot_wireframe(fx, fy, g(fx,fy))
plt.show()  # como malla
