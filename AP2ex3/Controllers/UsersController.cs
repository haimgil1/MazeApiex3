using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AP2ex3.Models;
using System.Security.Cryptography;
using System.Text;

namespace AP2ex3.Controllers
{
    public class UsersController : ApiController
    {
        private AP2ex3Context db = new AP2ex3Context();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {

            return db.Users.OrderBy(u => u.Losses - u.Wins);
        }
        [HttpPost]
        [Route("api/user/login")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> CheckPassword(User newUser)
        {
            User user = await db.Users.FindAsync(newUser.UserName);
            if (user == null)
            {
                return NotFound();
            }
            string hash = ComputeHash(newUser.Password);

            if (hash == user.Password)
            {
                return Ok(user);
            }
            return Conflict();
        }
        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/Winning/5
        [Route("api/Users/{id}/{winOrLose}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserWinning(string id, string winOrLose)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = await db.Users.FindAsync(id);
            if(winOrLose == "Win")
            {
                user.Wins++;
            }
            else
            {
                user.Losses++;
            }

            db.Entry(user).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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


        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            string password = this.ComputeHash(user.Password);
            user.Password = password;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Users.Add(user);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.UserName }, user);
        }



        private string ComputeHash(string input)
        {
            SHA1 sha = SHA1.Create();
            byte[] buffer = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha.ComputeHash(buffer);
            string hash64 = Convert.ToBase64String(hash);
            return hash64;
        }
        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.UserName == id) > 0;
        }
    }
}