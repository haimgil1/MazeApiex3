using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using AP2ex3.Models;
using MazeLib;
using Microsoft.AspNet.SignalR.Hubs;


namespace AP2ex3
{

    public class MultiPlayerHub1 : Hub
    {
        private static IModel model = new Model();

        public void StartGame(string name, int rows, int cols)
        {
            // current player
            string clientId = Context.ConnectionId;
            Maze maze = model.AddStartGame(name, rows, cols, clientId).Maze;
        }

        public Maze JoinTo(string name)
        {
            // current player
            string clientId = Context.ConnectionId;

            Maze maze = model.JoinToGame(name, Context.ConnectionId).Maze;
            return maze;
            //Clients.Client(clientId).drawBoard("myCanvas", maze,
            //    "Views/Images/minion.gif", "Views/Images/Exit.png", true);
            //// competitor
            //string opponentId = model.GetCompetitorOf(clientId);
            //Clients.Client(opponentId).drawBoard("competitorCanvas", maze,
            //    "Views/Images/pokemon.gif", "Views/Images/Exit.png", false);
        }

        public void Play(string direction)
        {
            //string playerId = Context.ConnectionId;
            //model.Play(direction, playerId);
            //string opponentId = model.GetCompetitorOf(playerId);
            //Clients.Client(opponentId).moveOtherPlayer(direction);
        }

        public void Close(string nameOfGame)
        {
            //model.Close(nameOfGame);
            //string opponentId = model.GetCompetitorOf(Context.ConnectionId);
            //// TODO: implement 'closeGame' method in client side.
            //Clients.Client(opponentId).closeGame();
        }


        public List<string> GetAvailablesGame()
        {
            return model.GetList();
        }
    }
}