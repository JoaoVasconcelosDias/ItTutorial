using System;
using System.Collections.Generic;

namespace ItTutorial.Models
{
    public partial class Posts
    {
        public Posts()
        {
            Comments = new HashSet<Comments>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string AspNetUsersId { get; set; }
        public int SubcategoriasId { get; set; }
        public string Post { get; set; }

        public AspNetUsers AspNetUsers { get; set; }
        public Subcategorias Subcategorias { get; set; }
        public ICollection<Comments> Comments { get; set; }
    }
}
