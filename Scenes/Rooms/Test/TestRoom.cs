using Godot;
using System;

public class TestRoom : Room
{
    // 主节点
    private Node2D _mapForeground;
    //x=1820

    public override void _Ready()
    {
        base._Ready();
        // 获取主节点
        _mapForeground = _maps.GetNode<Node2D>("Foreground");
        _player.SetSkin("shadow");
        // 播放背景音乐
        Game.MainScene.SetBGM(Game.GetAudio("Music/mus_wind.ogg"));
    }

    // 镜头跟随
    protected override void CameraFollow()
    {
        _camera.Position = new Vector2(Mathf.Clamp(_player.Position.x, 320, 2480), 240);
        // 前景跟随
        _mapForeground.Position = new Vector2((_camera.Position.x - 320) * -1f, 0);
    }
}
