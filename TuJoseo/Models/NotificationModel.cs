namespace TuJoseo.Models
{
    public class NotificationModel
    {
        public int NotificationID { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationBody { get; set; }
        public bool NotificationSeen { get; set; }
        
        //ID de la referencia, en caso de querer mandar una clave foranea.
        public int ReferenceID { get; set; }
        public NotificationType NotificationType { get; set; }
    }

    public enum NotificationType
    {
        Review
    }
}
