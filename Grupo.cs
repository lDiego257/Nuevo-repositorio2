using System;
using System.Collections.Generic;
using System.IO;

namespace Proshecto
{
    public class Grupo
    {
        private int id;
        private Queue<string> estudiantes = new Queue<string>();
        private Queue<string> temas = new Queue<string>();

        public int Id { get => id; set => id = value; }
        public Queue<string> Estudiantes { get => estudiantes; set => estudiantes = value; }
        public Queue<string> Temas { get => temas; set => temas = value; }
    }
}