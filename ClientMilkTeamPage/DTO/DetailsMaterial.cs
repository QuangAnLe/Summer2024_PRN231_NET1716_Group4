using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaBusinessObject.BusinessObject
{
    public class DetailsMaterial
    {
        public int DetailsMaterialID { get; set; }
        public int Quanity { get; set; }
        public string DetailMaterialName { get; set; }
        public int TeaID {  get; set; }
        public int MaterialID { get; set; }

        public Tea Tea { get; set; }
        public Material Material { get; set; }


    }
}
