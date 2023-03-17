using UnityEngine;

public static class Math
{
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

