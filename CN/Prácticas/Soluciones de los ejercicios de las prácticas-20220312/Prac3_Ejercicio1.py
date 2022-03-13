#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Horner
"""
import numpy as np
#%%
def horner(p,x0):
    n = len(p)
    q = np.zeros(n)
    q[0] = p[0]
    for i in range(1,n):
        q[i] = q[i-1]*x0 + p[i]
    cociente = q[:-1]
    resto = q[-1]    
    return cociente, resto
#%%
p0 = np.array([1.,2,1])
p1 = np.array([1, -1, 2, -3,  5, -2])
p2 = np.array([1., -1, -1, 1, -1, 0, -1, 1])
x0 = 1
x1 = 1.
x2 = -1.

c, r = horner(p0,x0)
print('Coeficientes de Q = ', c)
print('P0(1) = ', r)

print('\n')
c, r = horner(p1,x1)
print('Coeficientes de Q = ', c)
print('P1(1) = ', r)

print('\n')

c, r = horner(p2,x2)
print('Coeficientes de Q = ', c)
print('P2(-1) = ', r)