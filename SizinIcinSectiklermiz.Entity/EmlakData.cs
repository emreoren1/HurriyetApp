using System;
using System.Collections.Generic;
using System.Text;

namespace SizinIcinSectiklermiz.Entity
{
    public class EmlakData
    {
        public string Adv_Text { get; set; }
        public string Adv_Def_Link { get; set; }
        public string Adv_Location { get; set; }
        public string Adv_Price { get; set; }
        public string Adv_Title { get; set; }
        public string Adv_Imagename { get; set; }
        public string Adv_Image { get; set; }
        public int Adv_City_Id { get; set; }
        public string Adv_Cityname { get; set; }

        public class EmlakList
        {
            public List<EmlakData> Emlaks { get; set; }
        }
    }
}
