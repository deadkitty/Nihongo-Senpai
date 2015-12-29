using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SenpaiMarketplace.Controllers
{
    public class ClozesController : ApiController
    {
        // GET api/clozes
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/clozes/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/clozes
        public void Post([FromBody]string value)
        {
        }

        // PUT api/clozes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/clozes/5
        public void Delete(int id)
        {
        }
    }
}
