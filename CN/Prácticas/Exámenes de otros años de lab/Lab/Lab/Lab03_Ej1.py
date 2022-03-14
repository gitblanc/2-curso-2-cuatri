# -*- coding: utf-8 -*-
"""
Created on Mon Mar  8 11:38:40 2021

@author: rodri
"""

import numpy as np
#%%

def triangulariza(A,b):
    At= np.copy(A)
    bt= np.copy(b)
    n=len(b)
    for k in range(n-1):
        div=At[k+1][k]/At[k][k]
        At[k+1][k]=At[k+1][k]-div*At[k][k]
        At[k+1][k+1]=At[k+1][k+1]-div*At[k][k+1]
        bt[k+1]=bt[k+1]-div*bt[k]
    return At,bt
    


np.set_printoptions(precision = 2)   # solo dos decimales
np.set_printoptions(suppress = True) # no usar notación exponencial
   


n=7
A1 = np.diag(np.ones(n))*3
A2 = np.diag(np.ones(n-1),1) 
A = A1 + A2 + A2.T 

b = np.arange(n,2*n)*1.

m,n=(triangulariza(A,b))
print("----------DATOS----------")
print("A")
print(A)
print("b")
print(b)
print()
print("------SISTEMA TRIANGULARIZADO--------")
print("At")
print(m)
print("bt")
print(n)

n=8
np.random.seed(3)
A1 = np.diag(np.random.rand(n))
A2 = np.diag(np.random.rand(n-1),1)
A = A1 + A2 + A2.T 

b = np.random.rand(n)
m,n=(triangulariza(A,b))
print("----------DATOS----------")
print("A")
print(A)
print("b")
print(b)
print()
print("------SISTEMA TRIANGULARIZADO--------")
print("At")
print(m)
print("bt")
print(n)