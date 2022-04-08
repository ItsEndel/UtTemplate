using Godot;
using System;

public class TestRoom : Room
{
    // 主节点
    private Node2D foreground;

    public override void _Ready()
    {
        base._Ready();
        // 获取主节点
        foreground = maps.GetNode<Node2D>("Foreground");
        player.SetSkin("shadow");
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        // 前景跟随
        foreground.Position = new Vector2((camera.Position.x - 320) * -1f, 0);
    }
}
