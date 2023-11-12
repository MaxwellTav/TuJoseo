namespace TuJoseo.Models
{
    [Serializable]
    public class UserModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserCompleteName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }

        public bool UserRememberMe { get; set; }
        public int UserJoseosRealized { get; set; }
        public int UserJobQuality { get; set; }
        public int UserSimpaty { get; set; }
        public int UserStalkers { get; set; }
        public int UserRelevance { get; set; }
        public string UserKnowledge { get; set; }
        public int UserLastLogin { get; set; }

        public int UserUnreadNotification { get; set; }
        public int UserUnreadNotificationTime { get; set; }
        public int UserUnreadMessages { get; set; }
        public int UserUnreadMessagesTime { get; set; }
        public int UserUnreadReports { get; set; }
        public int UserUnreadReportsTime { get; set; }

        public string UserEducation { get; set; }
        public string UserLocation { get; set; }
        public string UserHabilities { get; set; }
        public string UserNotes { get; set; }
    }
}
