using System;
using SearchAlgorithmsLib;
using MazeLib;
using System.Collections.Generic;
using System.Text;

namespace ObjectAdapter
{
    /// <summary>
    /// Maze adapter.
    /// </summary>
    public class MazeAdapter : ISearchable<Position>
    {
        /// <summary>
        /// The maze.
        /// </summary>
        private Maze maze;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ObjectAdapter.MazeAdapter"/> class.
        /// </summary>
        /// <param name="maze">Maze.</param>
        public MazeAdapter(Maze maze)
        {
            this.maze = maze;
        }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns>The initial state.</returns>
        public State<Position> GetInitialState()
        {
            State<Position> state = State<Position>.StatePool.GetState(this.maze.InitialPos);
            state.Cost = 0;
            return state;
        }

        /// <summary>
        /// Gets the state of the IG oall.
        /// </summary>
        /// <returns>The IG oall state.</returns>
        public State<Position> GetIGoallState()
        {
            return State<Position>.StatePool.GetState(this.maze.GoalPos);
        }

        public void updateCost(State<Position> cameFrom, State<Position> current)
        {
            cameFrom.Cost = current.Cost + 1;
        }

        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <returns>The all possible states.</returns>
        /// <param name="s">S.</param>
        public List<State<Position>> GetAllPossibleStates(State<Position> s)
        {
            List<State<Position>> possibleStates = new List<State<Position>>();
            int i = s.GetState().Row;
            int j = s.GetState().Col;
            addNeighbor(0, i - 1, j, i, s, possibleStates);
            addNeighbor(i + 1, i + 1, j, maze.Rows, s, possibleStates);
            addNeighbor(0, i, j - 1, j, s, possibleStates);
            addNeighbor(j + 1, i, j + 1, maze.Cols, s, possibleStates);
            return possibleStates;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldpath"></param>
        /// <param name="newpath"></param>
        /// <returns></returns>
        public bool isBetterWay(State<Position> oldpath, State<Position> newpath)
        {
            return oldpath.Cost > (newpath.Cost + 1);
        }

        /// <summary>
        /// Adds the neighbor.
        /// </summary>
        /// <param name="k">K.</param>
        /// <param name="i">The index.</param>
        /// <param name="j">J.</param>
        /// <param name="rowCol">Row col.</param>
        /// <param name="s">S.</param>
        /// <param name="possibleStates">Possible states.</param>
        private void addNeighbor(int k, int i, int j, int rowCol, State<Position> s, List<State<Position>> possibleStates)
        {
            if ((k < rowCol) && (maze[i, j] == CellType.Free))
            {
                State<Position> state = State<Position>.StatePool.GetState(new Position(i, j));
                possibleStates.Add(state);
            }

        }

        /// <summary>
        /// Tos the string.
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="solution">Solution.</param>
        public static string ToString(Solution<Position> solution)
        {
            StringBuilder sb = new StringBuilder();
            Stack<State<Position>> printStack = new Stack<State<Position>>(solution.Queue);
            while (printStack.Count > 0)
            {
                State<Position> state = printStack.Pop();
                if (state.CameFrom != null)
                {
                    appendDirection(sb, state.GetState().Row, state.CameFrom.GetState().Row, (int)MazeLib.Direction.Up);
                    appendDirection(sb, state.CameFrom.GetState().Row, state.GetState().Row, (int)MazeLib.Direction.Down);
                    appendDirection(sb, state.GetState().Col, state.CameFrom.GetState().Col, (int)MazeLib.Direction.Left);
                    appendDirection(sb, state.CameFrom.GetState().Col, state.GetState().Col, (int)MazeLib.Direction.Right);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Appends the direction.
        /// </summary>
        /// <param name="sb">Sb.</param>
        /// <param name="state">State.</param>
        /// <param name="stateCameFrom">State came from.</param>
        /// <param name="direction">Direction.</param>
        public static void appendDirection(StringBuilder sb, int state, int stateCameFrom, int direction)
        {
            if (state > stateCameFrom)
            {
                sb.Append(direction);
            }
        }
    }
}