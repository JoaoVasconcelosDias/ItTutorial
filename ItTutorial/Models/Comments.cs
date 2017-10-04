using System;
using System.Collections.Generic;

namespace ItTutorial.Models
{
    public partial class Comments
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string AspNetUsersId { get; set; }
        public int PostsId { get; set; }

        public AspNetUsers AspNetUsers { get; set; }
        public Posts Posts { get; set; }
    }
}
