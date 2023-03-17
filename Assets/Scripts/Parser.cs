using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum LineType
{
    Main = 0,
    Other,
    Choice
}

public struct Line
{
    public LineType Type;
    public string Speech;

    public Line(LineType type, string speech)
    {
        Type = type;
        Speech = speech;
    }
}

public static class Parser
{

    public static Line[] ReadDialogue(string filename)
    {
        string filepath = string.Format("{0}/Dialogues/{1}.txt", Application.dataPath, filename);
        string [] inlines = File.ReadAllLines(filepath);
        List<Line> outLines = new List<Line>();

        foreach(string line in inlines)
        {
            int code = int.Parse(line.Substring(0, 2).Trim());
            string speech = line.Substring(2);
            outLines.Add(new Line((LineType)code, speech));
        }

        return outLines.ToArray();
    }

}
