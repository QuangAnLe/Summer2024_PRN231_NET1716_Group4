using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaBusinessObject.BusinessObject
{
    public class Tea
    {
        public int TeaID { get; set; }
        public string TeaName { get; set; }
        public int Estimation { get; set; }
        public decimal Price { get; set; }
        public string TeaDescription { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }
        public List<Comment> Comments { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public List<Material> Materials { get; set; }
    }
}
