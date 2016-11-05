using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;

namespace HC.Service.ArtistType
{
    public interface IartiseTypeService
    {
        IList<HC.Data.Models.ArtistType> GetAllArtisetPeopleType();
        IList<Data.Models.ArtistType> GetAllArtisetOperaType();
        void addArtiseType(Data.Models.ArtistType entity);
        void updateArtiseType(HC.Data.Models.ArtistType entity);
        HC.Data.Models.ArtistType getByID(int Id);
        void deleteArtistType(HC.Data.Models.ArtistType entity);

    }
}
