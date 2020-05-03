using System.Collections.Generic;
using System;

namespace ProblemaOchoReinas
{
    public class Tablero 
    {
        public List<int> PosicionesDeReinas = new List<int>();
        public int Puntuacion;
        

        public bool ChocaEnDiagonal(int reinaActual, int reinaEvaluada)
        {
            bool choca = false;
            int valorReinaActual = PosicionesDeReinas[reinaActual];
            int valorReinaEvaluada = PosicionesDeReinas[reinaEvaluada];
            
            choca = reinaActual - valorReinaActual == reinaEvaluada - valorReinaEvaluada || reinaActual + valorReinaActual == reinaEvaluada + valorReinaEvaluada;

            return choca;
        }
        

        public void Imprimir(bool enLista = false)
        {
            if (enLista)
            {
                Console.Write('{');
                for(int i = 0; i < PosicionesDeReinas.Count; i++)
                {
                    if (i == PosicionesDeReinas.Count - 1)
                    {
                        Console.Write(PosicionesDeReinas[i]);   
                    }
                    else
                    {
                        Console.Write(PosicionesDeReinas[i] + ", ");
                    }

                }
                
                Console.WriteLine('}');
            }
            else
            {
                Console.WriteLine("\tC0\tC1\tC2\tC3\tC4\tC5\tC6\tC7");
                int i = 0;
                foreach (int reina in PosicionesDeReinas)
                {
                    Console.Write("    " + i + " |\t");
                    i++;
                    for (int j = 0; j < reina; j++)
                    {
                        Console.Write('\t');
                    }
                    Console.WriteLine(reina);
                }
            }

        }
    }
}