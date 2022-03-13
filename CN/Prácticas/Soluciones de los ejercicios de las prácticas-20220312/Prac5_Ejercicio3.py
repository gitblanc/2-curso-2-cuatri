# -*- coding: utf-8 -*-
"""
Ejercicio 2
"""
import numpy as np
import matplotlib.pyplot as plt
#%%
def busquedaIncremental(f,a,b,n):
    x = np.linspace(a,b,n+1)
    intervalos = np.zeros((n,2))
    y = f(x)
    contador = 0
    
    for i in range(n):
        if y[i]*y[i+1] < 0:
            intervalos[contador,:] = x[i:i+2]
            contador += 1
            
    intervalos = intervalos[:contador,:]    
    return intervalos
#%%----------------------------------------------------------------------------
def punto_fijo(g,x0,tol,maxiter):  
    x1 = g(x0)
    error = abs(x1-x0)
    i = 1    
    while error > tol and i < maxiter: 
        i += 1    
        x0 = x1
        x1 = g(x0)
        error = abs(x1-x0)   
    raiz = x1 
    return raiz, i
#%%----------------------------------------------------------------------------    
f = lambda x: np.exp(-x) - x
g = lambda x: np.exp(-x)
#%%
a = 0; b = 1; n = 10
intervalos = busquedaIncremental(f,a,b,n)
x0 = intervalos[0,0]

tol = 1.e-6
maxiter = 200
    
raiz, i = punto_fijo(g,x0,tol,maxiter)
#%%----------------------------------------------------------------------------
print("\nExiste una raiz en " , intervalos, '\n') 
print(raiz, i)

x = np.linspace(a, b) 

plt.figure()            
plt.plot(x, g(x),'r-', label='g') 
plt.plot(x, x, 'b-',label='y = x') 
plt.legend(loc='best')
plt.plot(raiz,raiz,'bo',label='punto fijo')
plt.show()

#%%----------------------------------------------------------------------------
f = lambda x: x - np.cos(x)
g1 = lambda x: np.cos(x)
g2 = lambda x: 2*x - np.cos(x)
g3 = lambda x: x - (x - np.cos(x)) / (1 + np.sin(x))
g4 = lambda x: (9*x + np.cos(x)) / 10.
#%%
a = 0; b = 1; n = 10
intervalos = busquedaIncremental(f,a,b,n)
x0 = intervalos[0,0]

tol = 1.e-6
maxiter = 200

raiz1, i1 = punto_fijo(g1,x0,tol,maxiter)
raiz2, i2 = punto_fijo(g2,x0,tol,maxiter)
raiz3, i3 = punto_fijo(g3,x0,tol,maxiter)
raiz4, i4 = punto_fijo(g4,x0,tol,maxiter)
#%%----------------------------------------------------------------------------

print("\nExiste una raiz en " , intervalos, '\n')   

print("g1 ", raiz1, i1)
print("g2 ", raiz2, i2)
print("g3 ", raiz3, i3)
print("g4 ", raiz4, i4)

x = np.linspace(a, b)   

plt.figure()          
plt.plot(x, g1(x), 'r-',label='g1') 
plt.plot(x, g2(x), 'm-',label='g2') 
plt.plot(x, g3(x), 'g-',label='g3') 
plt.plot(x, g4(x), 'y-',label='g4') 
plt.plot(x, x, 'b-',label='y = x') 
plt.legend(loc='best')
plt.plot(raiz1,raiz1,'bo',label='punto fijo')
plt.show()

#%%----------------------------------------------------------------------------
