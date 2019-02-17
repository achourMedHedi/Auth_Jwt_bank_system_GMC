using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTwithNeo4j.Entity
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
