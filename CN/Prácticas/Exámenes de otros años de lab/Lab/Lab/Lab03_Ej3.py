# -*- coding: utf-8 -*-
"""
Created on Mon Mar  8 11:38:54 2021

@author: rodri
"""

import numpy as np
#%%
np.set_printoptions(precision = 2)   # solo dos decimales
np.set_printoptions(suppress = True) # no usar notación exponencial
def triangulariza(A,b):
    Ar= np.copy(A)
    bt= np.copy(b)
    z=len(b)
    for k in range(z-1):
        div=Ar[k+1][0]/Ar[k][1]
        Ar[k+1][0]=Ar[k+1][0]-div*Ar[k][1]
        Ar[k+1][1]=Ar[k+1][1]-div*Ar[k][2]
        bt[k+1]=bt[k+1]-div*bt[k]
    return Ar,bt

def sust_reg(Ar,bt):
    n=len(Ar)
    x=np.zeros(n)
    x[n-1]=bt[n-1]/Ar[n-1][1]
    for i in range(n-2,-1,-1):
        x[i]=(bt[i]-x[i+1]*Ar[i][2])/Ar[i][1]
    return x

n = 7 

Ar = np.zeros((n,3))
Ar[:,0] = np.concatenate((np.array([0]),np.ones((n-1),)))
Ar[:,1] = np.ones((n),)*3
Ar[:,2] = np.concatenate((np.ones((n-1),),np.array([0])))

b = np.arange(n,2*n)*1.
print("---------DATOS--------")
print("Ar")
print(Ar)
print("b")
print(b)
print()
print("---- SISTEMA TRIANGULARIZADO -------")
print("At")
x,y=triangulariza(Ar,b)
print(x)
print()
print("---------- SOLUCIÓN-------------")
print("x")
print(sust_reg(x,y))

n = 8

np.random.seed(3)
Ar = np.zeros((n,3))
Ar[:,1] = np.random.rand(n)
Ar[:,0] = np.concatenate((np.array([0]),np.random.rand(n-1)))
Ar[0:n-1,2] = Ar[1:n,0]

b = np.random.rand(n)

print("---------DATOS--------")
print("Ar")
print(Ar)
print("b")
print(b)
print()
print("---- SISTEMA TRIANGULARIZADO -------")
print("At")
x,y=triangulariza(Ar,b)
print(x)
print()
print("---------- SOLUCIÓN-------------")
print("x")
print(sust_reg(x,y))
