# -*- coding: utf-8 -*-
"""
Created on Tue Mar 9 15:29:51 2021

@author: rodri
"""
import numpy as np
#%%
def busquedaIncremental(f,a,b,n):
    x=np.linspace(a,b,n+1)
    intervalos=np.zeros((n,2))
    contador=0
    for i in range(len(x)-1):
        a=x[i]
        b=x[i+1]
        if(f(a)*f(b)<0):
            intervalos[contador]=[a,b]
            contador=contador+1
    intervalos=intervalos[:contador,:]
    return intervalos
        

f = lambda x : x**5 - 3 * x**2 + 1.6   # definimos la función
print("Intervalos que contienen raices de f1")
print()
print(busquedaIncremental(f, -1, 1.5, 25)) 
print()
f = lambda x : (x+2)*np.cos(2*x)  # definimos la función
print("Intervalos que contienen raices de f2")
print()
print(busquedaIncremental(f, 0, 10, 100)) 
