using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace TICKETS.Pages.users
{
    public class SignupModel : PageModel
    {
        String ConnectionString = "Data Source=.;Initial Catalog=bus_Transportation;Integrated Security=True";
        public Signup datas= new Signup();
        public String message = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            datas.email = Request.Form["email"];
            datas.username = Request.Form["username"];
            datas.password = Request.Form["password"];
            datas.gender = Request.Form["gender"];
            datas.dob = DateTime.Parse(Request.Form["dob"]);

            using(SqlConnection con = new SqlConnection(ConnectionString))
            {
                String query = " INSERT INTO users VALUES(@username,@gender,@password,@email,'Client',@dob)";
                con.Open();
                using(SqlCommand cmd = new SqlCommand(query,con))
                {
                    cmd.Parameters.AddWithValue("@username", datas.username);
                    cmd.Parameters.AddWithValue("@gender", datas.gender);
                    cmd.Parameters.AddWithValue("@email", datas.email);
                    cmd.Parameters.AddWithValue("@password", datas.password);
                    cmd.Parameters.AddWithValue("@dob", datas.dob);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if(rowsAffected > 0) 
                    {
                        message = "Data Saved";
                    }
                    else
                    {
                        message = "Data not Saved";
                    }
                }
                con.Close();
            }
        }
    }

    public class Signup
    {
        public String? username { get; set; }
        public string? password { get; set; }
        public String? email {  get; set; }
        public DateTime? dob {  get; set; }
        public String? gender {  get; set; }
    }
}
