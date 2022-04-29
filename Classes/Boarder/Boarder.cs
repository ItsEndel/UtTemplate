using Godot;
using System.Collections.Generic;

public class Boarder : Node2D
{
    // 主节点
    private LightOccluder2D _lightOccluder;
    private Polygon2D _background;
    private Line2D _frame;
    private List<Frame> _frames = new List<Frame>{};

    // 属性
    public Vector2[] Polygon 
    {
        get {
            return _polygon;
        }
        set {
            _polygon = value;
            updatePolygon();
        }
    }
    private Vector2[] _polygon = new Vector2[] {
        new Vector2(560, 0),
        new Vector2(560, 140),
        new Vector2(0, 140)
    };

    // GD方法
    public override void _Ready()
    {
        // 获取子节点
        _lightOccluder = GetNode<LightOccluder2D>("LightOccluder");
        _background = GetNode<Polygon2D>("Background");
        _frame = GetNode<Line2D>("Frame");

        // 更新多边形
        updatePolygon();
    }

    // 更新多边形方法
    private void updatePolygon() {
        // 清除碰撞节点
        foreach (Frame i in _frames)
        {
            i.QueueFree();
        }

        // 复制多边形数组
        Vector2[] polygon = new Vector2[_polygon.Length + 1];
        polygon[0] = new Vector2(0, 0);
        _polygon.CopyTo(polygon, 1);

        Vector2[] points = new Vector2[polygon.Length + 1];
        points[points.Length - 1] = new Vector2(0, 0);
        polygon.CopyTo(points, 0);

        // 修正多边形数组数据
        for (int i = 0; i < polygon.Length; i++)
        {
            polygon[i] = new Vector2(
                polygon[i].x - Game.GetSymbol(polygon[i].x) * 10,
                polygon[i].y - Game.GetSymbol(polygon[i].y) * 10
            );
        }
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new Vector2(
                points[i].x - Game.GetSymbol(points[i].x) * 10,
                points[i].y - Game.GetSymbol(points[i].y) * 10
            );
        }

        // 修改多边形
        _lightOccluder.Occluder.Polygon = polygon;
        _background.Polygon = polygon;
        _frame.Points = points;

        // 生成碰撞节点
        for (int i = 0; i + 1 < polygon.Length; i++)
        {
            newFrame(polygon[i], polygon[i + 1]);
        }
        newFrame(polygon[polygon.Length - 1], polygon[0]);
    }

    // 生成碰撞节点方法
    private Frame newFrame(Vector2 from, Vector2 to) {
        Frame frame = new Frame(from.DistanceTo(to) / 2);
        frame.Position = from.LinearInterpolate(to, 0.5f);
        frame.Rotation = from.AngleToPoint(to);

        _frames.Add(frame);
        AddChild(frame);

        return frame;
    }
}
