# -*- coding: utf-8 -*-
"""
Created on Tue May  4 22:11:54 2021

@author: rodri
"""
import numpy as np

def sust_regre(U,b):
    x=np.zeros(len(U))
    for i in range(len(U)-1,-1,-1):
        xj=0
        for j in range(i+1,len(U)):
             xj=U[i][j]*x[j]+xj
           
        xi=(1/U[i][i])*(b[i]-xj)
        x[i]=xi
        
    return x

def triang(A,b):
   for k in range(len(b)-1):
       for i in range(k+1,len(b)):
         if(A[k][k]==0):
            continue
         f=A[i][k]/A[k][k]
         b[i]=b[i]-f*b[k]
         A[i]=A[i]-f*A[k]
         
   return A,b

A = np.array([[2., 1, 1], [1, 2, 1], [1, 1, 2]])
b = np.array([2., 4, 6]) 
u,c=triang(A,b)
print('-----DATOS------')
print()
print('A')
print(A)
print('b')
print(b) 
print()
print('--TRIANGULARIZACION--') 
print()
print('U')
print(u)
print('c')
print(c)
print()
print('--SUSTITUCION REGRESIVA--')
print()
print('x')
print(sust_regre(u, c))
print()  
n = 5
np.random.seed(3)           
A = np.random.random((n,n)) 
b = np.random.random(n)
u,c=triang(A,b)
print('-----DATOS------')
print()
print('A')
print(A)
print('b')
print(b) 
print()
print('--TRIANGULARIZACION--') 
print()
print('U')
print(u)
print('c')
print(c)
print()
print('--SUSTITUCION REGRESIVA--')
print()
print('x')
print(sust_regre(u, c))
print()  