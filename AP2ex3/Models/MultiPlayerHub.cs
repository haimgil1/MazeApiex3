using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using AP2ex3.Models;
using MazeLib;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace AP2ex3
{
    [HubName("gameHub")]
    public class MultiPlayerHub : Hub
    {
        private static IModel model = new Model();

        public void StartGame(string name, int rows, int cols)
        {
            // current player
            string clientId = Context.ConnectionId;
            model.AddStartGame(name, rows, cols, clientId);
        }

        public void JoinTo(string name)
        {
            // current player
            string clientId = Context.ConnectionId;

            Game game = model.JoinToGame(name, Context.ConnectionId);
            Maze maze = game.Maze;
            JObject obj = JObject.Parse(maze.ToJSON());
            Clients.Client(clientId).sendMaze(obj);

            string otherClientId = game.GetOpponent(clientId);
            Clients.Client(otherClientId).sendMaze(obj);


            //// competitor
            //string opponentId = model.GetCompetitorOf(clientId);
            //Clients.Client(opponentId).drawBoard("competitorCanvas", maze,
            //    "Views/Images/pokemon.gif", "Views/Images/Exit.png", false);
        }

        public void Play(int row, int col)
        {
            string playerId = Context.ConnectionId;
            Game game = model.FindGameByClient(playerId);
            string otherClientId = game.GetOpponent(playerId);
            Clients.Client(otherClientId).sendDirection(row, col);
            //string playerId = Context.ConnectionId;
            //model.Play(direction, playerId);
            //string opponentId = model.GetCompetitorOf(playerId);
            //Clients.Client(opponentId).moveOtherPlayer(direction);
        }

        public void CloseTheGame(string name)
        {
            string playerId = Context.ConnectionId;
            Game game = model.FindGameByClient(playerId);
            string otherClientId = game.GetOpponent(playerId);
            model.DeleteGameFromPlayingGames(name);
            Clients.Client(otherClientId).closeGame();
            //model.Close(nameOfGame);
            //string opponentId = model.GetCompetitorOf(Context.ConnectionId);
            //// TODO: implement 'closeGame' method in client side.
            //Clients.Client(opponentId).closeGame();
        }


        public void GetList()
        {
            string clientId = Context.ConnectionId;
            List<string> list = model.GetList();
            Clients.Client(clientId).sendList(list);
        }
    }
}