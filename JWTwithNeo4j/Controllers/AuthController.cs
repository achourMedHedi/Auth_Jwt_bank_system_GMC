﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JWTwithNeo4j.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver.V1;
using Newtonsoft.Json;

namespace JWTwithNeo4j.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private readonly IDriver _driver;

        public AuthController(IUserService userService)
        {
            _userService = userService;
            _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "Aze123qsd456"));

        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var session = _driver.Session().WriteTransaction(tx => tx.Run("match (p:Person {Username : '"+userParam.Username+"' ,Password : '"+userParam.Password+"' }) return ID(p) as Id , p.Username as username , p.name as firstName ")).FirstOrDefault();
            if (session != null)
            {
                var nodeProps = JsonConvert.SerializeObject(session.Values);
                var user = _userService.Authenticate(JsonConvert.DeserializeObject<User>(nodeProps));
                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });
                return Ok(user);
           
            }
            return NotFound();
            
        }



      

        /// <summary>
        /// create an account
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST: api/Auth
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            _userService.CreatePerson(user);
            return Ok();
        }

        // GET: api/Auth
        [HttpGet]
        public IActionResult GetAll()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var users = _userService.GetAll(claims.First().Value);
            
            return Ok(users);
        }



       
    }
}


//var user = _userService.Authenticate((long)session.Values.Values.FirstOrDefault());
//var session = _driver.Session().WriteTransaction(tx => tx.Run("match (p:Person {Username : '"+userParam.Username+"' ,Password : '"+userParam.Password+"' }) return ID(p) as Id , p.Username as username , p.name as firstName ")).FirstOrDefault().Values;

//var nodeProps = JsonConvert.SerializeObject(session[0].As<INode>().Properties);
// var nodePropss = JsonConvert.SerializeObject(session[0].As<INode>().);

/*var nodeProps = JsonConvert.SerializeObject(session);
var user = _userService.Authenticate(JsonConvert.DeserializeObject<User>(nodeProps));*/
//return Ok(user);


// get all 
/*//var session = _driver.Session().WriteTransaction(tx => tx.Run("match (a:Person {name : 'achor'}) return a as achour"));
        //var users = _userService.GetAll(claims.First().Value);

        //var users = _userService.GetAll("user");
        //return Ok(session.Select(e => e.Values.Values));*/




/*
// GET: api/Auth/5
[HttpGet("{id}", Name = "Get")]
public IActionResult Get(string id)
{
var identity = HttpContext.User.Identity as ClaimsIdentity;
IEnumerable<Claim> claims = identity.Claims;
var users = _userService.GetAll(id);
return Ok(users);
}
*/
