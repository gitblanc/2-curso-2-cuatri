# -*- coding: utf-8 -*-
"""
Created on Sat Feb 20 14:17:35 2021

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
x0 = 0.5
tol=1.e-8
polinomio = 0.
polinomio2 = 0.
polinomio3= 0.
ce= np.inf
ce2= np.inf
ce3= np.inf
tol2=10**-4
i=1
j=0
k=1
w=0
#bucle para hacer el sinh(x)
while ce>tol :
       sumando = x0**i/factorial(i) 
       polinomio = polinomio + sumando
       ce = np.abs(sumando)#cota del error
       i=i+2
       
#bucle para hacer el cosh(x)       
while ce2>tol :
       sumando = x0**j/factorial(j) 
       polinomio2 = polinomio2 + sumando
       ce2 = np.abs(sumando)#cota del error
       j=j+2
#Bucle para hacer la tanjente ##no lo consigo intentando aplicarlo junto
while ce3>tol2:
       sumando1 = x0**k/factorial(k) 
       sumando2 = x0**w/factorial(w)
       if sumando2 !=0:
           sumandot= sumando1/sumando2
       polinomio3 = polinomio3 + sumandot    
       ce3 = np.abs(sumandot)#cota del error
       w=w+2
       k=k+2
       
#print(polinomio3)
print("Valor aprox   = ",polinomio/polinomio2)
print("Valor exacto  = ",np.tanh(0.5))
#print(np.sinh(0.5))
#print(polinomio)
#print(np.cosh(0.5))
#print(polinomio2)
