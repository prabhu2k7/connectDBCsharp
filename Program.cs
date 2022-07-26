using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConsoleApp1
{
    class Program
    {

        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public static bool connectionStatus;

        static void Main(string[] args)
        {
            Program Program1 = new Program();
            Program1.Initialize();
            connectionStatus = Program1.OpenConnection();
            Console.WriteLine("connectionStatus : " + connectionStatus);


            Program1.Select();

            Program1.CloseConnection();

            Console.ReadLine();
        }


        private void Initialize()
        {

            //spring.datasource.url = jdbc:mysql://localhost:3306/training_app?useSSL=false
           // spring.datasource.username = root
         // spring.datasource.password = root



            server = "localhost";
            database = "training_app";
            uid = "root";
            password = "root";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            Console.WriteLine("connectionString : "+ connectionString);
            connection = new MySqlConnection(connectionString);
            
        }


        //open connection to database
         private bool OpenConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("Sucess!!");
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }


        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Select statement
        public List<string>[] Select()
        {
            string query = "SELECT * FROM training_app.users;";

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            //Open connection
            //if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["first_name"] + "");
                    list[2].Add(dataReader["last_name"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
                Console.WriteLine("listn= " + list);
                //return list to be displayed
                return list;
            }
            
        }



    }
}
