using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum LineType
{
    Main = 0,
    Other,
    Choice,
    Answer,
    Inner
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

public static class Utils
{
    public static Line[] ReadDialogue(string filename)
    {
        string filepath = string.Format("{0}/Dialogues/{1}.txt", Application.dataPath, filename);
        string[] inlines = File.ReadAllLines(filepath);
        List<Line> outLines = new List<Line>();

        foreach (string line in inlines)
        {
            int code = int.Parse(line.Substring(0, 2).Trim());
            string speech = line.Substring(2);
            outLines.Add(new Line((LineType)code, speech));
        }

        return outLines.ToArray();
    }

    public static string Trim(string str)
    {
        string outstr = "";
        foreach(char c in str)
        {
            if (c != ' ')
                outstr += c;
        }
        return outstr;
    }

    public static float SquareDistance(Vector2 p0, Vector2 p1)
    {
        Vector2 temp = p0 - p1;
        return temp.x * temp.x + temp.y * temp.y;
    }

    public static float SquareDistance(Vector3 p0, Vector3 p1)
    {
        Vector3 temp = p0 - p1;
        return temp.x * temp.x + temp.y * temp.y + temp.z * temp.z;
    }
}

