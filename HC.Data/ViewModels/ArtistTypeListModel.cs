using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;
using Telerik.Web.Mvc;

namespace HC.Data.ViewModels
{
    public class ArtistTypeListModel
    {
        public ArtistTypeListModel()
        {
            this.ArtistTypes = new GridModel<ArtistType>();
        }

        public GridModel<ArtistType> ArtistTypes { get; set; }
    }
}
