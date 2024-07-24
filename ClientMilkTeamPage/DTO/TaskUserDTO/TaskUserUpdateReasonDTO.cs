namespace ClientMilkTeamPage.DTO.TaskUserDTO
{
    public class TaskUserUpdateReasonDTO
    {
        public int TaskId { get; set; }
        public bool? Status { get; set; }
        public string FailureReason { get; set; }
    }
}
