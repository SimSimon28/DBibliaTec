using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DBibliaTec.DB
{
    partial class Book
    {
        public string NalColor
        {
            get
            {
                if (Count == 0)
                    return "#F9B4B4";
                else
                    return "#C6C3EA";
            }
        }

        public string Nal
        {
            get
            {
                if (Count == 0)
                    return "НЕТ В НАЛИЧИИ";
                else
                    return $"Кол-во: {Count}";
            }
        }

    }
}
