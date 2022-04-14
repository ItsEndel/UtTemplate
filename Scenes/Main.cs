using Godot;
using System;

public class Main : Node
{
    // 主节点
    private AudioStreamPlayer _bgm; // 背景音乐
    private Node _sounds; // 音效
    private Node _scene; // 当前场景

    // 启动场景
    [Export] private PackedScene _initScene = GD.Load<PackedScene>("res://Scenes/Init.tscn");

    // GD方法
    public override void _Ready()
    {
        // 获取主节点
        _bgm = GetNode<AudioStreamPlayer>("BGM");
        _sounds = GetNode<Node>("Sounds");
        // 初始化
        Game.MainScene = this; // 设置主场景
        GD.Randomize(); // 初始化随机数种子
        OS.SetWindowTitle("UNDERTALE"); // 修改窗口名字
        SetScene(_initScene); // 加载启动场景
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_fullscreen")) {
            OS.WindowFullscreen = !OS.WindowFullscreen;
        }
    }

    // 场景方法
    public void SetScene(PackedScene scene) {
        if (_scene != null) {
            _scene.QueueFree();
        }

        _scene = scene.Instance();
        AddChild(_scene);
    }

    // 音频方法
    public AudioStreamPlayer SetBGM(AudioStream audio) {
        _bgm.Stream = audio;
        _bgm.Play();

        return _bgm;
    }
    public Sound PlaySound(AudioStream audio) {
        Sound newSound = new Sound(audio);
        _sounds.AddChild(newSound);

        return newSound;
    }
}
