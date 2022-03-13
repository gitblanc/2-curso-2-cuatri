#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Horner para un vector, sin vectorizar
"""
import numpy as np
import matplotlib.pyplot as plt
#%%
def hornerV(p,x):
    n = len(p)
    q = np.zeros(n)
    
    m = len(x)
    y = np.zeros_like(x)
    
    for k in range(m):
        x0 = x[k]

        q[0] = p[0]
        for i in range(1,n):
            q[i] = q[i-1]*x0 + p[i]
        
        y[k] = q[-1]   
    return y   
#%%
p = np.array([1, -1, 2, -3,  5, -2])

x = np.linspace(-1,1)

y = hornerV(p,x)
plt.plot(x,y)
plt.plot(x,0*x,'k')
plt.title('P')
plt.show()

#%%
r = np.array([1., -1, -1, 1, -1, 0, -1, 1])

x = np.linspace(-1,1)

y = hornerV(r,x)
plt.plot(x,y)
plt.plot(x,0*x,'k')
plt.title('R')
plt.show()

