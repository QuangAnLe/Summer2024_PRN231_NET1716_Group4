using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaBusinessObject.BusinessObject
{
    public class OrderDetail
    {
       public int OrderDetailID { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
        public string CostsIncurred { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public int TeaID { get; set; }
        public Tea Tea { get; set; }
    }
}
