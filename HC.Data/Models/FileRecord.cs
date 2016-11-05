using System;
using System.Collections.Generic;

namespace HC.Data.Models
{
    public class FileRecord : BaseEntity
    {
        public System.Guid ID { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public int FileType { get; set; }
        public string Description { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
