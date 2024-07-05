namespace MilkTeaStore.DTO.Create
{
    public class TaskUserCreateDTO
    {
        public int TaskId { get; set; }
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public bool? Status { get; set; }
        public int UserID { get; set; }
        public int OrderID { get; set; }
    }
}
