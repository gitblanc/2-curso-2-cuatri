# -*- coding: utf-8 -*-
"""
Created on Tue Feb 23 15:02:48 2021

@author: rodri
"""
import numpy as np
#%%
def horner(p,x0):  
     n = len(p)
     cociente=np.zeros(n-1)
     # inicializaria q
     for i in range(n):
        if i==0:
           cociente[0]=p[0]
        elif i==n-1:
           resto=p[n-1]+x0*cociente[n-2]
        else:
           cociente[i]=cociente[i-1]*x0+p[i]
 
     return cociente,resto

p0 = np.array([1.,2,1])
x0 = 1. 
c,r = horner(p0, x0)
print("Coeficiente de Q = ",c)
print("Resto P(",x0,") = ",r)   
p1 = np.array([1.,-1,2,-3,5,-2])
x1= 1.
c,r = horner(p1, x1)
print("Coeficiente de Q = ",c)
print("Resto P(",x1,") = ",r)
p2 = np.array([1., -1, -1, 1, -1, 0, -1, 1])
x2 = -1
c,r = horner(p2, x2)
print("Coeficiente de Q = ",c)
print("Resto P(",x2,") = ",r)
