using System.Collections.Generic;
using System.Linq;
using System;

namespace ProblemaOchoReinas
{
    public class AlgoritmoEvolutivo 
    {
        private readonly int TOTAL_POBLACION = 100;
        private readonly int MAXIMO_DE_ITERACIONES = 10000;
        private readonly float PROBABILIDAD_DE_MUTACION = 0.8f;
        private readonly int NUMERO_DE_REINAS = 8;
        public List<Tablero> Poblacion = new List<Tablero>();
        public int TotalEvaluaciones = 0;
        public int TotalIteraciones = 0;

        public void Iniciar()
        {
            LlenarPoblacion();
            Console.Write("MEJOR TABLERO EN POBLACION INICIAL: ");
            ImprimirMejorTablero();
            Console.WriteLine();

            bool solucionEncontrada = SeEncuentraLaSolucion();;

            while (TotalIteraciones < MAXIMO_DE_ITERACIONES && !solucionEncontrada)
            {
                TotalIteraciones++;
                int padre1 = EscogerMejorDe5Aleatorios();
                int padre2 = EscogerMejorDe5Aleatorios();
                List<List<int>> hijos = CruzarPadres(Poblacion[padre1], Poblacion[padre2]);
                MutarHijos(hijos);
                ReemplazarPoblacion(hijos);

                Console.Write("ITERACION: {0}  ",TotalIteraciones);
                ImprimirMejorTablero();

                solucionEncontrada = SeEncuentraLaSolucion();
            }
        }

        private bool SeEncuentraLaSolucion()
        {
            bool solucion = false;
            for (int i = 0; i < Poblacion.Count; i++)
            {
                if (Poblacion[i].Puntuacion == 0)
                {
                    solucion = true;
                }
            }
            return solucion;
        }

        public List<List<int>> CruzarPadres(Tablero primerPadre, Tablero segundoPadre)
        {
            List<int> padre1 = new List<int>(primerPadre.PosicionesDeReinas);
            List<int> padre2 = new List<int>(segundoPadre.PosicionesDeReinas);
            List<int> hijo1 = new List<int>();
            List<int> hijo2 = new List<int>();
            
            int mitadDeNumeroDeReinas = NUMERO_DE_REINAS / 2;
            for (int i = 0; i < NUMERO_DE_REINAS; i++)
            {
                if (i < mitadDeNumeroDeReinas)
                {
                    hijo1.Add(padre1[i]);
                }
                else
                {
                    if (hijo1.Contains(padre2[i]))
                    {
                        IntercambiarElementosDeListaSinRepetirEnOtraLista(padre2, hijo1, i);
                    }

                    hijo1.Add(padre2[i]);
                }
            }

            for (int i = 0; i < NUMERO_DE_REINAS; i++)
            {
                if (i < mitadDeNumeroDeReinas)
                {
                    hijo2.Add(padre1[i + mitadDeNumeroDeReinas]);
                }
                else
                {
                    hijo2.Add(padre2[i - mitadDeNumeroDeReinas]);
                }
            }

            List<List<int>> hijos = new List<List<int>>();
            hijos.Add(hijo1);
            hijos.Add(hijo2);

            return hijos;
        }

        private void IntercambiarElementosDeListaSinRepetirEnOtraLista(List<int> lista, List<int> listaANoRepetir, int indice)
        {
            for (int i = 0; i < lista.Count / 2; i++)
            {
                if (!listaANoRepetir.Contains(lista[i]))
                {
                    int elementoACambiar = lista[indice];
                    lista[indice] = lista[i];
                    lista[i] = elementoACambiar;
                    break;
                }
            }
        }

        public void MutarHijos(List<List<int>> hijos)
        {
            foreach (List<int> hijo in hijos)
            {
                float aleatorio = (float) new Random().NextDouble();
                if (aleatorio <= PROBABILIDAD_DE_MUTACION)
                {
                    int indiceACambiar = new Random().Next(NUMERO_DE_REINAS);
                    int indiceACambiar2 = new Random().Next(NUMERO_DE_REINAS);

                    int valorACambiar = hijo[indiceACambiar];
                    hijo[indiceACambiar] = hijo[indiceACambiar2];
                    hijo[indiceACambiar2] = valorACambiar;
                }

            }
        }

        public int EscogerMejorDe5Aleatorios()
        {
            int mejor = -1;
            List<int> listaAleatorios = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                int numero = new Random().Next(TOTAL_POBLACION - 1);
                if (listaAleatorios.Contains(numero))
                {
                    i--;
                }
                else
                {
                    listaAleatorios.Add(numero);
                }
            }

