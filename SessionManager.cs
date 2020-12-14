using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace fdm_gamify2
{
    public class SessionManager
    {
        public void QuizSetUp(HttpContext context, FileReader fileReader, string fileName)
        {

            SessionManager sessionManager = new SessionManager();
            fileReader.fileReader(fileName, sessionManager.toInt(context.Session.Get("Count")));
            sessionManager.Session(context, fileReader, sessionManager);
        }


        public void Session(HttpContext context, FileReader fileReader, SessionManager session)
        {
            
            if (context.Session.Get("Count") == null)
            {

		        context.Session.Set("Count", (BitConverter.GetBytes(0)));
                Console.WriteLine("Count established");
            }
            else
            {
                int currentCount = session.toInt(context.Session.Get("Count"));
                currentCount = currentCount + 1;
                context.Session.Set("Count", (BitConverter.GetBytes(currentCount)));
                Console.WriteLine("Count incremented");
                Console.WriteLine(currentCount);
                
            }

            foreach (FieldInfo field in fileReader.GetType().GetFields())
            {
                byte[] FieldValueByte = toByte(field);
                if (FieldValueByte != null)
                {
                    context.Session.Set(field.Name, FieldValueByte);
                }

            }
        }

        public static byte[] toByte(FieldInfo field)
        {
            string a = "a";
            int b = 1;
            if (field.FieldType == a.GetType())
            {
                byte[] bytes = Encoding.ASCII.GetBytes((string) field.GetValue(field));
                return bytes;
            }

            if (field.FieldType == b.GetType())
            {
                byte[] bytes = BitConverter.GetBytes((int) field.GetValue(field));
                return bytes;
            }

            return null;
        }

        public string toString(byte[] bytes)
        {
            if (bytes != null)
            {
                string str = Encoding.ASCII.GetString(bytes);
                return str;
            }

            return null;
        }
        public int toInt(byte[] bytes)
        {
            if (bytes != null)
            {
                int integer = BitConverter.ToInt32(bytes);
                return integer;
            }

            return -1;
        }

        public static string fileSetter(string filename, HttpContext context)
        {
            context.Session.Set("Count", (BitConverter.GetBytes(0)));
            context.Session.Set("FileName", @Encoding.ASCII.GetBytes((filename)));
            return "";
        }
    }
}

