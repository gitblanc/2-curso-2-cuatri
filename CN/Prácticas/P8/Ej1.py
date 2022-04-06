import numpy as np
import sympy as sym
import matplotlib.pyplot as plt
from numpy.linalg import norm


def punto_medio(f,a,b):
    I=(b-a)*f((a+b)/2)
    return I

def trapecio(f,a,b):
    I = ((b-a)/2)*(f(a)+f(b))
    return I

def simpson(f,a,b):
    I = ((b-a)/6)*(f(a)+4*f((a+b)/2)+f(b))
    return I

f = lambda x: np.log(x)

x = sym.Symbol('x', real=True)
f_sim = sym.log(x)
I_exacta = sym.integrate(f_sim,(x,1,3))
I_exacta = float(I_exacta)


print('1a) El valor aproximado es ', punto_medio(f, 1, 3))
print('1a)El valor exacto es ', I_exacta)
print()
print('1b) El valor aproximado es ', trapecio(f, 1, 3))
print('1b) El valor exacto es ', I_exacta)
print()
print('1c) El valor aproximado es ', simpson(f, 1, 3))
print('1c) El valor exacto es ', I_exacta)

