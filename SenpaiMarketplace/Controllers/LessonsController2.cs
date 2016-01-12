
using SenpaiMarketplace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SenpaiMarketplace.Controllers
{
    //public class LessonsController : ApiController
    //{
    //    private SenpaiDatabase database = new SenpaiDatabase();
        
    //    [Route("api/Lessons")]
    //    public List<Lesson> Get()
    //    {
    //        List<Lesson> lessons = database.Lessons.ToList();

    //        List<Lesson> returnValues = new List<Lesson>();

    //        return CopyLessons(lessons);
    //    }
        
    //    [Route("api/Lessons/{id}")]
    //    public Lesson Get(int id)
    //    {
    //        return database.Lessons.Where(c => c.Id == id).FirstOrDefault();
    //    }
        
    //    //[Route("api/Lessons/type/{type}")]
    //    //public List<Lesson> Get(int type)
    //    //{
    //    //    List<Lesson> lessons;
            
    //    //    if((Lesson.EType)type == Lesson.EType.undefined)
    //    //    {
    //    //        lessons = database.Lessons.ToList();
    //    //    }
    //    //    else
    //    //    {
    //    //        lessons = database.Lessons.Where(c => c.Type == type).ToList();
    //    //    }

    //    //    return CopyLessons(lessons);
    //    //}

    //    //[Route("api/Lessons/search/{searchText}")]
    //    //public List<Lesson> Get(String searchText)
    //    //{
    //    //    List<Lesson> lessons;
            
    //    //    if(searchText == "")
    //    //    {
    //    //        lessons = database.Lessons.ToList();
    //    //    }
    //    //    else
    //    //    {
    //    //        lessons = database.Lessons.Where(c => c.Name.Contains(searchText)).ToList();
    //    //    }

    //    //    return CopyLessons(lessons);
    //    //}

    //    [Route("api/Lessons/type/{type}/search/{searchText?}")]
    //    public List<Lesson> Get(int type, String searchText = "")
    //    {
    //        List<Lesson> lessons;
            
    //        if(searchText == "")
    //        {
    //            if((Lesson.EType)type == Lesson.EType.undefined)
    //            {
    //                lessons = database.Lessons.ToList();
    //            }
    //            else
    //            {
    //                lessons = database.Lessons.Where(c => c.Type == type).ToList();
    //            }
    //        }
    //        else
    //        {
    //            if((Lesson.EType)type == Lesson.EType.undefined)
    //            {
    //                lessons = database.Lessons.Where(c => c.Name.Contains(searchText)).ToList();
    //            }
    //            else
    //            {
    //                lessons = database.Lessons.Where(c => c.Type == type && c.Name.Contains(searchText)).ToList();
    //            }
    //        }

    //        return CopyLessons(lessons);
    //    }

    //    [Route("api/Lessons")]
    //    public List<Lesson> Post(List<Lesson> lessons)
    //    {
    //        foreach(Lesson lesson in lessons)
    //        {
    //            foreach(Word word in lesson.Words)
    //            {
    //                word.Lesson = lesson;
    //            }

    //            foreach(Kanji kanji in lesson.Kanjis)
    //            {
    //                kanji.Lesson = lesson;
    //            }

    //            foreach(Cloze cloze in lesson.Clozes)
    //            {
    //                cloze.Lesson = lesson;
    //            }
    //        }

    //        database.Lessons.AddRange(lessons);

    //        database.SaveChanges();

    //        return CopyLessons(lessons);
    //    }
        
    //    [Route("api/Lessons/{id}")]
    //    public List<int> Put(List<Lesson> lessons)
    //    {
    //        return null;
    //    }
        
    //    [Route("api/Lessons/{id}")]
    //    public List<int> Delete(List<int> ids)
    //    {
    //        return null;
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        database.Dispose();

    //        base.Dispose(disposing);
    //    }

    //    private List<Lesson> CopyLessons(List<Lesson> lessons)
    //    {
    //        //i make a copy of the lessons and return these because otherwise json would serialize all words, kanjis and clozes attached to this 
    //        //lesson to and send them to the client, but i just want to send the lessons itself here
    //        //and JsonIgnore doesn't work well because sometimes i want to ignore the properties and sometimes i don't want it =/ 
    //        //but if i use it the properties are always ignored >.>
    //        List<Lesson> returnValues = new List<Lesson>();
            
    //        foreach(Lesson lesson in lessons)
    //        {
    //            Lesson l = new Lesson(lesson);
    //            returnValues.Add(l);
    //        }

    //        return returnValues;
    //    }
    //}
}
