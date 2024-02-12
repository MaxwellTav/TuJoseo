using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;

namespace TuJoseo.Models
{
    public class UserModelReview : UserModel
    {
        public int UserIDOther { get; set; }
        public string UserNameOther { get; set; }
        public string ProyectName { get; set; }
    }
}
