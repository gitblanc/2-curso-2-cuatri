# -*- coding: utf-8 -*-
"""
Created on Tue Feb 23 16:05:39 2021

@author: rodri
"""

import numpy as np
import matplotlib.pyplot as plt
#%%

def horner(p,x0):
    cociente=np.zeros_like(p)
    for i in range(1,len(cociente)):
        cociente[i]=p[i]+cociente[i-1]*x0
    resto = cociente[len(cociente)-1]
    return resto

def HornerV(p,x):
    y=np.zeros_like(x)
    for i in range(len(x)):
        y[i]=horner(p,x[i])
    return y

x = np.linspace(-1,1)
p = np.array([1., -1, 2, -3, 5, -2])
r = np.array([1., -1, -1, 1, -1, 0, -1, 1])

plt.figure()
plt.plot(x,HornerV(p,x))
plt.plot(x,0*x,'k')
plt.title('P')
plt.show()
plt.figure()
plt.plot(x,HornerV(r,x))
plt.plot(x,0*x,'k')
plt.title('R')
plt.show()
plt.figure()
plt.plot(x,np.polyval(p,x))
plt.plot(x,0*x,'k')
plt.title('Polinomio P')
plt.show()