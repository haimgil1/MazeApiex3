using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeGeneratorLib;
using MazeLib;
using System.IO;
using Microsoft.AspNet.SignalR.Hubs;

namespace AP2ex3.Models
{
    //public delegate void ChangedEventHandler(object sender, EventArgs e);

    //public delegate void ClienetPlayedEventHandler(Direction direction);


    /// <summary>
    /// Game class
    /// </summary>
    public class Game
    {
        private string client1;
        private string client2;
        private Direction guestCurrentDirection;
        private Direction otherPlayerCurrDirection;
        private Maze maze;

        //public event ChangedEventHandler SomebodyClosedTheGameEvent;
        //public event ClienetPlayedEventHandler GuestPlayedEvent;
        //public event ClienetPlayedEventHandler HostPlayActionOccurd;

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


        public Maze Maze
        {
            get { return maze; }
            set
            {
                maze = value;
            }
        }


        //public Direction OtherPlayerCurrDirection
        //{
        //    get { return otherPlayerCurrDirection; }
        //    set
        //    {
        //        otherPlayerCurrDirection = value;
        //        HostPlayActionOccurd?.Invoke(otherPlayerCurrDirection);
        //    }
        //}


        //public Direction GuestCurrentDirection
        //{
        //    get { return guestCurrentDirection; }
        //    set
        //    {
        //        guestCurrentDirection = value;
        //        GuestPlayedEvent?.Invoke(guestCurrentDirection);
        //    }
        //}


        /// <summary>
        /// Joins the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        public void Join(string client)
        {
            this.client2 = client;
            // this.SendToClients();

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
















