using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bank.Core.Bank;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTwithNeo4j.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private IBank<string, string, string> Bank; 
        
        public BankController(IBank<string, string, string> bank) {
            Bank = bank;
        }

        /// <summary>
        /// get bank information
        /// </summary>=
        /// <response code="200"> success Bank information </response>
        /// <returns></returns>
        [ProducesResponseType(typeof(Bank<string , string , string>), (int)HttpStatusCode.OK)]


        // GET: api/Bank
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Bank);
        }


        /// <summary>
        /// create new bank
        /// </summary>=
        /// <response code="200"> success </response>
        /// <returns></returns>
        [ProducesResponseType(typeof(Bank<string, string, string>), (int)HttpStatusCode.OK)]


        // POST: api/Bank
        [HttpPost]
        public IActionResult Post([FromBody] Bank<string, string, string> value)
        {
            Bank = value;
            Bank.Name = "hello";
            return Ok(Bank);
        }

        // PUT: api/Bank/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
