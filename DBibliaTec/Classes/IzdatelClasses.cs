using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace DBibliaTec.DB
{
    partial class Izdatel
    {
        public string Izdatels
        {
            get
            {
                if (Name_izdat == Name_izdat)
                    return $"Издательство: {Name_izdat}";
                else
                    return "";
            }
        }
    }
}
