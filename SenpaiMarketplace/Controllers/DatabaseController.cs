using SenpaiMarketplace.Database;
using SenpaiMarketplace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SenpaiMarketplace.Controllers
{
    public class DatabaseController : ApiController
    {
        // GET api/database
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/database/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/database
        public void Post([FromBody]string value)
        {

        }

        // PUT api/database/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/database/5
        public void Delete()
        {
            SenpaiDatabase db = new SenpaiDatabase();
            db.Database.Delete();
            db.Database.Create();
            
            Lesson lesson = new Lesson("Tobira L1|0|4");
            Word w1 = new Word("ぎじゅつ|技術|Technologie, Technik, Fähigkeit||0");
            Word w2 = new Word("はったつする|発達する|entwickeln, wachsen||3");
            Word w3 = new Word("しゅじゅつ|手術|Operation, chirurgischer Eingriff|～をする|0");
            Word w4 = new Word("こえ|声|Stimme||0");
            
            w1.Lesson = lesson;
            w2.Lesson = lesson;
            w3.Lesson = lesson;
            w4.Lesson = lesson;

            lesson.Words = new List<Word>();

            lesson.Words.Add(w1);
            lesson.Words.Add(w2);
            lesson.Words.Add(w3);
            lesson.Words.Add(w4);

            db.Lessons.Add(lesson);
            db.SaveChanges();

            db.Dispose();
        }
    }
}
