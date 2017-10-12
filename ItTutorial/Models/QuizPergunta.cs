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
        public string PerguntaExtenso { get; set; }
        public string PerguntaOpcao1 { get; set; }
        public string PerguntaOpcao2 { get; set; }
        public string PerguntaOpcao3 { get; set; }
        public string PerguntaOpcao4 { get; set; }
        public string RespostaCerta { get; set; }
        public string RespostaUser { get; set; }

        public Quiz Quiz { get; set; }
        public ICollection<Resultados> Resultados { get; set; }
    }
}
