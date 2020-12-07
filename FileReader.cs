using System;

namespace fdm_gamify2
{
    public class FileReader
    {
        public static int count = 0;
        public static string question = "Question";
        public static string A;
        public static string B;
        public static string C;
        
        public static string fileReader(string fileName,int passedCount)
        {
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            count = passedCount + 1;
            Console.Write(count);
            string[] lines = System.IO.File.ReadAllLines(fileName);
            lines = lines[count].Split(",");
            question = lines[0];
            A = lines[1];
            B = lines[2];
            C = lines[3];
            return "Index";

        }
    }
}