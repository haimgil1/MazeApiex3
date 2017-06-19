using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeGeneratorLib;
using MazeLib;
using System.IO;

namespace AP2ex3.Models
{
    /// <summary>
    /// Game class
    /// </summary>
    public class Game
    {
        private string client1;
        private string client2;
        private Maze maze;
        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="maze">The maze.</param>
        public Game(string client, Maze maze)
        {
            this.client1 = client;
            this.maze = maze;
        }
        /// <summary>
        /// Joins the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        public void Join(string client)
        {
            this.client2 = client;
            this.SendToClients();

        }
        /// <summary>
        /// Gets the first client.
        /// </summary>
        /// <returns></returns>
        public string GetFirstClient()
        {
            return this.client1;
        }
        /// <summary>
        /// Gets the second client.
        /// </summary>
        /// <returns></returns>
        public string GetSecondClient()
        {
            return this.client2;
        }
        /// <summary>
        /// Sends to clients.
        /// </summary>
        public void SendToClients()
        {
            //NetworkStream stream = client1.GetStream();
            //StreamReader reader = new StreamReader(stream);
            //StreamWriter writer = new StreamWriter(stream);
            //writer.WriteLine(maze.ToJSON().Replace("\r\n", ""));
            //writer.Flush();
            //stream = client2.GetStream();
            //reader = new StreamReader(stream);
            //writer = new StreamWriter(stream);
            //writer.WriteLine(maze.ToJSON().Replace("\r\n", ""));
            //writer.Flush();
        }
        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <returns></returns>
        public Maze GetMaze()
        {
            return this.maze;
        }
        /// <summary>
        /// Gets the opponent.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string GetOpponent(string client)
        {
            if (client == client1)
            {
                return this.client2;
            }
            return this.client1;
        }
    }
}
