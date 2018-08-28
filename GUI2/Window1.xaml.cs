using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



using System.Data;

using System.Data.SqlClient;

using System.Text.RegularExpressions;
using System.Globalization;

namespace GUI2

{

    /// <summary> 

    /// Interaction logic for Registration.xaml 

    /// </summary> 

    public partial class Window1 : Window

    {


        public Window1()

        {

            InitializeComponent();

        }




        /* public void Login_Click(object sender, RoutedEventArgs e)

         {

             Login login = new Login();

             login.Show();

             Close();

         }*/



        private void reset_Click(object sender, RoutedEventArgs e)

        {

            Reset();

        }



        public void Reset()

        {

            textBoxFirstName.Text = "";

            textBoxLastName.Text = "";

            textBoxEmail.Text = "";

            textBoxPhone.Text = "";

            textBoxUsername.Text = "";

            textBoxOccupation.Text = "";

            passwordBox.Password = "";





        }

        /*private void close_Click(object sender, RoutedEventArgs e)

        {

            Close();

        }*/



        public void Submit_Click(object sender, RoutedEventArgs e)

        {

            if (textBoxUsername.Text.Length== 0  || !Regex.IsMatch(textBoxUsername.Text, @"(?=.*\d)(?=.*[a - z])(?=.*[A - Z]).{ 6,}/"))

            {

                textBoxUsername.BorderBrush = Brushes.Red;

                errormessage1.Text = "Username must have at least one number, one lowercase, one uppercase letter and at least six characters.";

                textBoxUsername.Focus();

            }

            else if ( textBoxEmail.Text.Length == 0 || !Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))

            {

                errormessage.Text = "Enter a valid email.";

                //textBoxEmail.Select(0, textBoxEmail.Text.Length);

                textBoxEmail.Focus();

            }

            else

            {

               

                if (passwordBox.Password.Length == 0 || !Regex.IsMatch(textBoxEmail.Text, @"(?=.*\d)(?=.*[a - z])(?=.*[A - Z]).{ 6,}/"))

                {

                    passwordBox.BorderBrush = Brushes.Red;
                    errormessage2.Text = "Password must have at least one number, one lowercase, one uppercase letter and at least six characters.";

                    passwordBox.Focus();

                }

                


              



                else

                {

                    string firstname = textBoxFirstName.Text;

                    string lastname = textBoxLastName.Text;

                    string email = textBoxEmail.Text;



                    string phone = textBoxPhone.Text;

                    string username = textBoxUsername.Text;

                    string occupation = textBoxOccupation.Text;

                    string password = passwordBox.Password;

                    string securityAnswer = textBoxSecurity.Text;


                    errormessage.Text = "";

                    //string address = textBoxAddress.Text;

                    SqlConnection con = new SqlConnection(@"Data Source=GRAD45-HP ; User ID=sa; Password=sa123 ; INITIAL CATALOG=ASSET_ALLOCATION; Trusted_Connection=Yes");

                    con.Open();
                    /* Console.WriteLine(firstname);
                     Console.WriteLine(lastname);
                     Console.WriteLine(email);
                     Console.WriteLine(phone);
                     Console.WriteLine(dob);
                     Console.WriteLine(gender);
                     Console.WriteLine(username);
                     Console.WriteLine(occupation);

                     Console.WriteLine(this.gender);
                    Console.WriteLine(this.gender.Text);
                    Console.WriteLine(this.dob.Text);*/


                    SqlCommand cmdd = new SqlCommand("Select * from AUTH_TABLE where USERNAME ='" + username + "'  and PASSWORD='" + password + "'", con);

                    cmdd.CommandType = CommandType.Text;

                    SqlDataAdapter adapter = new SqlDataAdapter();

                    adapter.SelectCommand = cmdd;

                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet);
                    if (dataSet.Tables[0].Rows.Count > 0)

                    {

                        //string username = dataSet.Tables[0].Rows[0]["USERNAME"].ToString() ;

                        //welcome.TextBlockName.Text = username;//Sending value from one form to another form. 

                        //welcome.Show();
                        //Console.WriteLine("succesfull login");
                        MessageBox.Show("Already existing user!");
                        Close();

                    }




                    else
                    {
                        SqlCommand cmd = new SqlCommand("Insert into PERSONAL_INFO (FIRSTNAME,LASTNAME,GENDER,DOB,EMAIL,PHONE_NO,USERNAME,OCCUPATION) values('" + firstname + "','" + lastname + "','" + this.gender.Text + "','" + this.dob.Text + "','" + email + "','" + phone + "','" + username + "','" + occupation + "')", con);

                        cmd.CommandType = CommandType.Text;

                        cmd.ExecuteNonQuery();

                        //Console.WriteLine(this.securityQues);
                        //Console.WriteLine(this.securityQues.Text);


                        SqlCommand cmd1 = new SqlCommand("Insert into AUTH_TABLE (USERNAME,PASSWORD,SECURITY_QNO,ANS) values('" + username + "','" + password + "','" + this.secques + "','" + securityAnswer + "')", con);

                        cmd1.CommandType = CommandType.Text;

                        cmd1.ExecuteNonQuery();

                        con.Close();

                        //errormessage.Text = "You have Registered successfully.";
                        MessageBox.Show("Yoi have registered successfully");

                        Reset();
                        Login login = new Login();

                        login.Show();

                        Close();
                        // Reset();
                    }


                }

            }

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var comboBox = sender as ComboBox;
            int securityQues = comboBox.SelectedIndex + 1;

            secques = securityQues;


            //Console.WriteLine(securityQues);
            //string selectedcmb = comboBox.Items[comboBox.SelectedIndex].ToString();
            //string securityQues = comboBox.SelectedValue.ToString();

        }
        public int secques;
        public void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

            var calendar = sender as System.Windows.Controls.Calendar;

            if (calendar.SelectedDate.HasValue)
            {

                DateTime date = calendar.SelectedDate.Value.Date;
                string dob = date.ToShortDateString();
                this.dob.Text = dob;
                //Console.WriteLine("new dob "+this.dob.Text);
                //dob = "";


                //Console.WriteLine(dob.Length);
            }
        }
        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            string gender = radioButton.Content.ToString();
            this.gender.Text = gender;
            // Console.WriteLine("new gender"+this.gender.Text);

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }

    }

}

