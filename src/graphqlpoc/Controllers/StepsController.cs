using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace graphqlpoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpGet("{userid}")]
        public ActionResult<string> GetUserDetails(string userid)
        {
            return "UserDetails for " + userid;
        }

        [HttpGet("{userid}")]
        public ActionResult<IEnumerable<string>> GetUserSteps(string userid)
        {
            return Enumerable.Range(1,5).Select(i => "UserSteps " + i + " for " + userid).ToArray();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
