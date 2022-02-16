import time
import numpy as np


f = lambda x: np.exp(x)  # define la funcion f
x = np.linspace(-1, 1, 5)  # vector de 5 coordenadas entre -1 y 1
y = f(x)  # evalúa f en x y almacena en y

# Sin vectorización----------------------------------------
z = np.linspace(-10, 10, 1000000)  # vector con un millón de elementos
zy = np.zeros_like(z)  # vector de ceros con la misma estructura que z
t = time.time()
for i in range(len(z)):
    zy[i] = f(z[i])
t1 = time.time() - t
print('Sin vectorización: ', t1, ' segundos')

# Con vectorización----------------------------------------
t = time.time()
zy = f(z)
t1 = time.time() - t
print('Con vectorización: ', t1, ' segundos')
