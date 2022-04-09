using Godot;
using System;

public class test : Node2D
{
    private Vector2[] _points = new Vector2[] {
        new Vector2(100, 250), new Vector2(400, 250), new Vector2(160, 450), new Vector2(250, 125), new Vector2(340, 450), new Vector2(100, 250)
    };

    public override void _Ready()
    {
        Update();
    }

    public override void _Draw()
    {
        for (int i = 0; i + 1 < _points.Length; i++)
        {
            DrawLine(_points[i], _points[i + 1], new Color(0, 0, 1));
        }

        for (float i = 0; i + 0.01f < 1; i += 0.01f) {
            DrawLine(BezierCurve.Get(_points, i), BezierCurve.Get(_points, i + 0.01f), new Color(1, 0, 0));
        }
    }
}
