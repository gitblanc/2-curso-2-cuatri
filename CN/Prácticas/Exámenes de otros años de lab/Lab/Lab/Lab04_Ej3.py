# -*- coding: utf-8 -*-
"""
Created on Tue Mar 9 15:29:52 2021

@author: rodri
"""
import numpy as np
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

def newton(f,df,x0,tol=1.e-6,maxiter=100):
   xk=x0-(f(x0)/df(x0)) 
   count=1
   while count!=maxiter and abs(f(xk))>tol :
       xk=x0-(f(x0)/df(x0))
       x0=xk
       count=count+1
   xk=x0-(f(x0)/df(x0)) 
   return xk,count

f = lambda x : x**5 - 3 * x**2 + 1.6   # definimos la función
df= lambda x : 5*x**4 - 6*x
c = busquedaIncremental(f, -1, 1.5, 25) 
for i in range(len(c)):
    a,b=c[i]
    print(newton(f,df,a))