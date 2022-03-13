## module newton1
# -*- coding: utf-8 -*-
"""
raiz, iteraciones = newton(f,df,x0,tol=1.e-6,n=100)
Encuentra la raíz de f(x)=0 utilizando el método de newton.
"""

def newton(f,df,x0,tol=1.e-6,n=100):
    
    for i in range(n):
        x1 = x0 - f(x0)/df(x0)
      
        if abs(x1 - x0) < tol:                   
            break
        x0 = x1 

    
    return x1,i+1 # i+1 porque el cero también cuenta
#---------------------------------------------------------
# MAIN
#---------------------------------------------------------
f  = lambda x: x**5 - 3 * x**2 + 1.6 # definimos la función f
df = lambda x : 5*x**4 - 6.*x        # definimos la derivada de la función f


x0 = -0.7
sol1, i1 = newton(f,df,x0)
print(sol1, i1)

x0 = 0.8
sol2, i2 = newton(f,df,x0)
print(sol2, i2)

x0 = 1.2
sol3, i3 = newton(f,df,x0)
print(sol3, i3)

# import scipy.optimize as op
# r = [-0.7, 0.8, 1.2]
# sol = op.newton(f,r)
# print(sol)
       
        

