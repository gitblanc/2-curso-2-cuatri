# -*- coding: utf-8 -*-
"""
Created on Sun Feb 21 17:45:22 2021

@author: rodri
"""

import numpy as np
#%%
def factorial(n):
      res =1
      i=1
      while i<=n:
          res = res *i
          i= i+1
      return res
x0 = np.pi/4
tol=1.e-8
maxNumSum=100
polinomio = 0.

ce= np.inf
i=1
count=0
while ce>tol :
    if  count%2!=0:
       sumando = x0**i/factorial(i) 
       polinomio = polinomio - sumando
    else:
        sumando = x0**i/factorial(i)
        polinomio = polinomio + sumando
    ce = np.abs(sumando)#cota del error
    i=i+2
    count = count+1
    
print("Valor aporx           = ",polinomio)
print("Valor exacto          = ",np.sin(np.pi/4))
print('Número de iteraciones = ',count)