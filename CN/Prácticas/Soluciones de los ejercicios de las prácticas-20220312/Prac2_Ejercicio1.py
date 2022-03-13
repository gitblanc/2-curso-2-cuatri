#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
MacLaurin e^x
"""
import numpy as np
#%% 
f = lambda x: np.exp(x)
#%% Aproximar la función en un punto
x0 = -0.4
suma = 0.
error = np.inf
iteraciones = 0
factorial = 1.
tol = 1.e-8

while (error > tol and iteraciones < 100):
    termino = x0**iteraciones/factorial
    suma += termino
    error = abs(termino)

    iteraciones += 1
    factorial *= iteraciones 
    
print('Valor de la función en -0.4      =', f(x0))
print('Valor de la aproximación en -0.4 =', suma)
print('Número de iteraciones            =', iteraciones) 

