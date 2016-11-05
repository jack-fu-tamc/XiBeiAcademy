using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maticsoft.Model
{
    public class User_Type_Relation_Search : Base_Search
    {
        private int _ExpID;
        public int ExpID
        {
            set { _ExpID = value; }
            get { return _ExpID; }
        }
    }
}
