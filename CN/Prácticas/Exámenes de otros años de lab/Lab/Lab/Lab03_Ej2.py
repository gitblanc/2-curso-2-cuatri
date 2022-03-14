# -*- coding: utf-8 -*-
"""
Created on Mon Mar  8 11:38:53 2021

@author: rodri
"""

import numpy as np
#%%
np.set_printoptions(precision = 2)   # solo dos decimales
np.set_printoptions(suppress = True) # no usar notación exponencial

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

def sust_reg(At,bt):
    n=len(At)
    x=np.zeros(n)
    x[n-1]=bt[n-1]/At[n-1][n-1]
    y=x[n-1]
    for i in range(n-2,-1,-1):
        x[i]=(bt[i]-y*At[i][i+1])/At[i][i]
        y=x[i]
    return x
        
n=7
A1 = np.diag(np.ones(n))*3
A2 = np.diag(np.ones(n-1),1) 
A = A1 + A2 + A2.T 

b = np.arange(n,2*n)*1.

m,n=(triangulariza(A,b))
print("x")
print(sust_reg(m,n))
print()
n=8
np.random.seed(3)
A1 = np.diag(np.random.rand(n))
A2 = np.diag(np.random.rand(n-1),1)
A = A1 + A2 + A2.T 

b = np.random.rand(n)
m,n=(triangulariza(A,b))
print("x")
print(sust_reg(m, n))