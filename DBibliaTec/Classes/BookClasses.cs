using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBibliaTec.DB
{
    partial class Book
    {
        public string NameS
        {
            get
            {
                if (Name == Name)
                    return $"Название книги: {Name}";
                else
                    return "";
            }
        }

        public string FIO
        {
            get
            {
                if (FAuthor == FAuthor)
                    return $"ФИО автора: {FAuthor}";
                else
                    return "";
            }
        }

        public string Counts
        {
            get
            {
                if (Count == Count)
                    return $"Кол-во: {Count}";
                else
                    return "";
            }
        }

        public string DateVipuska
        {
            get
            {
                if (Date_Vipusk == Date_Vipusk)
                    return $"Дата выпуска: {Date_Vipusk}";
                else
                    return "";
            }
        }
    }
}
