﻿namespace ClientMilkTeamPage.ViewModel
{
    public class CommentVM
    {
        public int CommentID { get; set; }
        public DateTime CommentDate { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public int TeaID { get; set; }
        public int UserID { get; set; }
    }
}
