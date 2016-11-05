using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;
using ResposityOfEf;

namespace HC.Service.ArtistType
{
    public class ArtistTypeService:IartiseTypeService
    {
        IRepository<HC.Data.Models.ArtistType> _irepositoryArtistType;


        public ArtistTypeService(IRepository<HC.Data.Models.ArtistType> irepositoryArtistType)
        {
            this._irepositoryArtistType = irepositoryArtistType;
        }

        public IList<Data.Models.ArtistType> GetAllArtisetPeopleType()
        {
            var query = _irepositoryArtistType.Table;
            return query.Where(x=>x.ArtistCategory==1&&x.Effective==true).ToList();
        }

        public IList<Data.Models.ArtistType> GetAllArtisetOperaType()
        {
            var query = _irepositoryArtistType.Table;
            return query.Where(x => x.ArtistCategory == 2 && x.Effective == true).ToList();
        }

        public void addArtiseType(Data.Models.ArtistType entity)
        {
            _irepositoryArtistType.Insert(entity);
        }

        public void updateArtiseType(Data.Models.ArtistType entity)
        {
            _irepositoryArtistType.Update(entity);
        }

        public Data.Models.ArtistType getByID(int Id)
        {
            return _irepositoryArtistType.GetById(Id);
        }

        public void deleteArtistType(Data.Models.ArtistType entity)
        {
            _irepositoryArtistType.Delete(entity);
        }
    }
}
