# -*- coding: utf-8 -*-
"""
Created on Tue Mar 16 15:31:05 2021

@author: rodri
"""

import numpy as np
import matplotlib.pyplot as plt
#%%
def secante(f,x0,x1,tol=1.e-6,maxiter=100):
    count=0
    xk=x1-f(x1)*((x1-x0)/(f(x1)-f(x0)))
    while count!=maxiter and abs(f(xk))>tol :
          xk =x1-f(x1)*((x1-x0)/(f(x1)-f(x0)))
          x0=x1
          x1=xk
          count=count+1
    xk =x1-f(x1)*((x1-x0)/(f(x1)-f(x0)))
    return xk,count
    



r= np.zeros(3) 
a,b=[-0.7, -0.6]
d,e=[ 0.8,  0.9]
k,g=[ 1.2,  1.3]
x= np.linspace(-1,1.5,100)
f=lambda x: x**5 -3*(x**2) +1.6
p,z=secante(f,a,b)

r[0],q=secante(f,a,b)
r[1],q=secante(f,d,e)
r[2],q=secante(f,k,g)



plt.figure()
plt.plot(x,f(x))
plt.plot(x,0*x,'k')
plt.plot(r,r*0,'ro')
plt.title('F')
plt.show()


