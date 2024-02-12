namespace TuJoseo.Models
{
    public class PendingReviewModel
    {
        public int PReviewID { get; set; }
        public string PReviewJoseadorID { get; set; }
        public bool PReviewJoseadorSeen { get; set; }
        public string PReviewJoseadorRealID { get; set; }
        public bool PReviewJoseadorRealSeen { get; set; }
        public string PReviewProyectName { get; set; } = string.Empty;
        public int PReviewProyectID { get; set; }
    }
}
