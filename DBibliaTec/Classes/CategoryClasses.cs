using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DBibliaTec.DB
{
    partial class Category
    {
        public string Categorys
        {
            get
            {
                if (Name == Name)
                    return $"Котегория: {Name}";
                else
                    return "";
            }
        }
    }
}
