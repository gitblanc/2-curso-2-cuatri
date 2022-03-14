# -*- coding: utf-8 -*-
"""
Created on Mon Feb 22 18:51:15 2021

@author: rodri
"""

import numpy as np
import matplotlib.pyplot as plt
#%%



def funExp(x0, tol):
    factorial = 1.
    polinomio = 0.
    ce= np.inf
    i=0

    while ce>tol:
       sumando = x0**i/factorial
       polinomio += sumando
       ce = np.abs(sumando)#cota del error
       factorial *= i+1
       i=i+1
       
    return polinomio  

        
    
x = np.linspace(-1,1,50)
tol=1.e-8
maxNumSum=100

p= funExp(x,tol)
plt.plot(x,p,'r--',label='funExp')
plt.plot(x,np.exp(x),label='f(x)=$e^x$')
plt.legend()
      