using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Neo4j.Driver.V1;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTwithNeo4j.Entity
{
    public interface IUserService
    {
        User Authenticate(User userId);
        IEnumerable<User> GetAll(string user);
        void CreatePerson(User user);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private readonly IDriver _driver;
      

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "Aze123qsd456"));

        }

        public User Authenticate(User user)
        {
            
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;
            return user;
        }

        public IEnumerable<User> GetAll(string userId)
        {
            var session = _driver.Session().WriteTransaction(tx => tx.Run("match (p:Person) where ID(p) = "+userId+ " return  ID(p) as Id , p.Username as username , p.name as firstName")).FirstOrDefault();
            var nodeProps = JsonConvert.SerializeObject(session.Values);
            yield return JsonConvert.DeserializeObject<User>(nodeProps);
          
        }
        public void CreatePerson(User user)
        {
            var session = _driver.Session().WriteTransaction(tx => tx.Run("Create (p:Person {name : '"+user.Name+ "' , Username : '" + user.Username + "' , Password : '" + user.Password + "'} )  return p"));

        }
    }
}


//var identity = _users.Identity as ClaimsIdentity;
//var result = _users.Where(x => userId.Equals(x.Id.ToString())).Select(x => { x.Password = "ss"; return x; });
// return users without passwords
/* return _users.Select(x => {
     x.Password = null;
     return x;
 });*/
