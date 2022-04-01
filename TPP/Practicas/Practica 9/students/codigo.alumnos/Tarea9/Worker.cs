﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica9
{
    internal class Worker
    {
        private BitcoinValueData[] vector;

        private int fromIndex, toIndex;

        private long valorFijado;

        private long result;

        internal long Result
        {
            get { return this.result; }
        }

        internal Worker(BitcoinValueData[] vector, int fromIndex, int toIndex, long valorFijado)
        {
            this.vector = vector;
            this.fromIndex = fromIndex;
            this.toIndex = toIndex;
            this.valorFijado = valorFijado;
        }

        internal void Compute()
        {
            this.result = 0;
            for (int i = this.fromIndex; i <= this.toIndex; i++)
                if(this.vector[i].Value > valorFijado)//si el valor supera la cifra establecida o es igual
                    ++this.result;
        }
    }
}
