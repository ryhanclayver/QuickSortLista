using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace QuickSortLista_2_
{
    internal class Program
    {
       public static int comparacoes = 0;
       public  static int trocas = 0;
        static void Main(string[] args)
        {
            TimeSpan tempoDecorrido = MedirTempoDecorrido(() =>
            {
                //LinkedList<int> lista = GerarListaAleatoria(10);
                //LinkedList<int> lista = GerarListaAleatoria(100);
                //LinkedList<int> lista = GerarListaAleatoria(500);
                LinkedList<int> lista = GerarListaAleatoria(1000);
                Console.WriteLine("Lista original:");
            ImprimirLista(lista);

            QuicksortIterativo(lista);

            Console.WriteLine("Lista ordenada:");
            ImprimirLista(lista);

            Console.WriteLine("O numero de comparações realizados: " + comparacoes);
            Console.WriteLine("O número de trocas realizadas: " + trocas);

            });

            Console.WriteLine("Tempo de execução: " + tempoDecorrido.TotalMilliseconds.ToString("F2") + " milessegundos");
            Console.ReadKey();
        }
        public static TimeSpan MedirTempoDecorrido(Action acao)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            acao.Invoke();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        static void QuicksortIterativo(LinkedList<int> lista)
        {
            if (lista == null || lista.Count <= 1)
                return;

            var pilha = new Stack<LinkedListNode<int>[]>();

            var inicio = lista.First;
            var fim = lista.Last;

            pilha.Push(new[] { inicio, fim });

            while (pilha.Count > 0)
            {
                var indices = pilha.Pop();
                inicio = indices[0];
                fim = indices[1];

                if (inicio != null && fim != null && inicio != fim && inicio.Previous != fim)
                {
                    var pivo = Particionar(lista, inicio, fim);

                    if (pivo?.Previous != null && pivo.Previous != inicio)
                    {
                        pilha.Push(new[] { inicio, pivo.Previous });
                    }

                    if (pivo?.Next != null && pivo.Next != fim)
                    {
                        pilha.Push(new[] { pivo.Next, fim });
                    }
                }
            }
        }

       public static LinkedListNode<int> Particionar(LinkedList<int> lista, LinkedListNode<int> inicio, LinkedListNode<int> fim)
        {
            var pivo = fim.Value;
            var i = inicio.Previous;

            for (var j = inicio; j != fim; j = j.Next)
            {
                comparacoes++;
                if (j.Value < pivo)
                {
                    trocas++;
                    i = (i == null) ? inicio : i.Next;
                    TrocarValores(i, j);
                }
            }

            i = (i == null) ? inicio : i.Next;
            TrocarValores(i, fim);

            return i;
        }

        static void TrocarValores(LinkedListNode<int> node1, LinkedListNode<int> node2)
        {
            var temp = node1.Value;
            node1.Value = node2.Value;
            node2.Value = temp;
        }

        static void ImprimirLista(LinkedList<int> lista)
        {
            foreach (var elemento in lista)
            {
                Console.Write(elemento + " ");
            }
            Console.WriteLine();
        }

        static LinkedList<int> GerarListaAleatoria(int tamanho)
        {
            Random random = new Random();
            LinkedList<int> lista = new LinkedList<int>();

            for (int i = 0; i < tamanho; i++)
            {
                lista.AddLast(random.Next(100));
            }

            return lista;
        }
    }
}