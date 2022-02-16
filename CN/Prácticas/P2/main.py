import numpy as np
import matplotlib.pyplot as plt

# Gráfica de una función exponencial con maya gruesa

f = lambda x: np.exp(x)  # define la funcion f
x = np.linspace(-1, 1, 5)  # vector de 5 coordenadas entre -1 y 1
y = f(x)  # evalúa f en x y almacena en y

plt.figure()  # abre una nueva ventana gráfica
plt.plot(x, y, label="f")  # etiqueta con leyenda y dibuja
ox = 0 * x
plt.plot(x, ox, "k", label="Eje OX")
plt.legend()
plt.title("Ejemplo dibujo función f")
# plt.plot(x, y)  # dibuja f
plt.grid()  # malla de fondo
plt.show()  # lo muestra
