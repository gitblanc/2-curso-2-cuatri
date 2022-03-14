# -*- coding: utf-8 -*-
"""
Created on Tue Mar  2 13:36:37 2021

@author: rodri
"""

import numpy as np



def divisores(m):
    x=abs(m)
    div=np.zeros(2*m)
    n=0
    for i in range(1,x+1):
       if  np.remainder(x,i) ==0:
           div[n]=i
           n=n+1
           div[n]=-i
           n=n+1
    div=div[:n]
    return div    

print("Los divisores del 6")
print(divisores(6))
print("Los divisores del 18")
print(divisores(18))
print("Los divisores del 20")
print(divisores(20))
