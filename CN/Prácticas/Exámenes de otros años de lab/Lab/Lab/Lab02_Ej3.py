# -*- coding: utf-8 -*-
"""
Created on Tue Mar  2 16:20:10 2021

@author: rodri
"""
import numpy as np
def divisores(m):
    x=abs(m)
    div=np.zeros(2*x)
    n=0
    for i in range(1,x+1):
       if  np.remainder(x,i) ==0:
           div[n]=i
           n=n+1
           div[n]=-i
           n=n+1
    div=div[:n]
    return div   

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

def raices(p):
    n=np.zeros_like(p)
    count=0
    div=divisores(int(p[-1]))
    for i in range(len(div)):
        if((horner(p,div[i]))[1]==0):
            n[count]=div[i]
            count = count +1
    return n[:count]

p0 = np.array([1., 0, -1])
p1 = np.array([1., -3, -6, 8])
p2 = np.array([1., 2, -16, -2, 15])
p3 = np.array([1.,-5, -13, 53, 60])   
p4 = np.array([1., 4, -56, -206, 343, 490])
print("Raices de p0")
print(raices(p0))
print("Raices de p1")
print(raices(p1))
print("Raices de p2")
print(raices(p2))
print("Raices de p3")
print(raices(p3))
print("Raices de p4")
print(raices(p4))