# -*- coding: utf-8 -*-
"""
Taylor
e^x aprox 1 + x/1! + x^2/2! + x^3/3!
"""
import numpy as np
#%%
x0 = -0.4
grado = 10

polinomio = 0.
factorial= 1.

for i in range(grado+1):
    sumando = x0**i/factorial
    polinomio += sumando
    factorial *= i+1
    
    
print('P3(0.5)     = ', polinomio)
print('np.exp(0.5) = ', np.exp(x0))

