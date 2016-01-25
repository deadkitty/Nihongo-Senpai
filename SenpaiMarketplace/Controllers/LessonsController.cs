using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SenpaiMarketplace.Models;
using System.IO;

namespace SenpaiMarketplace.Controllers
{
    public class LessonsController : ApiController
    {
        private SenpaiDatabase db = new SenpaiDatabase();

        // GET: api/Lessons
        public List<Lesson> GetLessons()
        {
            List<Lesson> lessons = new List<Lesson>();
            
            using (StreamReader sr = new StreamReader("Lessons\\lessons.txt"))
            {
                while(!sr.EndOfStream)
                {
                    lessons.Add(new Lesson(sr.ReadLine()));
                }
            }

            return lessons;
        }

        // GET: api/Lessons/5
        [ResponseType(typeof(Lesson))]
        public IHttpActionResult GetLesson(int id)
        {
            String path = "Lessons\\" + id + ".txt";

            if (!File.Exists(path))
            {
                return NotFound();
            }

            Lesson lesson = new Lesson();
                        
            using (StreamReader sr = new StreamReader(path))
            {
                lesson.Fill(sr.ReadLine());

                while (!sr.EndOfStream)
                {
                    lesson.AddItem(sr.ReadLine());
                }
            }

            return Ok(lesson);
        }

        // PUT: api/Lessons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLesson(List<Lesson> lessonsToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //check if lessons exists
            foreach (Lesson lesson in lessonsToUpdate)
            {
                String path = "Lessons\\" + lesson.Id + ".txt";

                if (!File.Exists(path))
                {
                    return NotFound();
                }
            }

            //update lessons
            foreach (Lesson lesson in lessonsToUpdate)
            {
                String path = "Lessons\\" + lesson.Id + ".txt";

                using (StreamWriter sw = new StreamWriter(File.Create(path)))
                {
                    sw.WriteLine(lesson.ToCreateNewString());

                    foreach (ILessonItem item in lesson.Words)
                    {
                        sw.WriteLine(item.ToCreateNewString());
                    }

                    foreach (ILessonItem item in lesson.Kanjis)
                    {
                        sw.WriteLine(item.ToCreateNewString());
                    }

                    foreach (ILessonItem item in lesson.Clozes)
                    {
                        sw.WriteLine(item.ToCreateNewString());
                    }
                }
            }

            //update lessons.txt
            List<Lesson> allLessons = GetLessons();
            
            foreach(Lesson updateLesson in lessonsToUpdate)
            {
                foreach(Lesson lesson in allLessons)
                {
                    if(lesson.Id == updateLesson.Id)
                    {
                        lesson.Fill(updateLesson);
                        break;
                    }
                }
            }
            
            String lessonsFile = "Lessons\\lessons.txt";

            using (StreamWriter sw = new StreamWriter(File.Create(lessonsFile)))
            {
                foreach (Lesson lesson in allLessons)
                {
                    sw.WriteLine(lesson.ToCreateNewString());
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Lessons
        [ResponseType(typeof(List<Lesson>))]
        public IHttpActionResult PostLesson(List<Lesson> lessons)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            //look if lessons exist
            foreach (Lesson lesson in lessons)
            {
                if(File.Exists("Lessons\\" + lesson.Id + ".txt"))
                {
                    return Conflict();
                }
            }

            //create lesson files
            foreach (Lesson lesson in lessons)
            {
                using (StreamWriter sw = new StreamWriter("Lessons\\" + lesson.Id + ".txt"))
                {
                    sw.WriteLine(lesson.ToCreateNewString());

                    foreach (ILessonItem item in lesson.Words)
                    {
                        sw.WriteLine(item.ToCreateNewString());
                    }

                    foreach (ILessonItem item in lesson.Kanjis)
                    {
                        sw.WriteLine(item.ToCreateNewString());
                    }

                    foreach (ILessonItem item in lesson.Clozes)
                    {
                        sw.WriteLine(item.ToCreateNewString());
                    }
                }
            }

            //update lessons.txt
            List<Lesson> allLessons = GetLessons();

            allLessons.AddRange(lessons);
            allLessons = allLessons.OrderBy(x => x.Id).ToList();

            using (StreamWriter sw = new StreamWriter(File.Create("Lessons\\lessons.txt")))
            {
                foreach (Lesson lesson in allLessons)
                {
                    sw.WriteLine(lesson.ToCreateNewString());
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = lessons[0].Id }, lessons);
        }

        // DELETE: api/Lessons/5
        [ResponseType(typeof(Lesson))]
        public IHttpActionResult DeleteLesson(int id)
        {
            String file = "Lessons\\" + id + ".txt";

            if (!File.Exists(file))
            {
                return NotFound();
            }

            String tempFile = Path.GetTempFileName();
            String lessonsFile = "Lessons\\lessons.txt";
            
            //Update lessons.txt and remove lesson line
            using (StreamReader sr = new StreamReader(lessonsFile))
            using (StreamWriter sw = new StreamWriter(tempFile))
            {
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    String lessonID = line.Substring(0, line.IndexOf('|'));
                    if (lessonID != id.ToString())
                    {
                        sw.WriteLine(line);
                    }
                }
            }
            
            File.Delete(lessonsFile);
            File.Move(tempFile, lessonsFile);

            File.Delete(file);

            return Ok(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LessonExists(int id)
        {
            return db.Lessons.Count(e => e.Id == id) > 0;
        }
    }
}