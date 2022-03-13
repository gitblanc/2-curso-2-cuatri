#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Busqueda incremental
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

#%% Ejemplo 1
f1 = lambda x: x**5 - 3 * x**2 + 1.6 
a = -1.; b = 1.5; n = 25


intervalos = busquedaIncremental(f1,a,b,n)
print('Intervalos que contienen raíces de f1\n')
print(intervalos)

#%% Ejemplo 2
f2 = lambda x: (x+2) * np.cos(2*x)
a = 0.; b = 10.; n = 100

intervalos = busquedaIncremental(f2,a,b,n)
print('\nIntervalos que contienen raíces de f2\n')
print(intervalos)
   

