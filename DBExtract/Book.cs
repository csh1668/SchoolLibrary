using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExtract
{
    [Serializable]
    public struct Book
    {
        public int num;
        public string reg_num;
        public string name;
        public string vol;
        public string author;
        public string publisher;
        public int year;
        public string symbol;
        public string reg_day;
        public string available;
        public string place;
    }
}
