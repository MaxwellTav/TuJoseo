using System.Reflection.Metadata;

namespace TuJoseo.Models
{
    [Serializable]
    public class UserModel
    {
        public int UserID { get; set; } = 0;
        public string UserName { get; set; }
        public string UserCompleteName { get; set; }
        public string UserPassword { get; set; }
        public string UserConfirmPassword { get; set; }
        public string UserEmail { get; set; }

        public bool UserRememberMe { get; set; } = true;
        public int UserJoseosRealized { get; set; } = 0;
        public int UserJobQuality { get; set; } = 0;
        public int UserSimpaty { get; set; } = 0;
        public int UserStalkers { get; set; } = 0;
        public int UserRelevance { get; set; } = 0;
        public string UserKnowledge { get; set; } = "Sin información.";
        public int UserLastLogin { get; set; } = 0;

        public int UserUnreadNotification { get; set; } = 0;
        public int UserUnreadNotificationTime { get; set; } = 0;
        public int UserUnreadMessages { get; set; } = 0;
        public int UserUnreadMessagesTime { get; set; } = 0;
        public int UserUnreadReports { get; set; } = 0;
        public int UserUnreadReportsTime { get; set; } = 0;

        public string UserEducation { get; set; } = "Sin información.";
        public string UserLocation { get; set; } = "Sin información.";
        public string UserHabilities { get; set; } = "Sin información.";
        public string UserNotes { get; set; } = "Sin información.";

        public string UserRol { get; set; } = "Sin información.";
        public bool Terms { get; set; } = true;
        public string UserPhone { get; set; } = "Sin información.";
        public string UserPhoto { get; set; } = "Sin información.";
    }
}
