import numpy as np
import matplotlib.pyplot as plt
import pandas as pd

df = pd.read_csv('http://www.unioviedo.es/compnum/laboratorios_py/new/cars.csv',sep=',')
df2 = pd.read_csv('cars.csv',sep=',') # lee datos separados por comas
print(df)
print(df2)

def estimatePower(weight,horsepower):
    x = weight
    y = horsepower
    pol1 = np.polyfit(x, y)
