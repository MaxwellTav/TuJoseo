using System.Text.Json.Serialization;

namespace TuJoseo.Models
{
    [Serializable]
    public class MainModel
    {
        public UserModel? User { get; set; }
    }
}
