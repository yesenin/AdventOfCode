using System.Text;
using System.Text.RegularExpressions;
using Common;

namespace AdventOfCode.Year2025;

public sealed class Day11Part2 : IProblemWithInput
{
    public string GetAnswer()
    {
        var lines = Input.Split('\n', StringSplitOptions.TrimEntries);
        ulong answer = 0;

        var graph = new Dictionary<string, List<string>>();

        foreach (var line in lines)
        {
            var colonParts = line.Split(':', StringSplitOptions.TrimEntries);
            graph.Add(colonParts[0], colonParts[1].Split(' ', StringSplitOptions.TrimEntries).ToList());
        }
        graph.Add("out", new List<string>());
        
        //
        var reverseGraph = new Dictionary<string, List<string>>();
        var getConnections = GetAllConnectionTo("out");
        reverseGraph.Add("out",getConnections);
        var toProcess = getConnections.Select(x => x).ToList();
        var ignored = new List<string>();
        while (toProcess.Count > 0)
        {
            var containsDac = toProcess.Contains("dac");
            var containsFft = toProcess.Contains("fft");
            if (containsDac)
            {
                getConnections = new List<string> { "dac" };
                //ignored.AddRange(toProcess.Where(x => x != "dac"));
            }
            if (containsFft)
            {
                getConnections = new List<string> { "fft" };
                //ignored.AddRange(toProcess.Where(x => x != "fft"));
            }
            toProcess = new List<string>();
            foreach (var connection in getConnections)
            {
                if (ignored.Contains(connection))
                {
                    continue;
                }
                var temp = GetAllConnectionTo(connection);
                reverseGraph.TryAdd(connection, temp);
                toProcess.AddRange(temp.Where(x => !reverseGraph.ContainsKey(x)).ToList());
            }

            getConnections = toProcess.Select(x => x).Distinct().ToList();
        }

        var tempDict = new Dictionary<string, ulong>();
        foreach (var key in graph.Keys)
        {
            tempDict[key] = 0L;
        }
        tempDict["out"] = 1L;
        foreach (var key in reverseGraph.Keys)
        {
            foreach (var c in reverseGraph[key])
            {
                tempDict[c] += tempDict[key];
            }
        }
        
        answer = tempDict["svr"];
        return $"{answer}";

        List<string> GetAllConnectionTo(string node)
        {
            var result = new List<string>();
            foreach (var key in graph.Keys)
            {
                if (graph[key].Contains(node))
                {
                    result.Add(key);
                }
            }
            return result;
        }
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }
}
