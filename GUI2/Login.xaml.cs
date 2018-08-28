
using System; 

using System.Collections.Generic; 

using System.Linq; 

using System.Text; 

using System.Windows; 

using System.Windows.Controls; 

using System.Windows.Data; 

using System.Windows.Documents; 

using System.Windows.Input; 

using System.Windows.Media; 

using System.Windows.Media.Imaging; 

using System.Windows.Navigation; 

using System.Windows.Shapes; 

using System.Data; 

using System.Data.SqlClient; 

using System.Text.RegularExpressions; 

  

namespace GUI2

{

    /// <summary> 

    /// Interaction logic for MainWindow.xaml 

    /// </summary> 



    public partial class Login : Window

    {

        public Login()

        {

            InitializeComponent();

        }

        Window1 registration = new Window1();

        //Welcome welcome = new Welcome();



        private void login_Click(object sender, RoutedEventArgs e)

        {

            if (textBoxUsername.Text.Length == 0 )

            {

               errormessage.Text = "Enter a username.";
                

                textBoxUsername.Focus();

            }

            /*else if (!Regex.IsMatch(textBoxUsername.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))

            {

                errormessage.Text = "Invalid username";

                textBoxUsername.Select(0, textBoxUsername.Text.Length);

                textBoxUsername.Focus();

            }*/

            else

            {

                string username = textBoxUsername.Text;

                string password = passwordBox.Password;

                SqlConnection con = new SqlConnection(@"Data Source = GRAD45-HP; Initial Catalog = ASSET_ALLOCATION; User ID = sa; Password = sa123; Trusted_Connection = Yes");

                con.Open();

                SqlCommand cmd = new SqlCommand("Select * from AUTH_TABLE where USERNAME ='" + username + "'  and PASSWORD='" + password + "'", con);

                cmd.CommandType = CommandType.Text;

                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = cmd;

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count > 0)

                {

                    //string username = dataSet.Tables[0].Rows[0]["USERNAME"].ToString() ;

                    //welcome.TextBlockName.Text = username;//Sending value from one form to another form. 

                    //welcome.Show();
                    Console.WriteLine("succesfull login");
                    MessageBox.Show("successfull login");
                    Close();

                }

                else

                {

                   errormessage.Text = "Sorry! Please enter existing username/password.";

                }

                con.Close();

            }

        }



        private void Register_Click(object sender, RoutedEventArgs e)

        {

            registration.Show();

            Close();

        }



    }

}
