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
        public string Question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Certa { get; set; }
<<<<<<< HEAD
        public string RespostaUser { get; set; }

=======
        public string RespostaUser { get; set; }     
>>>>>>> 41556fc565f133dad3eeaeb3f1f2e0b5fdb98c67
        public Quiz Quiz { get; set; }
        public ICollection<Resultados> Resultados { get; set; }
    }
}
