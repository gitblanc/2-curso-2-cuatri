import matplotlib.pyplot as plt
import numpy as np

# Ejercicio 1
"""
a = np.array([1, 2, 3, 4])
b = np.array([(1.5, 2, 3), (4, 5, 6)])
c = np.zeros((3, 4))
d = np.ones((2, 3))
e = np.arange(1, 10, 2)
f = np.arange(1., 10, 2)
g = np.linspace(1, 9, 5)
a =
 [2.  2.5 3.  3.5 4. ]

"""
a1 = np.array([1, 3, 7])
b1 = np.array([(2, 4, 3), (0, 1, 6)])
c1 = np.zeros((3, 2))
d1 = np.ones((3, 4))
print(a1)
print(b1)
print(c1)
print(d1)
print("---------------------------")
# Ejercicio 2
a2 = np.arange(7, 16, 2)  # hay que poner uno más
b2 = np.arange(10, 5, -1)
c2 = np.arange(15, -1, -5)
print(a2)
print(b2)
print(c2)
a2 = np.linspace(7, 15, 5)  # hay que poner uno más
b2 = np.linspace(10, 6, 5)
c2 = np.linspace(15, 0, 4)
print(a2)
print(b2)
print(c2)
print("---------------------------")

# Ejercicio 3
"""
a
 [0 1 2 3 4 5 6 7 8 9]

a[1:8]
 [1 2 3 4 5 6 7]

a[1:8:2]
 [1 3 5 7]

a[-1]
 9

a[:-1]
 [0 1 2 3 4 5 6 7 8]

a[3]
 3

a[3:]
 [3 4 5 6 7 8 9]

a[b]
 [0 1 5 9]
"""
A = np.array([(2, 1, 3, 4), (9, 8, 5, 7), (6, -1, -2, -8), (-5, -7, -9, -6)])
a = A[:,0]
b = A[2,:]
c = A[0:2,0:2]#Añadir siempre uno más
d = A[2:,2:]#el -1 es la penultima posición
e = A[1:3,1:3]
f = A[0:,1:]
g = A[1:,1:]

print("A = \n", A)
print("a = \n",a)
print("b = \n",b)
print("c = \n",c)
print("d = \n",d)
print("e = \n",e)
print("f = \n",f)
print("g = \n",g)
print("--------------------------")

#Ejercicio 4
"""
Escribe las funciones y calcula sus valores en los puntos que se indican:
"""
f = lambda x : x*np.exp(x)
g = lambda z : z/(np.sin(z)*np.cos(z))
h = lambda x,y : (x*y)/(x**2 + y**2)

print("f(2) = ",f(2))
print("g(pi/4) = ", g(np.pi/4))
print("h(2,4) = ", h(2,4))

#Ejercicio 5
"""
Dibuja en el intervalo  [−2π,2π]  la función  f(x)=xsen(3x)
"""
f = lambda x : x*np.sin(3*x)
x=np.linspace(-2*np.pi, 2*np.pi, 200)
OX = 0*x
plt.figure()
plt.plot(x,f(x))
plt.plot(x,OX,'k-')
plt.xlabel('x')
plt.ylabel('y')
plt.title('x sen(3x)')
plt.show()