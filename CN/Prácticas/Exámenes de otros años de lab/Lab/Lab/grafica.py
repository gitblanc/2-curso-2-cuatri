# -*- coding: utf-8 -*-
"""
grafica de una funcion
"""
import numpy as np
import matplotlib.pyplot as plt
#%%
f = lambda x: np.exp(x)
a = -1.; b = 1.
x = np.linspace(a,b,5)
#x1 = np.arange(a,b+0.1,0.5)
y = f(x) # vectorizacion
#%% vectorizacion
#import time

#z = np.linspace(a,b,10**6)
#zy = np.zeros_like(z)

#t0 = time.time()
#for i in range(len(z)):
#   zy[i] = f(z[i])
#t1 = time.time() - t0
#print('Sin vectorizacion t =',t1)

#t0 = time.time()
#zy = f(z)
#t1 = time.time() - t0
#print
#%%
plt.figure()
plt.plot(x,y)
plt.show()
#%%
#plt.figure()
#plt.plot(x,np.sin(20*x),'o-')
#plt.show()
