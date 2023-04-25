using DBibliaTec.Pages.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DBibliaTec.DB
{
    partial class Client
    {
        public BitmapImage AgePhotoBabka
        {
            get
            {
                if (Age >= 60 && Gender == 2)
                    return new BitmapImage(new Uri("\\LogoAndImages\\Images\\ImageAge\\Babka.png", UriKind.Relative));
                else
                    if (Age >= 65 && Gender == 1)
                    return new BitmapImage(new Uri("\\LogoAndImages\\Images\\ImageAge\\DED.jpg", UriKind.Relative));
                else
                    if (Age >= 25 && Age <= 64 && Gender == 1)
                    return new BitmapImage(new Uri("\\LogoAndImages\\Images\\ImageAge\\Muj.png", UriKind.Relative));
                else
                    if (Age >= 25 && Age <= 64 && Gender == 2)
                    return new BitmapImage(new Uri("\\LogoAndImages\\Images\\ImageAge\\Jen.jpg", UriKind.Relative));
                else
                    if (Age <= 24 && Gender == 2)
                    return new BitmapImage(new Uri("\\LogoAndImages\\Images\\ImageAge\\deva.jpg", UriKind.Relative));
                else
                    if (Age <= 24 && Gender == 1)
                    return new BitmapImage(new Uri("\\LogoAndImages\\Images\\ImageAge\\Mal.png", UriKind.Relative));
                else
                    return null;
            }
        }
    }
}
