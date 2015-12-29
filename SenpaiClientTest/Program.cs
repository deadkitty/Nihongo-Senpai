using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiClientTest
{
    class Program
    {
        static String marketPlaceBaseUrl = "http://localhost:65337/";

        static void Main(string[] args)
        {
            String operation = "";
            
            Console.WriteLine("Operation:");
            while(operation != "0")
            {
                operation = Console.ReadLine();

                switch(operation)
                {
                    case "1": GetLessons  (); break;
                    case "2": PostLesson  (); break;
                    case "3": PostLessons (); break;
                    case "4": PutLesson   (); break;
                    case "5": DeleteLesson(); break;
                }

                Console.WriteLine();
                Console.WriteLine("Operation:");
            }
        }

        static void GetLessons()
        {
            // WRITE ALL PEOPLE TO CONSOLE (JSON):
            Console.WriteLine("Lektionen:");
            //GetAllLessons();
            List<Lesson> lessons = GetAllLessons();
            foreach (var lesson in lessons)
            {
                Console.WriteLine(lesson);
            }
        }
        
        static List<Lesson> GetAllLessons()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(marketPlaceBaseUrl + "api/Lessons").Result;
            client.Dispose();
            return response.Content.ReadAsAsync<List<Lesson>>().Result;
        }

        static void PostLesson()
        {
            Console.WriteLine("Lektionstext eingeben:");
            String text = Console.ReadLine();

            //Lesson lesson = new Lesson("Tobira L1|1|55");
            //Lesson lesson = new Lesson(text);
            Lesson lesson = Lesson.GetTestLesson();
            Lesson postedLesson = AddLesson(lesson);
            Console.WriteLine("Lektion hinzugefügt: " + postedLesson);   
        }

        static void PostLessons()
        {        
            List<Lesson> postedLessons = AddLessons();

            foreach(Lesson lesson in postedLessons)
            {
                Console.WriteLine("Lektion hinzugefügt: " + lesson);
            }
        }

        static Lesson AddLesson(Lesson newLesson)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(marketPlaceBaseUrl);

            List<Lesson> newLessonList = new List<Lesson>();
            newLessonList.Add(newLesson);
            HttpResponseMessage response = client.PostAsJsonAsync("api/Lessons", newLessonList).Result;
            
            client.Dispose();

            return response.Content.ReadAsAsync<List<Lesson>>().Result.FirstOrDefault();
        }
                
        static List<Lesson> AddLessons()
        {
            List<Lesson> lessons = new List<Lesson>();

            lessons.Add(new Lesson("Genki L3|3|15"));
            lessons.Add(new Lesson("Genki L4|3|14"));
            lessons.Add(new Lesson("Genki L5|3|15"));

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(marketPlaceBaseUrl);

            HttpResponseMessage response = client.PostAsJsonAsync("api/Lessons/" + @"Post", lessons).Result;
            
            client.Dispose();

            return response.Content.ReadAsAsync<List<Lesson>>().Result;
        }

        static void PutLesson()
        {
            Console.WriteLine("Lektionstext eingeben:");
            String text = Console.ReadLine();

            //Lesson lesson = new Lesson("Tobira L1|1|55");
            Lesson lesson = new Lesson(text);
            
            if(UpdateLesson(lesson))
            {
                Console.WriteLine("Update erfolgreich");
            }
            else
            {
                Console.WriteLine("Update fehlgeschlagen");
            }
        }

        static bool UpdateLesson(Lesson lesson)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(marketPlaceBaseUrl);
            HttpResponseMessage response = client.PutAsJsonAsync("api/Lessons", lesson).Result;
            client.Dispose();

            return response.Content.ReadAsAsync<bool>().Result;
        }

        static void DeleteLesson()
        {
            Console.WriteLine("Lektions-id eingeben:");
            String text = Console.ReadLine();

            if(DeleteLesson(Convert.ToInt32(text)))
            {
                Console.WriteLine("Delete erfolgreich");
            }
            else
            {
                Console.WriteLine("Delete fehlgeschlagen");
            } 
        }

        static bool DeleteLesson(int id)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(marketPlaceBaseUrl + "api/Lessons");
            HttpResponseMessage response = client.DeleteAsync(id.ToString()).Result;

            client.Dispose();
            return response.Content.ReadAsAsync<bool>().Result;
        }
    }
}
