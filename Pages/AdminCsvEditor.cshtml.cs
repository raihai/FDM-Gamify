using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace fdm_gamify2
{
    public class AdminCsvEditor : PageModel
    {
        private string test = "test";
        public void OnGet()
        {
            if (System.Text.Encoding.Default.GetString(HttpContext.Session.Get("IsAdmin")) == "true")
            {
                SessionManager sessionManager = new SessionManager();
                int counter = 0;  
                string line;
                string htmlstring = "<form id='CsvForm'>";
                if (HttpContext.Session.GetString("filename") == null)
                {
                    HttpContext.Session.SetString("filename", "wwwroot/SoftwareTesting");
                }
                else
                {
                    HttpContext.Session.SetString("filename", "wwwroot/"+HttpContext.Session.GetString("filename"));
                }
                string filename = HttpContext.Session.GetString("filename");

                // Read the file and display it line by line.  
                System.IO.StreamReader file =
                    new System.IO.StreamReader(filename+".csv");  
                while((line = file.ReadLine()) != null)
                {
                    string[] parts = line.Split(",");
                    string question = parts[0];
                    string AnswerA = parts[1];
                    string AnswerB = parts[2];
                    string AnswerC = parts[3];
                    string Answer = parts[4];
                    htmlstring = htmlstring + $@"            
                        <div style='float:left;margin-right:20px'>
                            <label for='question+{counter}'>Question</label><br/>
                            <input style='width: 300px' id='question+{counter}' type='text' class='line1' height=200px value='{question}' name='question{counter}'>
                        </div>
                        <div style='float:left;margin-right:20px;'>
                            <label for='AnswerA+{counter}'>Answer A</label><br/>
                            <input style='width: 300px' id='AnswerA+{counter}' type='text' height=200px value='{AnswerA}' name='AnswerA{counter}'>
                        </div>

                        <div style='float:left;margin-right:20px;'>
                            <label for='AnswerB+{counter}'>Answer B</label><br/>
                            <input style='width: 300px' id='AnswerB+{counter}' type='text' height=200px value='{AnswerA}' name='AnswerB{counter}'>
                        </div>
                        
                        <div style='float:left;margin-right:20px;'>
                            <label for='AnswerC+{counter}'>Answer C</label><br/>
                            <input style='width: 300px' id='AnswerC+{counter}' type='text' height=200px value='{AnswerB}' name='AnswerC{counter}'>
                        </div>
                        <div style='float:left;margin-right:20px;'>
                            <label for='Answer+'{counter}'>Correct Answer </label><br/>
                            <input style='width: 300px' id='Answer+{counter}' type='text' height=200px value='{AnswerC}' name='Answer{counter}'>
                        </div>
                        <br style='clear:both;'>";
                    counter++;
                }
                file.Close();
                HttpContext.Session.SetString("HtmlString",htmlstring);
                Console.WriteLine("finished get");
            }
            else
            {
                Response.Redirect("./Error");
            }
        }

        public async void Onpost()
        {
            if (HttpContext.Request.Form.ContainsKey("tablename"))// if choosing to change csv file
            {
                HttpContext.Session.SetString("filename",HttpContext.Request.Form["tablename"]);
                OnGet();
            }
            else// if editing the csv
            {
                Console.WriteLine("else");
                int count = 0;
                string newline = "";
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(HttpContext.Session.GetString("filename")+".csv"))
                {
                    foreach (var key in HttpContext.Request.Form.Keys)
                    {
                        
                        newline = newline + "," + HttpContext.Request.Form[key];
                        count = count + 1;
                        if (count % 4 == 0)
                        {
                            newline = newline.Substring(1);
                            file.WriteLine(newline);
                            count = 0;
                            newline = "";
                        }
                    }
                }

                string filename = HttpContext.Session.GetString("filename");
                Console.WriteLine(HttpContext.Request.Form["Answer1"]);
            }

        }
    }
}