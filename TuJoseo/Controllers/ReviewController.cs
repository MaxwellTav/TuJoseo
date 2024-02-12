using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TuJoseo.Models;

namespace TuJoseo.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;

        public ReviewController(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DB");
        }

        public IActionResult SetReview(int notificationID)
        {
            int userID = Convert.ToInt32(TempData["UserID"]);
            UserModelReview UserReview = new UserModelReview();

            //ID
            string queryGetReview = $"Select * From PendingReviewTable Where PReviewID = {notificationID};";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(queryGetReview, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToInt32(userID) == Convert.ToInt32(reader.GetInt32(1)))
                                {
                                    UserReview.UserID = Convert.ToInt32(reader.GetInt32(1));
                                    UserReview.UserIDOther = Convert.ToInt32(reader.GetInt32(3));
                                }
                                else
                                {
                                    UserReview.UserID = Convert.ToInt32(reader.GetInt32(3));
                                    UserReview.UserIDOther = Convert.ToInt32(reader.GetInt32(1));
                                }

                                UserReview.ProyectName = reader.GetString(5);
                            }
                        }
                    }
                }
            }

            //Nombre
            string queryGetNameUsersMe = @$"SELECT u1.UserName AS Yo, u2.UserName AS Other 
                                        FROM UserTable u1
                                        INNER JOIN UserTable ur ON u1.UserID = {UserReview.UserID}
                                        INNER JOIN UserTable u2 ON u2.UserID = {UserReview.UserIDOther}
                                        WHERE ur.UserID = {UserReview.UserID};";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(queryGetNameUsersMe, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserReview.UserName = reader.GetString(0);
                                UserReview.UserNameOther = reader.GetString(1);
                            }
                        }
                    }
                }
            }

            UserReview.NotificationID = notificationID;
            TempData["UserID"] = userID;
            return View(UserReview);
        }

        public IActionResult DeleteNotification(int notificationID)
        {
            string userID = TempData["UserID"].ToString();
            string queryDeleteNotification = $"Delete From PendingReviewTable Where PReviewID = {notificationID};";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(queryDeleteNotification, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            TempData["UserID"] = userID;
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult SendReview([FromBody] ReviewModel review)
        {
            string userID = TempData["UserID"].ToString();
            #region Formato y condiciones.
            if (review.ReviewStars == 0 ||
                string.IsNullOrEmpty(review.ReviewCriticadorID) ||
                review.ReviewPersonID < 1)
            {
                return BadRequest(new { success = false });
            }

            if (string.IsNullOrEmpty(review.ReviewDescription))
                review.ReviewDescription = "Sin descripción...";
            #endregion

            //Eliminar la notificación.
            TempData["UserID"] = userID;
            return Ok(new { success = true });
        }

    }
}
