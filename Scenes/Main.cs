using Godot;
using System;
using System.Text.RegularExpressions;

public class Main : Node
{
    // 主节点
    private Node _sounds;

    // 启动场景
    [Export] private PackedScene _launchScene = GD.Load<PackedScene>("res://Scenes/Rooms/Test/TestRoom.tscn");

    // GD方法
    public override void _Ready()
    {
        // 获取主节点
        _sounds = GetNode<Node>("Sounds");
        // 初始化
        Game.MainScene = this; // 设置主场景
        GD.Randomize(); // 初始化随机数种子
        OS.SetWindowTitle("UNDERTALE"); // 修改窗口名字
        this.AddChild(_launchScene.Instance()); // 加载启动场景
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_fullscreen")) {
            OS.WindowFullscreen = !OS.WindowFullscreen;
        }
    }

    // 音效方法
    public void PlaySound(AudioStream audio) {
        Sound newSound = new Sound(audio);
        _sounds.AddChild(newSound);
    }
}
