using System;
using System.Collections.Generic;

namespace ProblemaOchoReinas
{
    class Program
    {
        static void Main(string[] args)
        {   
            const int TOTAL_ALGORITMOS = 30;
            List<int> totalIteraciones = new List<int>();
            List<int> totalEvaluaciones = new List<int>();
            int ceroVeces = 0;
            for(int i = 0; i < TOTAL_ALGORITMOS; i++)
            {
                Console.WriteLine("\n\n\n ---------- INICIO DEL ALGORITMO NÚMERO {0} ----------", i);
                AlgoritmoEvolutivo algoritmo = new AlgoritmoEvolutivo();
                algoritmo.Iniciar();
                totalIteraciones.Add(algoritmo.TotalIteraciones);
                totalEvaluaciones.Add(algoritmo.TotalEvaluaciones);
                if (algoritmo.TotalIteraciones == 0)
                    ceroVeces++;
                Console.WriteLine("---------- FIN DEL ALGORITMO ----------");
            }

            Console.WriteLine("\nEstadisticas de las {0} iteraciones del algoritmo:", TOTAL_ALGORITMOS);

            for(int i = 0; i < TOTAL_ALGORITMOS; i++)
            {
                Console.WriteLine("EVALUACION {0}: Iteraciones = {1}, Evaluaciones = {2}",i , totalIteraciones[i], totalEvaluaciones[i]);
            }

            decimal promedio =  ((decimal) ceroVeces /  (decimal) TOTAL_ALGORITMOS) * 100;
            Console.WriteLine("Veces 0 iteraciones = {0} - %{1}", ceroVeces, promedio);

        }

        
    }
}
