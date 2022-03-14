# -*- coding: utf-8 -*-
"""
nociones de numpy
"""
import numpy as np

#%%
print(np.pi)
#%%vector numpy
a= np.array([1,2,3,4])
a1=np.array([1.0,2,3,4])
a2= np.array([[1.0],[2],[3],[4]])
#%% matriz numpy
b= np.array([(1.5,2,3),(4,5,6)])

#%% Generar arrays
a= np.zeros(3)
c= np.zeros((3,4))
b= np.ones(3)
d = np.ones((3,4))
#%%
a = np.array([1.0,2,3,4])
b = np.array([(1.5,2,3),(4,5,6)])
#%%
a = np.arange(4)
a1= range(4)
a2= np.arange(1,4.)
a4=np.arange(1,4.,2)
a5 = np.arange(10,4.,-2)
#%%
a = np.array([[2,1,3,4],[9,8,5,7],[6,-1,-2,-8],
              [-5,-7,-9,6]])
#%%
a= np.arange(10.)
#%%
a= np.array([[1,2,4],[3,-1,2]])