using System;
using System.Collections.Generic;

namespace ItTutorial.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            QuizPergunta = new HashSet<QuizPergunta>();
        }

        public int QuizId { get; set; }
        public int LinguagemId { get; set; }
        public string DescricaoQuiz { get; set; }
        public string Descricao { get; set; }

        public ICollection<QuizPergunta> QuizPergunta { get; set; }
    }
}
