using System.Collections.Generic;
using System.Text;
using Graph;
using TMPro;
using UnityEngine;
using Color = System.Drawing.Color;

public class Root : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _sourceStationField;

    [SerializeField]
    private TMP_InputField _destinationStationField;

    [SerializeField]
    private TMP_Text _outputField;
    
    private Graph.Graph _graph;
    private StringBuilder _pathStringBuilder;

    private void Start()
    {
        _pathStringBuilder = new StringBuilder();
        
        _graph = new Graph.Graph(1);

        var stationA = _graph.CreatePoint("A");
        var stationB = _graph.CreatePoint("B");
        var stationC = _graph.CreatePoint("C");
        var stationD = _graph.CreatePoint("D");
        var stationE = _graph.CreatePoint("E");
        var stationF = _graph.CreatePoint("F");
        var stationG = _graph.CreatePoint("G");
        var stationH = _graph.CreatePoint("H");
        var stationJ = _graph.CreatePoint("J");
        var stationK = _graph.CreatePoint("K");
        var stationL = _graph.CreatePoint("L");
        var stationM = _graph.CreatePoint("M");
        var stationN = _graph.CreatePoint("N");
        var stationO = _graph.CreatePoint("O");
        
        //red line
        _graph.AddEdge(stationA, stationB, Color.Red);
        _graph.AddEdge(stationB, stationC, Color.Red);
        _graph.AddEdge(stationC, stationD, Color.Red);
        _graph.AddEdge(stationD, stationE, Color.Red);
        _graph.AddEdge(stationE, stationF, Color.Red);
        
        //black line
        _graph.AddEdge(stationB, stationH, Color.Black);
        _graph.AddEdge(stationH, stationJ, Color.Black);
        _graph.AddEdge(stationJ, stationF, Color.Black);
        _graph.AddEdge(stationF, stationG, Color.Black);
        
        //blue line
        _graph.AddEdge(stationN, stationL, Color.Blue);
        _graph.AddEdge(stationL, stationD, Color.Blue);
        _graph.AddEdge(stationD, stationJ, Color.Blue);
        _graph.AddEdge(stationJ, stationO, Color.Blue);
        
        //green line
        _graph.AddEdge(stationC, stationJ, Color.Green);
        _graph.AddEdge(stationJ, stationE, Color.Green);
        _graph.AddEdge(stationE, stationM, Color.Green);
        _graph.AddEdge(stationM, stationL, Color.Green);
        _graph.AddEdge(stationL, stationK, Color.Green);
        _graph.AddEdge(stationK, stationC, Color.Green);
    }

    public void FindPath()
    {
        var sourceStation = _graph.FindPointByLabel(_sourceStationField.text);
        if (sourceStation == null)
        {
            _outputField.text = $"SourceStation not found, label = {_sourceStationField.text}";
            return;
        }
        
        var destinationStation = _graph.FindPointByLabel(_destinationStationField.text);
        if (destinationStation == null)
        {
            _outputField.text = $"DestinationStation not found, label = {_destinationStationField}";
            return;
        }

        var (path, lineChanges) = Pathfinder.FindPath(_graph, sourceStation, destinationStation);
        if (path == default)
        {
            _outputField.text = "Path not found";
            return;
        }

        FormatPath(path, lineChanges);
        _outputField.text = _pathStringBuilder.ToString();
    }

    private void FormatPath(List<Point> path, int lineChanges)
    {
        _pathStringBuilder.Clear();
        _pathStringBuilder.Append("Path found: ");

        var first = true;
        foreach (var point in path)
        {
            if (!first)
            {
                _pathStringBuilder.Append(" -> ");
            }

            first = false;

            _pathStringBuilder.Append(point.Label);
        }

        _pathStringBuilder.Append($", lineChanges = {lineChanges.ToString()}");
    }
}