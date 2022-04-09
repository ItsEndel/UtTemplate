using Godot;
using System;

public class Main : Node
{
    // 启动场景
    [Export] private PackedScene _launchScene = GD.Load<PackedScene>("res://Scenes/Rooms/Test/TestRoom.tscn");

    public override void _Ready()
    {
        Game.MainScene = this; // 设置主场景
        GD.Randomize(); // 初始化随机数种子
        OS.SetWindowTitle("UNDERTALE"); // 修改窗口名字
        this.AddChild(_launchScene.Instance()); // 加载启动场景
    }
}
