﻿using System;
using System.Collections.Generic;

namespace ItTutorial.Models
{
    public partial class QuizPergunta
    {
        public QuizPergunta()
        {
            Resultados = new HashSet<Resultados>();
        }

        public int QuizId { get; set; }
        public int PerguntaId { get; set; }
        public string Question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Certa { get; set; }
        public string RespostaUser { get; set; }

        public Quiz Quiz { get; set; }
        public ICollection<Resultados> Resultados { get; set; }
    }
}
