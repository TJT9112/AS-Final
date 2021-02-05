using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;

using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Drawing;

namespace Application_Security_Assignment
{
    public partial class Login : System.Web.UI.Page
    {

        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ASDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public class MyObject
        {
            public string success { get; set; }
            public List<string> ErrorMessage { get; set; }
        }

        public bool ValidateCaptcha()
        {
            bool result = true;

            string captchaResponse = Request.Form["g-recaptcha-response"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(" https://www.google.com/recaptcha/api/siteverify?secret= &response=" + captchaResponse);

            try
            {
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();
                        lbl_gScore.Text = jsonResponse.ToString();
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);
                        result = Convert.ToBoolean(jsonObject.success);
                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }

        }


        protected string getDBHash(string userid)
        {
            string h = null;
            SqlConnection con = new SqlConnection(MYDBConnectionString);
            string sql = "select PasswordHash FROM Register WHERE Email=@USERID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@USERID", userid);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { con.Close(); }
            return h;
        }

        protected int getAttempts(string userid)
        {
            int l = 0;

            SqlConnection con = new SqlConnection(MYDBConnectionString);

            string sql = "select Attempts FROM Register WHERE Email=@USERID";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@USERID", userid);

            try
            {
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Attempts"] != null)
                        {
                            if (reader["Attempts"] != DBNull.Value)
                            {
                                l = Convert.ToInt32(reader["Attempts"]);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            finally { con.Close(); }

            return l;
        }

        protected string getDBSalt(string userid)
        {
            string s = null;
            SqlConnection con = new SqlConnection(MYDBConnectionString);
            string sql = "select PasswordSalt FROM Register WHERE Email=@USERID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@USERID", userid);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PASSWORDSALT"] != null)
                        {
                            if (reader["PASSWORDSALT"] != DBNull.Value)
                            {
                                s = reader["PASSWORDSALT"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { con.Close(); }
            return s;
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(MYDBConnectionString);
            string pwd = HttpUtility.HtmlEncode(tb_passwordlogin.Text.ToString().Trim());
            string user_id = HttpUtility.HtmlEncode(tb_userid.Text.ToString().Trim());
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(user_id);
            string dbSalt = getDBSalt(user_id);


            if (ValidateCaptcha())
            {
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string pwdWithSalt = pwd + dbSalt;

                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

                    string userHash = Convert.ToBase64String(hashWithSalt);

                    int Attempts = getAttempts(user_id);

                    if (userHash.Equals(dbHash) && Attempts < 4)
                    {
                        Session["LoggedIn"] = tb_userid.Text.Trim();
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthenticationToken"] = guid;
                        Response.Cookies.Add(new HttpCookie("AuthenticationToken", guid));

                        string ResetLoginAttempts = "update Register set Attempts = @Attempts where Email = @Email ";
                        SqlCommand Rcmd = new SqlCommand(ResetLoginAttempts, con);
                        Rcmd.Parameters.AddWithValue("@Attempts", 0);
                        Rcmd.Parameters.AddWithValue("@Email", tb_userid.Text);
                        con.Open();
                        Rcmd.ExecuteNonQuery();
                        con.Close();

                        Response.Redirect("HomePage.aspx", false);
                    }

                    else if (!userHash.Equals(dbHash))
                    {
                        string IncreaseLoginAttempts = "update Register set Attempts = @Attempts where Email = @Email ";
                        SqlCommand Icmd = new SqlCommand(IncreaseLoginAttempts, con);
                        Icmd.Parameters.AddWithValue("@Attempts", Attempts + 1);
                        Icmd.Parameters.AddWithValue("@Email", tb_userid.Text);
                        con.Open();
                        Icmd.ExecuteNonQuery();
                        con.Close();

                        if (Attempts == 3)
                        {
                            string blockstatus = "update Register set Status = @Status where Email = @Email ";
                            SqlCommand UScmd = new SqlCommand(blockstatus, con);
                            UScmd.Parameters.AddWithValue("@Status", 0);
                            UScmd.Parameters.AddWithValue("@Email", tb_userid.Text);
                            con.Open();
                            UScmd.ExecuteNonQuery();
                            con.Close();
                            lbl_message.Text = "You have failed used 3 attempts and fail you account has been locked.";
                            lbl_message.ForeColor = Color.Red;
                        }

                        else
                        {
                            lbl_message.Text = "You have entered either the password wrongly or the username wrongly.";
                        }

                    }

                }

                else
                {
                    lbl_message.Text = "You have entered either the password wrongly or the username wrongly.";
                }


            }

        }

        protected void btn_registerpage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx", false);
        }
    }
}