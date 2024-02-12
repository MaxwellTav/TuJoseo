namespace TuJoseo.Models
{
    public class ReviewModel
    {
        public int ReviewStars { get; set; }
        public string ReviewProyectName { get; set; }
        public string ReviewDescription { get; set; }
        public int ReviewPersonID { get; set; }
        public string ReviewCriticadorID { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;
        public int NotificationID { get; set; }
    }
}

