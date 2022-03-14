# -*- coding: utf-8 -*-
"""
Created on Tue Apr 20 15:42:22 2021

@author: rodri
"""

import numpy as np
import sympy as sym
import matplotlib.pyplot as plt
from numpy.linalg import norm


h=0.1
x = sym.Symbol('x', real=True)
f_sim   = sym.log(x)
df_sim  = sym.diff(f_sim,x)
df_p_sim = (sym.log(x+h)-sym.log(x))/h
df_r_sim = (sym.log(x)-sym.log(x-h))/h
df_c_sim = (sym.log(x+h)-sym.log(x-h))/(2*h)

f   = sym.lambdify([x], f_sim,'numpy') 
df  = sym.lambdify([x], df_sim,'numpy') 

df_p= sym.lambdify([x],df_p_sim,'numpy')
df_r= sym.lambdify([x],df_r_sim,'numpy')
df_c= sym.lambdify([x],df_c_sim,'numpy')

x= np.linspace(1,2,100)
xp = np.arange(1,2,h)
xr = np.arange(1+h,2+h,h)
xc = np.arange(1+h,2,h)
# dibujamos la primera derivada
plt.figure()
plt.plot(x,df(x),'--')
plt.plot(xp,df_p(xp))
plt.plot(xr,df_r(xr))
plt.plot(xc,df_c(xc))
plt.plot
plt.title('Derivada de f(x) = ln(x) y sus aproximaciones(h=0.1)')
plt.show()

plt.figure()
plt.plot(xp,np.abs(df(xp)-df_p(xp)))
plt.plot(xr,np.abs(df(xr)-df_r(xr)))
plt.plot(xc,np.abs(df(xc)-df_c(xc)))
plt.plot
plt.title('Errores(h=0.1')
plt.show()

#error_p=np.abs(df(xp)-df_p(xp))
print('h = ',h)
errorG_p=norm(df(xp)-df_p(xp)) / norm(df(xp))
print('ErrorG_p = ',errorG_p)   
errorG_r=norm(df(xr)-df_r(xr)) / norm(df(xr))
print('ErrorG_r = ',errorG_r)  
errorG_c=norm(df(xc)-df_c(xc)) / norm(df(xc))  
print('ErrorG_c = ',errorG_c)  

h=0.01
x = sym.Symbol('x', real=True)
f_sim   = sym.log(x)
df_sim  = sym.diff(f_sim,x)
df_p_sim = (sym.log(x+h)-sym.log(x))/h
df_r_sim = (sym.log(x)-sym.log(x-h))/h
df_c_sim = (sym.log(x+h)-sym.log(x-h))/(2*h)

f   = sym.lambdify([x], f_sim,'numpy') 
df  = sym.lambdify([x], df_sim,'numpy') 

df_p= sym.lambdify([x],df_p_sim,'numpy')
df_r= sym.lambdify([x],df_r_sim,'numpy')
df_c= sym.lambdify([x],df_c_sim,'numpy')
x= np.linspace(1,2,100)
xp = np.arange(1,2,h)
xr = np.arange(1+h,2+h,h)
xc = np.arange(1+h,2,h)
# dibujamos la primera derivada
plt.figure()
plt.plot(x,df(x),'--')
plt.plot(xp,df_p(xp))
plt.plot(xr,df_r(xr))
plt.plot(xc,df_c(xc))
plt.plot
plt.title('Derivada de f(x) = ln(x) y sus aproximaciones(h=0.01)')
plt.show()

plt.figure()
plt.plot(xp,np.abs(df(xp)-df_p(xp)))
plt.plot(xr,np.abs(df(xr)-df_r(xr)))
plt.plot(xc,np.abs(df(xc)-df_c(xc)))
plt.plot
plt.title('Errores(h=0.01')
plt.show()

#error_p=np.abs(df(xp)-df_p(xp))
print()
print('h = ',h)
errorG_p=norm(df(xp)-df_p(xp)) / norm(df(xp))
print('ErrorG_p = ',errorG_p)   
errorG_r=norm(df(xr)-df_r(xr)) / norm(df(xr))
print('ErrorG_r = ',errorG_r)  
errorG_c=norm(df(xc)-df_c(xc)) / norm(df(xc))  
print('ErrorG_c = ',errorG_c)  

 

 

