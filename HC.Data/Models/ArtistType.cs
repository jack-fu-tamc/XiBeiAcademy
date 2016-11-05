using System;
using System.Collections.Generic;

namespace HC.Data.Models
{
    public partial class ArtistType : BaseEntity
    {
        public ArtistType()
        {
            this.News = new List<News>();
        }

        public int ArtistTypeID { get; set; }
        public string TypeName { get; set; }
        public bool Effective { get; set; }
        public int ArtistCategory { get; set; }        
        public virtual ICollection<News> News { get; set; }
    }
}
