using Godot;
using System.Collections.Generic;

public static class BezierCurve {
    public static Vector2 Get(Vector2[] points, float t) {
        List<Vector2> list1 = new List<Vector2>();
        List<Vector2> list2 = new List<Vector2>(points);

        do
        {
            list1 = list2;
            list2 = new List<Vector2>();
            for (int i = 0; i + 1 < list1.Count; i++)
            {
                list2.Add(list1[i].LinearInterpolate(list1[i + 1], t));
            }
        } while (list2.Count > 1);

        return list2[0];
    }
}
