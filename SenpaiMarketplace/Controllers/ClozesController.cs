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
    public class ClozesController : ApiController
    {
        private SenpaiDatabase db = new SenpaiDatabase();

        // GET: api/Clozes
        public IQueryable<Cloze> GetClozes()
        {
            return db.Clozes;
        }

        // GET: api/Clozes/5
        [ResponseType(typeof(Cloze))]
        public IHttpActionResult GetCloze(int id)
        {
            Cloze cloze = db.Clozes.Find(id);
            if (cloze == null)
            {
                return NotFound();
            }

            return Ok(cloze);
        }

        // PUT: api/Clozes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCloze(int id, Cloze cloze)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cloze.Id)
            {
                return BadRequest();
            }

            db.Entry(cloze).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClozeExists(id))
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

        // POST: api/Clozes
        [ResponseType(typeof(Cloze))]
        public IHttpActionResult PostCloze(Cloze cloze)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clozes.Add(cloze);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cloze.Id }, cloze);
        }

        // DELETE: api/Clozes/5
        [ResponseType(typeof(Cloze))]
        public IHttpActionResult DeleteCloze(int id)
        {
            Cloze cloze = db.Clozes.Find(id);
            if (cloze == null)
            {
                return NotFound();
            }

            db.Clozes.Remove(cloze);
            db.SaveChanges();

            return Ok(cloze);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClozeExists(int id)
        {
            return db.Clozes.Count(e => e.Id == id) > 0;
        }
    }
}