# -*- coding: utf-8 -*-
"""
Created on Tue Mar 9 15:29:52 2021

@author: rodri
"""
import numpy as np
#%%

def busquedaIncremental(f,a,b,n):
    x=np.linspace(a,b,n+1)
    intervalos=np.zeros((n,2))
    contador=0
    for i in range(len(x)-1):
        a=x[i]
        b=x[i+1]
        if(f(a)*f(b)<0):
            intervalos[contador]=[a,b]
            contador=contador+1
    intervalos=intervalos[:contador,:]
    return intervalos
        

def biseccion(f,a,b,tol=1.e-6,maxiter=100):
    count=0
    ak=a
    bk=b
    xk=(ak+bk)/2
    while count!=maxiter and abs(bk-ak)>tol :
        xk=(ak+bk)/2 
        if(f(ak)*f(xk)<0):
            bk=xk
            count=count+1
        elif(f(xk)*f(bk)<0):
            ak=xk
            count=count+1
        else:
            break
            
    return xk,count

f = lambda x : x**5 - 3 * x**2 + 1.6   # definimos la función
c = busquedaIncremental(f, -1, 1.5, 25) 
for i in range(len(c)):
    a,b=c[i]
    print(biseccion(f,a,b))