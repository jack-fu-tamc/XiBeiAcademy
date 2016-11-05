using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HC.Data.Models;
using ResposityOfEf;

namespace HC.Service.Attachment
{
    public class AttachmentService : IAttachmentService
    {
        IRepository<HC.Data.Models.Attachment> _irepositoryAtta;

        public AttachmentService(IRepository<HC.Data.Models.Attachment> irepositoryAtta)
        {
            this._irepositoryAtta = irepositoryAtta;
        }
        public void addAttachment(HC.Data.Models.Attachment entity)
        {
            _irepositoryAtta.Insert(entity);
        }

        public void deleteAttachment(HC.Data.Models.Attachment entity)
        {
            _irepositoryAtta.Delete(entity);
        }

        public void updateAttachment(HC.Data.Models.Attachment entity)
        {
            _irepositoryAtta.Update(entity);
        }

        public HC.Data.Models.Attachment getByID(int Id)
        {
            return _irepositoryAtta.GetById(Id);
        }
    }
}
