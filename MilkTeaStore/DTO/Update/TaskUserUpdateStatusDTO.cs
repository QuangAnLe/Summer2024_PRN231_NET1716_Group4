﻿namespace MilkTeaStore.DTO.Update
{
    public class TaskUserUpdateStatusDTO
    {
        public int TaskId { get; set; }
        public bool? Status { get; set; }
        public string FailureReason { get; set; }
    }
}
