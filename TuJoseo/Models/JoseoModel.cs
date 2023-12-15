namespace TuJoseo.Models
{
    public class JoseoModel
    {
        public int JoseoID { get; set; }
        public string JoseoTitle { get; set; } = string.Empty;
        public string JoseoDescription { get; set; } = string.Empty;
        public string JoseoPrice { get; set; } = string.Empty;
        public string JoseadorID { get; set; } = string.Empty;
        public DateTime JoseoStartTime { get; set; } = DateTime.Now;
        public DateTime JoseoEstimatedTime { get; set; } = DateTime.Now;
        public DateTime JoseoFinishTime { get; set; } = DateTime.Now;
        public string? JoseoContratoID { get; set; } = string.Empty;
        public string JoseoStatus { get; set; } = string.Empty;
    }
}
