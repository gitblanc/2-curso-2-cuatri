# -*- coding: utf-8 -*-
"""
Created on Tue Mar 16 15:31:05 2021

@author: rodri
"""
import numpy as np
import scipy.optimize as op
import matplotlib.pyplot as plt
import sympy as sym

x = sym.Symbol('x', real=True)
f_sim   = x**2 + sym.log(2*x+7)*sym.cos(3*x)+0.1
df_sim  = sym.diff(f_sim,x)
d2f_sim = sym.diff(df_sim,x)

f   = sym.lambdify([x], f_sim,'numpy') 
df  = sym.lambdify([x], df_sim,'numpy') 
d2f = sym.lambdify([x], d2f_sim,'numpy') 

x= np.linspace(-1,3,100)
# dibujamos la primera derivada
plt.figure()
plt.plot(x,df(x))
plt.plot(x,0*x,'k')
plt.title('Funcion derivada de f')
plt.show()

r= np.zeros(100) 
count=0
for i in range(-1,4):
      r[count]=op.newton(df,i)
      count=count+1
r=r[:count]
print("EXTREMOS")
print(r)
v=np.zeros(3) ## vector para los minimos
v[0]=r[0]
v[1]=r[2]
v[2]=r[4]
r[2]=0
r[0]=0
r[4]=0
w=np.zeros(2) ## vector para los maximos
w[0]=r[1]
w[1]=r[3]
y= np.linspace(-2,4,100)
## dibujamos la funcion f
plt.figure()
plt.plot(y,f(y))
plt.plot(y,0*y,'k')
plt.plot(w,f(w),"ro")
plt.plot(v,f(v),"v")
plt.title('Funcion de f')
plt.show()
# dibujamos la segunda derivada
plt.figure()
plt.plot(x,d2f(x))
plt.plot(x,0*x,'k')
plt.title('Funcion derivada segunda de f')
plt.show()

print("PUNTOS DE INFLEXION EN [-1,4]")
w=np.zeros(5)
w[0]=op.newton(d2f,1)
w[1]=op.newton(d2f,0.5)
w[2]=op.newton(d2f,2)
w[3]=op.newton(d2f,3)
w[4]=op.newton(d2f,4)
print(w)
## dibujamos la funcion f
y= np.linspace(-1,4,100)
plt.figure()
plt.plot(y,f(y),label="Funcion f")
plt.plot(y,0*y,'k')
plt.plot(w,f(w),"ro",label="puntos de inflexion")
plt.title('Funcion de f')
plt.legend()
plt.show()


