using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;

namespace TICKETS.Pages.users
{
    public class LoginModel : PageModel
    {
        String ConnectionString = "Data Source=.;Initial Catalog=bus_Transportation;Integrated Security=True";
        public Login log = new Login();
        public String message = "";

        public void OnGet()
        {
        }
        public IActionResult Onpost()
        {
            log.email = Request.Form["email"];
            log.password = Request.Form["password"];
            log.role = Request.Form["role"];

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                String query = "Select email,password,role from users where email=@email";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@email", log.email);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Login logs = new Login();
                            logs.email = reader.GetString(0);
                            logs.password = reader.GetString(1);
                            logs.role = reader.GetString(2);
                            if (logs.password.Equals(log.password))
                            {
                                if (logs.role.Equals("Admin"))
                                {
                                    return RedirectToPage("/users/Signup");
                                }
                                else
                                {
                                  return  RedirectToPage("/Indexss");
                                }
                            }
                            else
                            {
                                message = "Email or Password is incorrect";
                            }
                        }
                        else 
                        {
                            message = "User not found";
                        }
                    }
                }
            }
            return Page();

        }
        public class Login
        {

            public string? password { get; set; }
            public String? email { get; set; }
            public String? role { get; set; }
        }
    }
}
