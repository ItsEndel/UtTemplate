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

    public override void _Process(float delta)
    {
        base._Process(delta);
        // 前景跟随
        _foreground.Position = new Vector2((_camera.Position.x - 320) * -1f, 0);
    }
}
