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

        // 测试
        string text = "[wait=10]";
        text = text + "测 试 测 试 TesT tESt";
        text = text + "\n换行测试";
        text = text + "\n延迟[wait=1]测试";
        text = text + "\n[size=32][line_space=36][full_char_space=36][half_char_space=20]尺寸测试";
        text = text + "\n[color=(1, 0, 0, 1)]C[color=(0, 1, 0, 1)]O[color=(0, 1, 0, 1)]L[color=(1, 1, 0, 1)]O[color=(0, 1, 1, 1)]R[color=(1, 1, 1, 1)]";
        text = text + "\n[outline_size=2][outline_color=(1, 0, 0, 1)]OUTLINE TEST 轮廓线测试[outline_size=0]";
        text = text + "\n[shake=()]default shake[/shake=]";
        text = text + "\n[shake=(frequency=1000)]faster shake[/shake=]";
        text = text + "\n[shake=(amplitude=5,max_amplitude=25)]bigger shake[/shake=]";
        text = text + "\n[position=(200, 200)]POSITION TEST\n\n";
        for (int i = 0; i < 100; i ++) {
            Color color = Color.FromHsv(i / 100f, 1, 1);
            text = text + "[color=(" + color.r + "," + color.g + "," + color.b + "," + color.a + ")]O";
        }
        TextDisplayer textDisplayer = new TextDisplayer(text);
        AddChild(textDisplayer);
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
