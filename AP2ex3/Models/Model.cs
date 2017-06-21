using System;
using System.Collections.Generic;
using MazeLib;
using MazeGeneratorLib;
using System.Net.Sockets;
using System.Threading;
using SearchAlgorithmsLib;
using ObjectAdapter;


namespace AP2ex3.Models
{
    /// <summary>
    /// model class
    /// </summary>
    /// <seealso cref="Server.IModel" />
    internal class Model : IModel
    {
        private Dictionary<string, Maze> mazes;
        private Dictionary<string, Solution<Position>> solutionsBFS;
        private Dictionary<string, Solution<Position>> solutionsDFS;
        private Dictionary<string, Game> startGames;
        private Dictionary<string, Game> playingGames;
        private static Mutex mazesMutex = new Mutex();
        private static Mutex bfsMutex = new Mutex();
        private static Mutex dfsMutex = new Mutex();
        private static Mutex startMutex = new Mutex();
        private static Mutex playingMutex = new Mutex();

        private ISearcher<Position> BFS;
        private ISearcher<Position> DFS;
        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        public Model()
        {
            playingGames = new Dictionary<string, Game>();
            startGames = new Dictionary<string, Game>();
            mazes = new Dictionary<string, Maze>();
            solutionsBFS = new Dictionary<string, Solution<Position>>();
            solutionsDFS = new Dictionary<string, Solution<Position>>();
            BFS = new BestFirstSearch<Position>();
            DFS = new Dfs<Position>();
        }

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        Maze IModel.GenerateMaze(string name, int rows, int cols)
        {
            Maze maze = this.GetMaze(name, rows, cols);
            mazes.Add(name, maze);
      
            return maze;

        }
        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        public Maze GetMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator generatorMaze = new DFSMazeGenerator();
            Maze maze = generatorMaze.Generate(rows, cols);
            maze.Name = name;
            return maze;
        }

        /// <summary>
        /// Gets the BFS solution.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        string IModel.GetBFSSolution(string name)
        {
            bfsMutex.WaitOne();
            ISearchable<Position> mazeObjectAdapter = new MazeAdapter(mazes[name]);

            if (solutionsBFS.ContainsKey(name))
            {
                return MazeAdapter.ToString (solutionsBFS[name]);
            }
            Solution<Position> solution = BFS.Search(mazeObjectAdapter);
            State<Position>.StatePool.Clear();
            bfsMutex.ReleaseMutex();
            return MazeAdapter.ToString(solution);
            //return solution;
        }
        /// <summary>
        /// Gets the DFS solution.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        string IModel.GetDFSSolution(string name)
        {
            dfsMutex.WaitOne();
            ISearchable<Position> mazeObjectAdapter = new MazeAdapter(mazes[name]);
        
            if (solutionsDFS.ContainsKey(name))
            {
                return MazeAdapter.ToString(solutionsDFS[name]);
            }
            Solution<Position> solution = DFS.Search(mazeObjectAdapter);
            Console.WriteLine("DFS solution: ");
            State<Position>.StatePool.Clear();
            dfsMutex.ReleaseMutex();
            return MazeAdapter.ToString(solution);
           // return solution;
        }
        /// <summary>
        /// Determines whether [is contain maze for solution] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if [is contain maze for solution] [the specified name]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsContainMazeForSolution(string name)
        {
            return mazes.ContainsKey(name);
        }

        /// <summary>
        /// Gets the list of the games.
        /// </summary>
        /// <returns></returns>
        public List<string> GetList()
        {
            startMutex.WaitOne();
            List<string> namesList = new List<string>();
            foreach (string name in startGames.Keys)
            {
                namesList.Add(name);
            }
            startMutex.ReleaseMutex();
            return namesList;
        }
        /// <summary>
        /// Adds the start game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="name">The name.</param>
        public void AddStartGame(string name, int rows, int cols, string clientID)
        {
            Game game = new Game(clientID, this.GetMaze(name, rows, cols));
            startGames.Add(name, game);
        }
        /// <summary>
        /// Determines whether [is game already exist] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if [is game already exist] [the specified name]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsGameAlreadyExist(string name)
        {
            return (this.startGames.ContainsKey(name) || this.playingGames.ContainsKey(name));
        }
        /// <summary>
        /// Determines whether [is game in waiting list] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if [is game in waiting list] [the specified name]; otherwise, <c>false</c>.
        /// </returns>
        bool IsGameInWaitingList(string name)
        {
            return startGames.ContainsKey(name);
        }
        /// <summary>
        /// Joins to game.
        /// </summary>
        /// <param name="name">The name.</param>
        public void JoinToGame(string name)
        {
            Game game = this.startGames[name];
            this.playingGames[name] = game;
            this.startGames.Remove(name);

        }
        /// <summary>
        /// Gets the game from waiting list.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Game GetGameFromWaitingList(string name)
        {
            return this.startGames[name]; ;
        
        }
        /// <summary>
        /// Deletes the game from playing games.
        /// </summary>
        /// <param name="name">The name.</param>
        public void DeleteGameFromPlayingGames(string name)
        {
            playingMutex.WaitOne();
            this.playingGames.Remove(name);
            playingMutex.ReleaseMutex();
        }

        /// <summary>
        /// Finds the game by client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public Game FindGameByClient(string client)
        {
            playingMutex.WaitOne();
            foreach (Game game in this.playingGames.Values)
            {
                if (game.GetFirstClient() == client || game.GetSecondClient() == client)
                {
                    playingMutex.ReleaseMutex();
                    return game;
                }
            }
            playingMutex.ReleaseMutex();
            return null;
        }

        /// <summary>
        /// Starts the and playing mutex wait on.
        /// </summary>
        public void StartAndPlayingMutexWaitOn()
        {
            startMutex.WaitOne();
            playingMutex.WaitOne();
        }

        /// <summary>
        /// Starts the and playing mutex realese.
        /// </summary>
        public void StartAndPlayingMutexRealese()
        {
            startMutex.ReleaseMutex();
            playingMutex.ReleaseMutex();
        }

        /// <summary>
        /// Determines whether [is game in waiting list] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if [is game in waiting list] [the specified name]; otherwise, <c>false</c>.
        /// </returns>
        bool IModel.IsGameInWaitingList(string name)
        {
            return startGames.ContainsKey(name);
        }

        /// <summary>
        /// Mazeses the mutex wait on.
        /// </summary>
        public void MazesMutexWaitOn()
        {
            mazesMutex.WaitOne();
        }

        /// <summary>
        /// Mazeses the mutex realese.
        /// </summary>
        public void MazesMutexRealese()
        {
            mazesMutex.ReleaseMutex();
        }

        public void DeleteSingleGame(string name)
        {
            mazes.Remove(name);
            if (solutionsBFS.ContainsKey(name))
            {
                solutionsBFS.Remove(name);
            }
            if (solutionsDFS.ContainsKey(name))
            {
                solutionsDFS.Remove(name);
            }
        }
    }
}