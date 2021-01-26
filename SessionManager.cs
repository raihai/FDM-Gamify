using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Web;
using Org.BouncyCastle.Asn1.Ocsp;

namespace fdm_gamify2
{
    public class SessionManager// Class is built to manage session data in the pages context
    {
        public void NewUser(HttpContext context, SessionManager session, String IsAdmin)
        {
            Byte[] Admin = Encoding.ASCII.GetBytes(IsAdmin);
            context.Session.Set("IsAdmin", Admin);
        }

        public static String GetUserType(HttpContext context, SessionManager session)
        {
            return context.Session.Get("IsAdmin").ToString();
        }
        public void QuizSetUp(HttpContext context, FileReader fileReader, string fileName) // Sets up quiz data into the session
        {
            
            SessionManager sessionManager = new SessionManager();// creates new session manager
            fileReader.fileReader(fileName, sessionManager.toInt(context.Session.Get("Count")));
            sessionManager.QuizSession(context, fileReader, sessionManager); //creates a session
        }


        public void QuizSession(HttpContext context, FileReader fileReader, SessionManager session)
        {
            if (context.Session.Get("Count") == null) // if the count of the quiz question dosnt exist
            {

                context.Session.Set("Count", (BitConverter.GetBytes(0)));// sets the count to 0 in the session
                Console.WriteLine("Count established");
            }
            else //if the count of which question they are on already exists
            {
                // increment the count by one
                int currentCount = session.toInt(context.Session.Get("Count"));
                currentCount = currentCount + 1;
                context.Session.Set("Count", (BitConverter.GetBytes(currentCount)));// sets count to the new value in the session
                Console.WriteLine("Count incremented");
                Console.WriteLine(currentCount);

            }

            foreach (FieldInfo field in fileReader.GetType().GetFields())
            {
                // reads the question and answers from the line using the count
                byte[] FieldValueByte = FieldtoByte(field);
                if (FieldValueByte != null)
                {
                    context.Session.Set(field.Name, FieldValueByte);
                    Console.WriteLine(field.Name);
                }

            }
        }

        /*
         * filename: is a string based on the page that is loaded allows for modular quiz'
         * context: This is simply the context of the page allows for the session/page data to be accessed
         */
        public static string quizFileSetter(string filename, HttpContext context)
        {
            context.Session.Set("Count", (BitConverter.GetBytes(0)));// sets the file for the data to be read from. 
            context.Session.Set("FileName", @Encoding.ASCII.GetBytes((filename)));
            Console.WriteLine(context.Session.Get("FileName"));
            return "";
        }

        /*
         * All functions below here are for switching between data types in and out of the page 
         */
        public static byte[] FieldtoByte(FieldInfo field)
        {
            string a = "a";
            int b = 1;
            if (field.FieldType == a.GetType())// if the field is a string
            {
                byte[] bytes = Encoding.ASCII.GetBytes((string) field.GetValue(field));
                return bytes;
            }

            if (field.FieldType == b.GetType())// if the field is an integer
            {
                byte[] bytes = BitConverter.GetBytes((int) field.GetValue(field));
                return bytes;
            }

            return null;
        }

        public string toString(byte[] bytes)// converts a byte array to a string
        {
            if (bytes != null)
            {
                string str = Encoding.ASCII.GetString(bytes);
                return str;
            }

            return null;
        }

        public int toInt(byte[] bytes)// converts a byte array to a string
        {
            if (bytes != null)
            {
                int integer = BitConverter.ToInt32(bytes);
                return integer;
            }

            return -1;
        }

        public byte[] stringToByte(string String)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(String);
            return bytes;
        }

        public bool newUser(string nickname, int points)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.OpenConnection();
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine(nickname);
            string query = "Insert into SoftwareTestingQuiz VALUES(null,''" + nickname + "'," + points + ")";
            Console.WriteLine(query + "------------------------");
            db.ExecuteQuery("Insert into SoftwareTestingQuiz VALUES(null,"+"'"+ nickname +"',"+ points+")");
            db.CloseConnection();
            return true;
        }
        public void setString(string name, string value, HttpContext context)
        {
            byte[] valuebyte = stringToByte(value);
            context.Session.Set(name, valuebyte);
        }
    }

}

