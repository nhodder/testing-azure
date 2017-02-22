using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestingAzure.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            List<Tuple<string, string>> records = new List<Tuple<string, string>>();
            string connString = ConfigurationManager.ConnectionStrings["TestAzure"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                string query = "select Name, Url from dbo.Blogs";;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new Tuple<string, string>(reader.GetString(0), reader.GetString(1)));
                        }
                    }
                }
            }

            ViewBag.Records = records;

            return View();
        }
	}
}