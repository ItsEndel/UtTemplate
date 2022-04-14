using Godot;
using System;

public class Init : Node
{
    // 属性
    private float _timer = 0;

    // 启动场景
    [Export] private PackedScene _launchScene = GD.Load<PackedScene>("res://Scenes/Rooms/Test/TestRoom.tscn");

    // GD方法
    public override void _Process(float delta)
    {
        _timer += delta;

        if (_timer > 2) {
            Game.MainScene.SetScene(_launchScene);
        }
    }
}
