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
    public class WordsController : ApiController
    {
        SenpaiDatabase db = new SenpaiDatabase();

        [Route("api/Words")]
        public List<Word> GetAllWords()
        {
            return db.Words.ToList();
        }

        [Route("api/Words/{id}")]
        public Word GetWord(int id)
        {
            return db.Words.Where(c => c.Id == id).SingleOrDefault();
        }

        [Route("api/Lessons/{lessonID}/Words")]
        public List<Word> GetWords(int lessonID)
        {
            return db.Lessons.Where(c => c.Id == lessonID).SingleOrDefault().Words;
        }
        
        [Route("api/Words")]
        public void Post([FromBody]string value)
        {
        }
        
        [Route("api/Words/{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }
        
        [Route("api/Words/{id}")]
        public void Delete(int id)
        {
        }
    }
}
