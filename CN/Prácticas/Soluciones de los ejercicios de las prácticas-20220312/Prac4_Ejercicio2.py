## module biseccion2
# -*- coding: utf-8 -*-
"""
raiz, iteraciones = biseccion2(f,x1,x2,tol=1.e-6)
Encuentra la raíz de f(x)=0 utilizando el método de bisección.
La raíz debe estar en el intervalo (x1,x2)
"""

def biseccion(f,a,b,tol=1.e-6,n=100):
    if f(a)*f(b) > 0.0: 
        print('La función tiene el mismo signo en los extremos')
    
    for i in range(n):
        m = 0.5*(a + b)
                
        if f(a)*f(m) < 0: 
            b = m;
        elif f(m)*f(b) < 0:  
            a = m
        else:
            break
        
        if b - a < tol:
            break
    
    return m,i+1 # i+1 porque el cero también cuenta
#----------------------------------------------------------
# MAIN
#----------------------------------------------------------
f = lambda x: x**5 - 3 * x**2 + 1.6     # definimos la función f

a = -0.7; b = -0.6
sol1, i1 = biseccion(f,a,b)
print(sol1, i1)


a = 0.8; b = 0.9
sol2, i2 = biseccion(f,a,b)
print(sol2, i2)

a = 1.2; b = 1.3
sol3, i3 = biseccion(f,a,b)
print(sol3, i3)


#import scipy.optimize as op
#r = [-0.7, 0.8, 1.2]
#sol = op.newton(f,r)

       
        

