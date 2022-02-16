import numpy as np
import matplotlib.pyplot as plt

"""
Utilizando un bucle while, escribir un programa, que utilizando el 
desarrollo de McLaurin para la función  𝑓(𝑥)=𝑒𝑥 , calcule su valor 
aproximado en el punto x0 = -0.4. El criterio de parada será que el
valor del último sumando añadido en valor absoluto es menor que una 
tolerancia tol=1.e-8 y que el número máximo de sumandos es 100, es 
decir maxNumSum=100. Comparar su valor con el valor de la función 
lambda f. Dar el número de sumandos utilizados.

Nota:

Usar algún operador lógico and, or, not, para la condición del while.
El valor absoluto de un número real a puede obtenerse con np.abs(a)
Recordamos que el desarrollo de McLaurin para la función  𝑓(𝑥)=𝑒𝑥  es
𝑒𝑥=1+𝑥11!+𝑥22!+𝑥33!+𝑥44!+𝑥55!+⋯
"""


i = 0
polinomio = 0.
factorial = 1
x0 = -0.4
tol = 1.e-8
cota = tol + 1  # para que entre en el bucle

while i < 100 and cota >= tol:
    sumando = x0 ** i / factorial
    polinomio += sumando
    cota = abs(sumando)
    factorial *= i + 1
    i += 1

print("Valor de la función en -0.4 = ", polinomio)
print("Valor de la aproximación en -0.4 = ", polinomio)
print("Número de iteraciones = ", i)