            List<int> listaOrdenada;
            listaOrdenada = listaAleatorios.OrderByDescending(o => Poblacion[o].Puntuacion).ToList();
            mejor = listaAleatorios.First();

            return mejor;
        }


        public void ImprimirMejorTablero()
        {
            Tablero mejorTablero = Poblacion.First();
            foreach (Tablero tablero in Poblacion)
            {
                if (tablero.Puntuacion < mejorTablero.Puntuacion)
                {
                    mejorTablero = tablero;
                }
            }

            Console.Write(" Puntuacion: {0}  ", mejorTablero.Puntuacion);
            mejorTablero.Imprimir(true);
        }

        public int CalcularNumeroDeChoques(Tablero tablero)
        {
            TotalEvaluaciones++;
            int choques = 0;
            List<List<int>> listaDeChoquesDeReinas = new List<List<int>>();

            for (int i = 0; i < NUMERO_DE_REINAS; i++)
            {
                listaDeChoquesDeReinas.Add(new List<int>());
            }

            for (int indiceReinaActual = 0; indiceReinaActual < tablero.PosicionesDeReinas.Count; indiceReinaActual++)
            {
                for (int indiceReinaAEvaluar = 0; indiceReinaAEvaluar < tablero.PosicionesDeReinas.Count; indiceReinaAEvaluar++)
                {
                    
                    bool esLaMismaReina = indiceReinaActual == indiceReinaAEvaluar;

                    bool seHaEvaluadoEnReinaActual = listaDeChoquesDeReinas[indiceReinaActual].Contains(indiceReinaAEvaluar);
                    bool seHaEvaluadoEnReinaEvaluada = listaDeChoquesDeReinas[indiceReinaAEvaluar].Contains(indiceReinaAEvaluar);
                    bool noSeHaEvaluado = !seHaEvaluadoEnReinaActual && !seHaEvaluadoEnReinaEvaluada;

                    if (!esLaMismaReina && noSeHaEvaluado) 
                    {
                        if (tablero.ChocaEnDiagonal(indiceReinaActual, indiceReinaAEvaluar))
                        {   
                            choques++;
                            listaDeChoquesDeReinas[indiceReinaActual].Add(indiceReinaAEvaluar);
                            listaDeChoquesDeReinas[indiceReinaAEvaluar].Add(indiceReinaActual);
                        }
                    }
                }
            }

            return choques;
        }

        private void ReemplazarPoblacion(List<List<int>> hijos)
        {
            List<int> indicesDePoblacion = new List<int>();
            for (int i = 0; i < TOTAL_POBLACION; i++)
            {
                indicesDePoblacion.Add(i);
            }

            indicesDePoblacion = indicesDePoblacion.OrderByDescending(i => Poblacion[i].Puntuacion).ToList();
            foreach (List<int> hijo in hijos)
            {
                int indice = indicesDePoblacion.First();
                indicesDePoblacion.Remove(indicesDePoblacion.First());
                Poblacion[indice].PosicionesDeReinas = hijo;
                Poblacion[indice].Puntuacion = CalcularNumeroDeChoques(Poblacion[indice]);
            }
        }

        private void LlenarPoblacion()
        {
            for (int i = 0; i < TOTAL_POBLACION; i++)
            {
                Tablero tablero = new Tablero();
                tablero.PosicionesDeReinas = GenerarListaDeReinasAleatorias();
                tablero.Puntuacion = CalcularNumeroDeChoques(tablero);
                Console.Write("Tablero #{0}, Choques: {1}   ",i,tablero.Puntuacion);
                tablero.Imprimir(true);
                Poblacion.Add(tablero);
            }
        }

        private List<int> GenerarListaDeReinasAleatorias()
        {
            List<int> lista = new List<int>();
            for (int i = 0; i < NUMERO_DE_REINAS; i++)
            {
                lista.Add(i);
            }
            lista = lista.OrderBy(i => new Random().Next()).ToList();

            List<int> listaRandom = new List<int>();
            int j = NUMERO_DE_REINAS;
            while (j > 0)
            {
                int indiceRandom = new Random().Next(j);
                listaRandom.Add(lista[indiceRandom]);
                lista.RemoveAt(indiceRandom);
                j--;
            }

            return listaRandom;
        }
    }
}