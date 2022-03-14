# -*- coding: utf-8 -*-
"""
Created on Tue Apr  6 13:14:33 2021

@author: rodri
"""
import numpy as np
import matplotlib.pyplot as plt
    
def lagrange_fund(k,x,z):
   x0=x[0]
   xk=x[k]
   if k !=0:
      lk=(z-x0)/(xk-x0)
   else:
       lk=1
   for i in range(1,len(x)):
       if i==k:
           lk=lk
       else:    
           lk=lk*(z-x[i])/(xk-x[i])
   return lk
    
def lagrange_fundamental(k,x,z):
     n=np.zeros(len(z))
     for j in range(len(z)):
         n[j]=lagrange_fund(k, x, z[j])
     return n


x = np.array([-1., 0, 2, 3, 5])
k = 2
z = np.array([1.3, 2.1, 3.2])
yz = lagrange_fundamental(k,x,z)
print(yz)
print()
print(np.eye(len(x)))
y = np.array([ 1., 3, 4, 3, 1])
xp = np.linspace(min(x),max(x))
p  = np.polyfit(x,y,len(x)-1)




plt.figure()
plt.plot(lagrange_fundamental(1, x, z))
plt.plot(x,0*x,'k')
plt.title('L1')
plt.show()

plt.figure()
plt.plot(lagrange_fundamental(2, x, z))
plt.plot(x,0*x,'k')
plt.title('L2')
plt.show()

plt.figure()
plt.plot(lagrange_fundamental(3, x, z))
plt.plot(x,0*x,'k')
plt.title('L3')
plt.show()

plt.figure()
plt.plot(lagrange_fundamental(4, x, z))
plt.plot(x,0*x,'k')
plt.title('L4')
plt.show()