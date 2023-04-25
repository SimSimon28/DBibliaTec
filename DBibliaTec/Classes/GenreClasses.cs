using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DBibliaTec.DB
{
        partial class Genre
        {
            public string Genres
            {
                get
                {
                    if (Name == Name)
                        return $"Жанр: {Name}";
                    else
                        return "";
                }
            }
        } 
}
