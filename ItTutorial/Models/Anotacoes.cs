using System;
using System.Collections.Generic;

namespace ItTutorial.Models
{
    public partial class Anotacoes
    {
        public int Iduser { get; set; }
        public string Notas { get; set; }

        public User IduserNavigation { get; set; }
    }
}
