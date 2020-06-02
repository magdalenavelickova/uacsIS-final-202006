using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

namespace uascWebApi.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static List<string> listOfRecords = new List<string>
            { "FirstRecord", "SecondRecord" };

        // GET api/values/Get
        [HttpGet]
        [Route("Get")]
        public IEnumerable<string> Get()
        {
            return listOfRecords;
        }

        // GET api/values/Get/5
        [HttpGet]
        [Route("Get/{id:int}")]
        public string Get(int id)
        {
            return listOfRecords[id];
        }

        // POST api/values/Create
        [HttpPost]
        [Route("Create")]
        public void Post([FromBody]string value)
        {
            listOfRecords.Add(value);
        }

        // PUT api/values/Modify/5
        [HttpPut]
        [Route("Modify/{id:int}")]
        public void Put(int id, [FromBody]string value)
        {
            listOfRecords[id] = value;
        }

        // DELETE api/values/Remove/5
        [HttpDelete]
        [Route("Remove/{id:int}")]
        public void Delete(int id)
        {
            listOfRecords.RemoveAt(id);
        }
    }
}