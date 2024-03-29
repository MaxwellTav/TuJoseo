﻿namespace TuJoseo.Models
{
    public class MainHomeModel
    {
        public int REFERENCEID { get; set; }
        public UserModel USER { get; set; }
        public NotesModel? NOTES { get; set; }
        public List<JoseoModel>? JOSEOS { get; set; }
        public int Room { get; set; } = 0;

        public List<NotificationModel>? Notifications { get; set; }
    }
}
