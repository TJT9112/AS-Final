using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace Application_Security_Assignment
{
    public partial class Register : System.Web.UI.Page
    {

        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ASDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        void registerfailed()
        {
            lbl_registermsg.Text = "Register Failed";
            lbl_registermsg.ForeColor = Color.Red;
        }

        void registersuccess()
        {
            lbl_registermsg.Text = "Register success";
            lbl_registermsg.ForeColor = Color.Green;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private int checkPassword(string password)
        {
            int score = 0;

            if (password.Length < 8)
            {
                return 1;
            }
            else
            {
                score = 1;
            }

            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }

            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }

            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }

            if (Regex.IsMatch(password, "[^A-Za-z0-9]"))
            {
                score++;
            }

            return score;
        }

        private bool passwordcheck()
        {
            int scores = checkPassword(tb_password.Text);
            string status = "";
            lbl_pwdchecker.Text = "Status : " + status;
            switch (scores)
            {
                case 1:
                    status = "Very Weak";
                    break;
                case 2:
                    status = "Weak";
                    break;
                case 3:
                    status = "Medium";
                    break;
                case 4:
                    status = "Strong";
                    break;
                case 5:
                    status = "Very Strong";
                    break;
                default:
                    break;
            }

            if (scores < 4)
            {
                lbl_pwdchecker.Text = "Status : Make a stronger password";
                lbl_pwdchecker.ForeColor = Color.Red;
                return false;
            }

            if (scores == 5)
            {
                lbl_pwdchecker.ForeColor = Color.Green;
                return true;
            }

            return false;
        }

        private bool checkdob()
        {
            string dobpattern = (@"^\s*(3[01]|[12][0-9]|0?[1-9])\/(1[012]|0?[1-9])\/((?:19|20)\d{2})\s*$");

            if(Regex.IsMatch(tb_dob.Text, dobpattern))
            {
                lbl_dobchecker.Text = "Status : Good";
                lbl_dobchecker.ForeColor = Color.Green;
                return true;
            }

            else
            {
                lbl_dobchecker.Text = "Date Of Birth can only be in the format of DD/MM/YYYY!!!!!";
                lbl_dobchecker.ForeColor = Color.Red;
                tb_dob.Text = "  ";
                return false;
            }
        }

        private bool checkemail()
        {
            string emailpattern = (@"^[\w-\.]+@([\w-]+\.)+[\w-]{3}$");

            if(Regex.IsMatch(tb_emailaddress.Text, emailpattern))
            {
                lbl_emailchecker.Text = "Status : Good";
                lbl_emailchecker.ForeColor = Color.Green;
                return true;
            }
            else
            {
                lbl_emailchecker.Text = "Please enter a valid email address!!!!!";
                lbl_emailchecker.ForeColor = Color.Red;
                tb_emailaddress.Text = "  ";
                return false;
            }

        }

        private bool checkcardno()
        {
            string cardnopattern = (@"\d{16}");

            if (Regex.IsMatch(tb_cardnumber.Text, cardnopattern))
            {
                lbl_cardnumberalert.Text = "Status : Good";
                lbl_cardnumberalert.ForeColor = Color.Green;
                return true;
            }
            else
            {
                lbl_cardnumberalert.Text = "Card Number must contain 16 Numbers!!!!!";
                lbl_cardnumberalert.ForeColor = Color.Red;
                return false;
            }
        }

        private bool checkcardcvc()
        {
            string cardcvcpattern = (@"\d{3}");

            if (Regex.IsMatch(tb_cardcvc.Text, cardcvcpattern))
            {
                lbl_cardcvcalert.Text = "Status : Good";
                lbl_cardcvcalert.ForeColor = Color.Green;
                return true;
            }
            else
            {
                lbl_cardcvcalert.Text = "Card CVC must contain 3 Numbers!!!!!";
                lbl_cardcvcalert.ForeColor = Color.Red;
                return false;
            }
        }

        private bool checkcardexpire()
        {
            string cardexpirepattern = (@"^(0[1-9]|1[0-2])\/([0-9]{2})$");

            if (Regex.IsMatch(tb_cardexpire.Text, cardexpirepattern))
            {
                lbl_cardexpirealert.Text = "Status : Good";
                lbl_cardexpirealert.ForeColor = Color.Green;
                return true;
            }
            else
            {
                lbl_cardexpirealert.Text = "Card expire should only be in the format of MM/YY!!!!!";
                lbl_cardexpirealert.ForeColor = Color.Red;
                return false;
            }
        }

        private bool checkfirstname()
        {
            string firstnamepattern = (@"[^A-Za-z0-9]");

            if (Regex.IsMatch(tb_firstname.Text, firstnamepattern))
            {
                lbl_firstnamechecker.Text = "First Name cannot contain special symbol";
                lbl_firstnamechecker.ForeColor = Color.Red;
                return false;
            }
            else
            {
                lbl_firstnamechecker.Text = "Status : Good";
                lbl_firstnamechecker.ForeColor = Color.Green;
                return true;
            }
        }

        private bool checklastname()
        {
            string lastnamepattern = (@"[^A-Za-z0-9]");

            if (Regex.IsMatch(tb_firstname.Text, lastnamepattern))
            {
                lbl_lastnamechecker.Text = "last Name cannot contain special symbol";
                lbl_lastnamechecker.ForeColor = Color.Red;
                return false;
            }
            else
            {
                lbl_lastnamechecker.Text = "Status : Good";
                lbl_lastnamechecker.ForeColor = Color.Green;
                return true;
            }
        }

        public void createAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Register VALUES(@FirstName,@LastName,@Email,@DOB,@PasswordHash,@PasswordSalt,@CreditCardNumber, @CreditCardCVC,@CreditCardExpire,@IV,@Key,@Status,@Attempt)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@FirstName", tb_firstname.Text.Trim());
                            cmd.Parameters.AddWithValue("@LastName", tb_lastname.Text.Trim());
                            cmd.Parameters.AddWithValue("@Email", tb_emailaddress.Text.Trim());
                            cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(tb_dob.Text.Trim()));
                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@CreditCardNumber", Convert.ToBase64String(encryptData(tb_cardnumber.Text.Trim())));
                            cmd.Parameters.AddWithValue("@CreditCardCVC", Convert.ToBase64String(encryptData(tb_cardcvc.Text.Trim())));
                            cmd.Parameters.AddWithValue("@CreditCardExpire", Convert.ToBase64String(encryptData(tb_cardexpire.Text.Trim())));
                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                            cmd.Parameters.AddWithValue("@Status", 1);
                            cmd.Parameters.AddWithValue("@Attempt", 0);
                            cmd.Connection = con;

                            try
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                lb_error1.Text = ex.ToString();
                            }
                            finally
                            {
                                con.Close();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }

        protected void btn_register_Click(object sender, EventArgs e)
        {
            if(checkemail() == true)
            {

                SqlConnection sqlconn = new SqlConnection(MYDBConnectionString);
                string sqlquery = "select * from Register where Email=@Email";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlconn.Open();
                sqlcomm.Parameters.AddWithValue("@Email", tb_emailaddress.Text);
                SqlDataReader sdr = sqlcomm.ExecuteReader();
                if (sdr.HasRows)
                {
                    MessageBox.Show("Email alreadly exist");
                    Response.AddHeader("REFRESH", "5;Login.aspx");
                    registerfailed();
                }
                else
                {

                    string pwd = tb_password.Text.ToString().Trim(); ;

                    //Generate random "salt"
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    byte[] saltByte = new byte[8];

                    //Fills array of bytes with a cryptographically strong sequence of random values.
                    rng.GetBytes(saltByte);
                    salt = Convert.ToBase64String(saltByte);

                    SHA512Managed hashing = new SHA512Managed();

                    string pwdWithSalt = pwd + salt;
                    byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

                    finalHash = Convert.ToBase64String(hashWithSalt);
                    RijndaelManaged cipher = new RijndaelManaged();

                    cipher.GenerateKey();
                    Key = cipher.Key;
                    IV = cipher.IV;


                    if (checkfirstname() == true && checklastname() == true && checkemail() == true && checkdob() == true && passwordcheck() == true && checkcardno() == true && checkcardcvc() == true && checkcardexpire() == true)
                    {
                        lbl_pwdchecker.Text = "Status : Good";
                        lbl_pwdchecker.ForeColor = Color.Green;
                        registersuccess();
                        createAccount();
                    }
                    else
                    {
                        registerfailed();
                    }

                }
            }

        }

        protected void btn_loginpage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx", false);
        }
    }
}