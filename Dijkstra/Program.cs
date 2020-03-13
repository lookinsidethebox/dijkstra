using System;
using System.Collections.Generic;

namespace Dijkstra
{
	class Point
	{
		public string Name { get; set; }
		public int PathWeight { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{
			var graph = new Dictionary<string, List<Point>>();
			graph.Add("start", new List<Point>
			{
				new Point { Name = "a", PathWeight = 5 },
				new Point { Name = "b", PathWeight = 4 }
			});
			graph.Add("a", new List<Point>
			{
				new Point { Name = "c", PathWeight = 2 },
				new Point { Name = "d", PathWeight = 3 }
			});
			graph.Add("b", new List<Point>
			{
				new Point { Name = "a", PathWeight = 2 },
				new Point { Name = "c", PathWeight = 5 },
				new Point { Name = "d", PathWeight = 5 }
			});
			graph.Add("c", new List<Point>
			{
				new Point { Name = "end", PathWeight = 3 },
				new Point { Name = "d", PathWeight = 1 }
			});
			graph.Add("d", new List<Point>
			{
				new Point { Name = "end", PathWeight = 1 }
			});
			graph.Add("end", new List<Point>());

			var weights = new Dictionary<string, double>();
			weights.Add("a", 5);
			weights.Add("b", 4);
			weights.Add("c", double.PositiveInfinity);
			weights.Add("d", double.PositiveInfinity);
			weights.Add("end", double.PositiveInfinity);

			FindBestPath(graph, weights);

			Console.WriteLine($"Для того, чтобы добраться до последней вершины, потребуется {weights["end"]} мин.");
			Console.ReadLine();
		}

		static void FindBestPath(Dictionary<string, List<Point>> graph, Dictionary<string, double> weights)
		{
			var checkedPoints = new List<string>();
			var lowestWeightKey = FindLowestWeight(weights, checkedPoints);
			while (lowestWeightKey != string.Empty)
			{
				var weight = weights[lowestWeightKey];
				var neightbors = graph[lowestWeightKey];
				foreach (var neightbor in neightbors)
				{
					var newWeight = weight + neightbor.PathWeight;
					if (weights[neightbor.Name] > newWeight)
					{
						weights[neightbor.Name] = newWeight;
					}
				}
				checkedPoints.Add(lowestWeightKey);
				lowestWeightKey = FindLowestWeight(weights, checkedPoints);
			}
		}

		static string FindLowestWeight(Dictionary<string, double> weights, List<string> checkedPoints)
		{
			var lowestWeight = double.PositiveInfinity;
			var lowestWeightKey = string.Empty;
			foreach (var weight in weights)
			{
				if (weight.Value < lowestWeight && !checkedPoints.Contains(weight.Key))
				{
					lowestWeightKey = weight.Key;
					lowestWeight = weight.Value;
				}
			}
			return lowestWeightKey;
		}
	}
}
