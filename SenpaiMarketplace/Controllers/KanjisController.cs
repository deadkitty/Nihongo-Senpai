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
    public class KanjisController : ApiController
    {
        private SenpaiDatabase db = new SenpaiDatabase();

        // GET: api/Kanjis
        public IQueryable<Kanji> GetKanjis()
        {
            return db.Kanjis;
        }

        // GET: api/Kanjis/5
        [ResponseType(typeof(Kanji))]
        public IHttpActionResult GetKanji(int id)
        {
            Kanji kanji = db.Kanjis.Find(id);
            if (kanji == null)
            {
                return NotFound();
            }

            return Ok(kanji);
        }

        // PUT: api/Kanjis/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKanji(int id, Kanji kanji)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kanji.Id)
            {
                return BadRequest();
            }

            db.Entry(kanji).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KanjiExists(id))
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

        // POST: api/Kanjis
        [ResponseType(typeof(Kanji))]
        public IHttpActionResult PostKanji(Kanji kanji)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kanjis.Add(kanji);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KanjiExists(kanji.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = kanji.Id }, kanji);
        }

        // DELETE: api/Kanjis/5
        [ResponseType(typeof(Kanji))]
        public IHttpActionResult DeleteKanji(int id)
        {
            Kanji kanji = db.Kanjis.Find(id);
            if (kanji == null)
            {
                return NotFound();
            }

            db.Kanjis.Remove(kanji);
            db.SaveChanges();

            return Ok(kanji);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KanjiExists(int id)
        {
            return db.Kanjis.Count(e => e.Id == id) > 0;
        }
    }
}