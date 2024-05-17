using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaBusinessObject.BusinessObject
{
    public class Material
    {
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public int TeaID { get; set; }
        public Tea Tea { get; set; }

    }
}
