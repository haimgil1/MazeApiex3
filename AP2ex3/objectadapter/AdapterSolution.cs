using MazeLib;
using Newtonsoft.Json;
using ObjectAdapter;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Class : AdapterSolution. The class get a solution and can return the Json format.
    /// </summary>
    class AdapterSolution
    {
        private Solution<Position> solution;
        private string name;

        /// <summary>
        /// Constructor AdapterSolution.
        /// </summary>
        /// <param name="newSolution"></param>
        /// <param name="newName"></param>
        /// <param name="newNodesEvaluated"></param>
        public AdapterSolution(Solution<Position> newSolution, string newName)
        {
            this.solution = newSolution;
            this.name = newName;
        }

        /// <summary>
        /// Return the format string of JSON to the object.
        /// </summary>
        /// <returns>String of JSON.</returns>
        public string ToJson()
        {
            string strSolution = MazeAdapter.ToString(this.solution);
            NestedAdapterSolution nested = new NestedAdapterSolution(name, strSolution, solution.EvaluatedNodes);
            return JsonConvert.SerializeObject(nested);
        }

        /// <summary>
        /// Inner class NestedAdapterSolution.
        /// </summary>
        public class NestedAdapterSolution
        {
            // The members are public to using the JSON format.
            public string Name;
            public string Solution;
            public int NodesEvaluated;

            /// <summary>
            /// Constructor of NestedAdapterSolution.
            /// </summary>
            /// <param name="name1"></param>
            /// <param name="solution1"></param>
            /// <param name="numNodes"></param>
            public NestedAdapterSolution(string name1, string solution1, int numNodes)
            {
                this.Name = name1;
                this.Solution = solution1;
                this.NodesEvaluated = numNodes;
            }
        }
    }
}
