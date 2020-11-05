using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;

namespace Proshecto
{
    class Program
    {
        static void Main(string[] args)
        {
            //Estructuras de datos a manejar
            Console.WriteLine();
            
            Queue<string> estudiantes = new Queue<string>();
            Queue<string> temas = new Queue<string>();
            Queue<Grupo> grupos = new Queue<Grupo>();

            //Llenado de estructuras
            LlenarCola(estudiantes, args[0]); //0
            LlenarCola(temas, args[1]); //1
            for (int i = 0; i < int.Parse(args[2]); i++)
            {
                Grupo g = new Grupo();
                g.Id = i + 1;
                grupos.Enqueue(g);
            }

            //Randomizar
            estudiantes = Shuffle(estudiantes);
            temas = Shuffle(temas);

            //Revisar metricas
            string x = EsPosible(estudiantes.Count, temas.Count, int.Parse(args[2])); //AQUI VA EL ARG 2

            //repartidor
            if (x == "s")
            {
                //aisgnar los estudiantes
                grupos = Shuffle(grupos);
                while (estudiantes.Count != 0)
                {
                    //asignar los estudiantes
                    foreach (var item in grupos)
                    {
                        if (estudiantes.Count != 0)
                        {
                            string vEstudiante = estudiantes.Dequeue();
                            item.Estudiantes.Enqueue(vEstudiante);
                        }

                    }
                }
                //Asignar los temas
                grupos = Shuffle(grupos);
                while (temas.Count != 0)
                {
                    foreach (var item in grupos)
                    {
                        if (temas.Count != 0)
                            item.Temas.Enqueue(temas.Dequeue().ToString());
                    }
                }
                grupos = new Queue<Grupo>(grupos.OrderBy(x => x.Id));

                //Mostrar datos
                foreach (var item in grupos)
                {
                    int i = 0;
                    Console.WriteLine($"Grupo {item.Id}, integrantes({item.Estudiantes.Count}): ");
                    foreach (var item2 in item.Estudiantes)
                    {
                        i++;
                        Console.WriteLine($" {i}. {item2}");
                    }
                    foreach (var item2 in item.Temas)
                    {
                        Console.Write($"{item2}| ");
                    }
                    Console.WriteLine("\n-----------------------------------\n");
                }
            }
            else
                Console.WriteLine(x);


        }

        //algunos metodos
        public static void LlenarCola(Queue<string> cola, string direccion)
        {
            using (StreamReader SR = new StreamReader(direccion))
            {
                string line;
                while ((line = SR.ReadLine()) != null)
                {
                    cola.Enqueue(line);
                }
            }
        }

        public static string EsPosible(int estudiantes, int temas, int grupos)
        {
            if (grupos > estudiantes)
                return "Hay mas grupos que estudiantes. No existen suficientes estudiantes para llenar todos los grupos";

            if (grupos > temas)
                return "Hay mas grupos que temas. No existen suficientes temas";

            return "s";
        }
        public static Queue<T> Shuffle<T>(Queue<T> cola)
        {
            var rnd = new Random();
            return new Queue<T>(cola.OrderBy(x => rnd.Next()));
        }
    }
}
