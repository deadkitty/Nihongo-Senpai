using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SenpaiMarketplace.Controllers
{
    public class KanjisController : ApiController
    {
        // GET api/kanjis
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/kanjis/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/kanjis
        public void Post([FromBody]string value)
        {
        }

        // PUT api/kanjis/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/kanjis/5
        public void Delete(int id)
        {
        }
    }
}
