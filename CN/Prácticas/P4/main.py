#Bolzano
#Dada f [a,b] de R
# Si f es continua en [a,b] y f(a)*f(b)<0 existe al menos un c perteneciente a (a,b) tal que f(c) = 0
import numpy as np
import matplotlib.pyplot as plt
f = lambda x : x**5 - 3 * x**2 + 1.6   # definimos la función
x = np.linspace(-1,1.5)                # definimos un vector con 50 elementos en (-1,1.5)
ox = 0*x                               # definimos un vector de ceros del tamaño de x

plt.figure()
plt.plot(x,f(x))                   # dibujamos la función
plt.plot(x,ox,'k-')                # dibujamos el eje X
plt.show()                         # hemos acabado este gráfico


