using Godot;
using System;

public class TestRoom : Room
{
    // 主节点
    private Node2D _foreground;

    public override void _Ready()
    {
        base._Ready();
        // 获取主节点
        _foreground = _maps.GetNode<Node2D>("Foreground");
        _player.SetSkin("shadow");
    }

    // 镜头跟随
    protected override void CameraFollow()
    {
        _camera.Position = new Vector2(Mathf.Clamp(_player.Position.x, 320, 2480), 240);
        // 前景跟随
        _foreground.Position = new Vector2((_camera.Position.x - 320) * -1f, 0);
    }
}
