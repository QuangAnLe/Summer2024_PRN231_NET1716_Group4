namespace MilkTeaStore.ViewModels
{
    public class TaskUserVM
    {
        public int TaskId { get; set; }
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public bool? Status { get; set; }
        public int UserID { get; set; }
        public int OrderID { get; set; }


        public string UserName { get; set; }
        public string ReasonContent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ShipAddress { get; set; }
    }
}
