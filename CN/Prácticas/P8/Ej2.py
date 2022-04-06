import numpy as np
import sympy as sym

def punto_medio_comp(f,a,b,n):
    h=(b-a)/n
    count=0
    xi_1=a
    for i in range(1,n+1):
        xi=a+i*h
        Xi=(xi_1+xi)/2
        xi_1=xi
        count=f(Xi)+count
    I=h*count
    return I

def trapecio_comp(f,a,b,n):
    h=(b-a)/n
    count=0
    for i in range(1,n):
        xi=a+i*h
        count=f(xi)+count
    I = (h/2)*(f(a)+f(b))+h*count
    return I

def simpson_comp(f,a,b,n):
    h=(b-a)/n
    count=0
    xi_1=a
    for i in range(1,n+1):
        xi=a+i*h
        Xi=(xi_1+xi)/2
        count=f(xi_1)+4*f(Xi)+f(xi)+count
        xi_1=xi
    I = (h/6)*count
    return I

f = lambda x: np.log(x)

x = sym.Symbol('x', real=True)
f_sim = sym.log(x)
I_exacta = sym.integrate(f_sim,(x,1,3))
I_exacta = float(I_exacta)
print('El valor aproximado es ', punto_medio_comp(f, 1, 3, 5))
print('El valor exacto es ', I_exacta)
print()
print('El valor aproximado es ', trapecio_comp(f, 1, 3, 5))
print('El valor exacto es ', I_exacta)
print()
print('El valor aproximado es ', simpson_comp(f, 1, 3, 5))
print('El valor exacto es ', I_exacta)