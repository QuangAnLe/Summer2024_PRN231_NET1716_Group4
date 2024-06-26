using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaStore.ViewModels
{
    public class OrderDetailVM
    {
        public int OrderDetailID { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
        public string CostsIncurred { get; set; }
        public int OrderID { get; set; }
        public TeaVM TeaVM { get; set; }
    }
}
