using System;
using System.Collections.Generic;

namespace ItTutorial.Models
{
    public partial class AspNetUserLogins
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public string UserId { get; set; }
        public string Nome { get; set; }

        public AspNetUsers User { get; set; }
    }
}
