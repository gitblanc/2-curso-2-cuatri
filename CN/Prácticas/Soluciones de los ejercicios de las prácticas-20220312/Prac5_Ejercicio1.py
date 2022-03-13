## module secante1
# -*- coding: utf-8 -*-
"""
raiz = secante(f,x0,x1,tol=1.e-6)
Encuentra la raíz de f(x)=0 utilizando el método de la secante.
"""
import numpy as np
import matplotlib.pyplot as plt
#------------------------------------
def secante(f,x0,x1,tol=1.0e-6,n=200):
    
    for i in range(n):
        x2 = x1 - f(x1) * (x1-x0) /  (f(x1)-f(x0))
        if abs(x2 - x1) < tol:
            break
        x0 = x1
        x1 = x2
    
    return x2,i+1 # i+1 porque el cero también cuenta

#%%---------------------------------------------------------   
f = lambda x: x**5 - 3 * x**2 + 1.6  # definimos la función f
r = np.zeros(3)

tol = 1.e-6; maxiter = 100

x0 = -0.7; x1 = -0.6
r[0], i1 = secante(f,x0,x1,tol,maxiter)
print(r[0],i1)

x0 = 0.8; x1 = 0.9
r[1], i2 = secante(f,x0,x1,tol,maxiter)
print(r[1], i2)

x0 = 1.2; x1 = 1.3
r[2], i3 = secante(f,x0,x1,tol,maxiter)
print(r[2], i3)  

plt.figure()
x = np.linspace(-1,1.5)
plt.plot(x,f(x),x,0*x,'k',r,r*0,'ro')
plt.show()
    
        

