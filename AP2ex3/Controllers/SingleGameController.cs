using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MazeGeneratorLib;
using MazeLib;
using AP2ex3.Models;
using Server;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;

namespace AP2ex3.Controllers
{
    public class SingleGameController : ApiController
    {
        private static IModel model = new Model();

        [HttpGet]
        [Route("api/SingleGame/GenerateMaze/{name}/{rows}/{cols}")]
        public IHttpActionResult GenerateMaze(string name, int rows, int cols)
        {
            Maze maze = model.GenerateMaze(name, rows, cols);
            //Maze maze1 = Maze.FromJSON(maze);
            if(maze== null)
            {
                return Conflict();
            }
            JObject obj = JObject.Parse(maze.ToJSON());
            return Ok(obj);
        }

        [HttpGet]
        [Route("api/SingleGame/SolveMaze/{name}/{algo}")]
        public string SolveMaze(string name, int algo)
        {
            string solution = null;
            if (algo == 0)
            {
                solution = model.GetBFSSolution(name);
            }
            else
            {
                solution = model.GetDFSSolution(name);
            }
            return solution;
        }

    }
}
