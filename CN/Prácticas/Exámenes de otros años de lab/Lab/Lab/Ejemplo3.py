# -*- coding: utf-8 -*-
"""
Created on Thu Feb 18 15:54:48 2021

@author: rodri
"""
import numpy as np
import matplotlib.pyplot as plt
#%%
def P(x0,grado):
    polinomio = 0.
    factorial = 1.

    for i in range(grado+1):
        sumando = x0**i/factorial
        polinomio += sumando
        factorial *= i+1
        
    return polinomio 
#----------------------------
x = np.linspace(-1,1)
ox = 0*x 

plt.figure()
plt.plot(x,np.exp(x),label='f')
plt.plot(x,ox,'k')
plt.title('Ejemplo polinomios Taylor')

for grado in range(1,5):
    p= P(x,grado)
    plt.plot(x,p,'r',label='P'+str(grado))
plt.legend()
plt.show()