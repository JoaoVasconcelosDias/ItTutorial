using System;
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
        public string Pergunta { get; set; }
        public string Opcao1 { get; set; }
        public string Opcao2 { get; set; }
        public string Opcao3 { get; set; }
        public string Opcao4 { get; set; }
        public string Certa { get; set; }
<<<<<<< HEAD
        public string RespostaUser { get; set; }
      
=======

>>>>>>> 387311d6fd5b569fb79362a83adc6943c1b04971
        public Quiz Quiz { get; set; }
        public ICollection<Resultados> Resultados { get; set; }
    }
}
