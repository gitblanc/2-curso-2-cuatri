# -*- coding: utf-8 -*-
"""
Created on Mon Feb 22 20:27:45 2021

@author: rodri
"""
import numpy as np
import matplotlib.pyplot as plt
#%%
def factorial(n):
      res =1
      i=1
      while i<=n:
          res = res *i
          i= i+1
      return res
def funSin(x,tol,maxNumSum):
  polinomio = 0.

  ce= np.inf
  i=1
  count=0
  while ce>tol :
   
    if  count%2!=0:
       sumando = x**i/factorial(i) 
       polinomio = polinomio - sumando
    else:
        sumando = x**i/factorial(i)
        polinomio = polinomio + sumando
    ce = np.abs(sumando)#cota del error
    i=i+2
    count = count+1
    
x = np.linspace(-np.pi,np.pi,50)
tol=1.e-8
maxNumSum=100

p= funSin(x,tol,100)
plt.plot(x,np.sin(x),label='sin(x)')
plt.plot(x,p,'r--',label='funSin')

plt.legend()
      