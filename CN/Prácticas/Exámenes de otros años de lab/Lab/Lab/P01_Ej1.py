# -*- coding: utf-8 -*-
"""
Created on Thu Feb 18 16:14:02 2021

@author: rodri
"""
import numpy as np
#%%
x0 = -0.4
tol =1.e-8
maxNumSum = 100
grado = 3

polinomio = 0.
factorial= 1.

ce= np.inf
i=0

while ce>tol:
    sumando = x0**i/factorial
    polinomio += sumando
    ce = np.abs(sumando)#cota del error
    factorial *= i+1
    i=i+1


print('Valor de la funcion en -0.4      = ', np.exp(x0))
print('Valor de la aproximacion en -0.4 = ', polinomio)
print('Número de iteraciones            = ', i)