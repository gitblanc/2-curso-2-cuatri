import numpy as np
import matplotlib.pyplot as plt
"""
Modificar el programa de forma que calcule simultáneamente la aproximación de la función de los 50 
valores contenidos en x = np.linspace(-1,1). El criterio de parada será ahora que, el máximo del último
 sumando en valor absoluto (ahora tenemos 50  𝑥0  distintas y 50 últimos sumandos distintos), sea menor 
 que que una tolerancia tol=1.e-8 y que el número máximo de sumandos es 100, maxNumSum=100.

Convertir este programa en una función funExp(x, tol, maxNumSum) cuyos argumentos de entrada sean 
el array numpy x que contiene los 50 valores, tol y maxNumSum y cuyo argumento de salida sea un array 
numpy y que contenga los valores aproximados de la función obtenidos con el polinomio de McLaurin. Utilizar 
esta función para dibujar la función. Dibujar también la función utilizando la función lambda f definida a 
partir de np.exp(x)

Notas:

np.max(np.abs(sumando)) nos da el valor máximo de los valores absolutos de los valores contenidos 
en un array numpy llamado sumando.
Para dibujar una línea gruesa amarilla plt.plot(x,y,'y', linewidth = 4)
Para dibujar una línea azul discontinua plt.plot(x,y,'b--')
"""

def funExp(x, tol, maxNumSum):
    x0 = x
    i = 0
    polinomio = 0.
    factorial = 1
    cota = tol + 1  # para que entre en el bucle

    while i < maxNumSum and cota >= tol:
        sumando = x0 ** i / factorial
        polinomio += sumando
        cota = np.max(abs(sumando))
        factorial *= i + 1
        i += 1
    return polinomio, i


toler = 1.e-8
maxNumSum = 100
f = lambda xs: np.exp(xs)
x = np.linspace(-1,1)
y = funExp(x, toler, maxNumSum)
y0 = y[0]
plt.plot(x, f(x), "y", linewidth = 4, label = "f")
plt.plot(x, y0, "b--", label = "Aproximación de f")
plt.plot(x, 0*x, "k")
plt.title("Aproximación de f con el polinomio de McLaurin")
plt.grid()
plt.show()


