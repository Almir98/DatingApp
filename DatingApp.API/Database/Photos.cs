using System;
using System.Collections.Generic;

namespace DatingApp.API.Database
{
    public partial class Photos
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdd { get; set; }
        public bool IsMain { get; set; }
        public int UserId { get; set; }
        public string PublicId { get; set; }

        public virtual Users User { get; set; }
    }
}
