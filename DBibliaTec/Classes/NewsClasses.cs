using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBibliaTec.DB
{
    partial class News
    {
        public string Themes
        {
            get
            {
                if (Theme == Theme)
                    return $"Тема: {Theme}";
                else return "";
            }
        }
    }
}
