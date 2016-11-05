using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;
using HC.Data.ViewModels;
using ResposityOfEf;

namespace HC.Service.Attachment
{
    public interface IAttachmentService
    {
        void addAttachment(HC.Data.Models.Attachment entity);
        void deleteAttachment(HC.Data.Models.Attachment entity);
        void updateAttachment(HC.Data.Models.Attachment entity);
        HC.Data.Models.Attachment getByID(int Id);
    }
}
