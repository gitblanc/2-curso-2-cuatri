# -*- coding: utf-8 -*-
"""
Created on Tue May  4 20:15:20 2021

@author: rodri
"""

import numpy as np 

np.set_printoptions(precision = 2)   # solo dos decimales
np.set_printoptions(suppress = True) # no usar notación exponencial

def sust_regre(U,b):
    x=np.zeros(len(U))
    for i in range(len(U)-1,-1,-1):
        xj=0
        for j in range(i+1,len(U)):
             xj=U[i][j]*x[j]+xj
           
        xi=(1/U[i][i])*(b[i]-xj)
        x[i]=xi
        
    return x
    
    

U = np.array([[2., 1, 1], [0, 2, 1], [0, 0, 2]])
b = np.array([9., 4, 4])

print(sust_regre(U,b))

n = 5
np.random.seed(2)           
U = np.random.random((n,n)) 
U = np.triu(U) # Haz cero los elementos bajo la diagonal
b = np.random.random(n)

print(sust_regre(U,b))