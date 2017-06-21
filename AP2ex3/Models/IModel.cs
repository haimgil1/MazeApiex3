using MazeLib;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using SearchAlgorithmsLib;

namespace AP2ex3.Models
{
    public interface IModel
    {
        
        Maze GenerateMaze(string name, int rows, int cols);
        void DeleteSingleGame(string name);
        string GetBFSSolution(string name);
        string GetDFSSolution(string name);
        //   void AddWaitingGame(string name, int rows, int cols);
        List<string> GetList();
        Maze GetMaze(string name, int rows, int cols);
        void AddStartGame(string name, int rows, int cols, string clientID);

        void DeleteGameFromPlayingGames(string name);
        Game FindGameByClient(string client);
        bool IsContainMazeForSolution(string name);
        bool IsGameAlreadyExist(string name);
        bool IsGameInWaitingList(string name);

        Game GetGameFromWaitingList(string name);
        void JoinToGame(string name);

        void StartAndPlayingMutexWaitOn();
        void StartAndPlayingMutexRealese();
        void MazesMutexWaitOn();
        void MazesMutexRealese();



    }
}