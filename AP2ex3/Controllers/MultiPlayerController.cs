using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MazeLib;
using Newtonsoft.Json.Linq;
using AP2ex3.Models;


namespace AP2ex3.Controllers
{
    public class MultiPlayerController : ApiController
    {
        private static IModel model = new Model();


        [HttpGet]
        //[Route("MultiPlayer/{name}/{rows}/{cols}/{id}")]
        [Route("api/MultiPlayer/StartMultiPlayer/{name}/{rows}/{cols}/{id}")]
        public IHttpActionResult StartMultiGame(string name, int rows, int cols, string id)
        {
            Maze maze = model.AddStartGame(name, rows, cols, id).Maze;
            //Maze maze1 = Maze.FromJSON(maze);
            //return maze.ToJSON();
            JObject obj = JObject.Parse(maze.ToJSON());
            return Ok(obj);
        }

        [HttpGet]
        //[Route("MultiPlayer/{name}/{rows}/{cols}/{id}")]
        [Route("api/MultiPlayer/GetList")]
        public List<string> GetList()
        {
            return model.GetList();
        }

        [HttpGet]
        //[Route("MultiPlayer/JoinGame/{name}")]
        [Route("api/MultiPlayer/JoinGame/{name}")]
        public IHttpActionResult JoinGame(string name)
        {
            Maze maze = model.JoinToGame(name,"koko").Maze;
            JObject obj = JObject.Parse(maze.ToJSON());
            return Ok(obj);
        }
    }
}
