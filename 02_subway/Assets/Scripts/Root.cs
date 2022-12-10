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

        _graph.AddLine(Color.Red, stationA, stationB, stationC, stationD, stationE, stationF);
        _graph.AddLine(Color.Black, stationB, stationH, stationJ, stationF, stationG);
        _graph.AddLine(Color.Blue, stationN, stationL, stationD, stationJ, stationO);
        _graph.AddLine(Color.Green, stationC, stationJ, stationE, stationM, stationL, stationK, stationC);
    }

    public void FindPath()
    {
        var sourceStation = _graph.FindPointByLabel(_sourceStationField.text.ToUpper());
        if (sourceStation == null)
        {
            _outputField.text = $"SourceStation not found, label = {_sourceStationField.text}";
            return;
        }
        
        var destinationStation = _graph.FindPointByLabel(_destinationStationField.text.ToUpper());
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
                _pathStringBuilder.Append(" - ");
            }

            first = false;

            _pathStringBuilder.Append(point.Label);
        }

        _pathStringBuilder.Append($"\nlineChanges = {lineChanges.ToString()}");
    }
}