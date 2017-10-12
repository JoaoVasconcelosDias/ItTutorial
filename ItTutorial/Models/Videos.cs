using System;
using System.Collections.Generic;

namespace ItTutorial.Models
{
    public partial class Videos
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        public int LinguagemId { get; set; }
        public string Notes { get; set; }
    }
}
