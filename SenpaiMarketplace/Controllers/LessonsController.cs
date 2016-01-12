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

namespace SenpaiMarketplace.Controllers
{
    public class LessonsController : ApiController
    {
        private SenpaiDatabase db = new SenpaiDatabase();

        // GET: api/Lessons
        public List<Lesson> GetLessons()
        {
            //i have to copy the lessons from db.Lessons to a list, otherwise 
            //the lessons aren't returned. don't know why, but i think the problem is this shit windows 10 , because on windows 8 it worked -.-"
            List<Lesson> lessons = new List<Lesson>();

            lessons.AddRange(db.Lessons);
            //foreach (Lesson lesson in db.Lessons)
            //{
            //    lessons.Add(lesson);
            //}

            return lessons;
        }

        // GET: api/Lessons/5
        [ResponseType(typeof(Lesson))]
        public IHttpActionResult GetLesson(int id)
        {
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }

            return Ok(lesson);
        }

        // PUT: api/Lessons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLesson(int id, Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lesson.Id)
            {
                return BadRequest();
            }

            db.Entry(lesson).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
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

            db.Lessons.AddRange(lessons);

            foreach(Lesson lesson in lessons)
            {
                db.Words .AddRange(lesson.Words);
                db.Kanjis.AddRange(lesson.Kanjis);
                db.Clozes.AddRange(lesson.Clozes);
            }

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lessons[0].Id }, lessons);
        }

        // DELETE: api/Lessons/5
        [ResponseType(typeof(Lesson))]
        public IHttpActionResult DeleteLesson(int id)
        {
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }

            db.Lessons.Remove(lesson);
            db.SaveChanges();

            return Ok(lesson);
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