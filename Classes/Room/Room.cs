using Godot;
using System;

public class Room : Node
{
    // 主节点
    protected Camera2D _camera;
    protected Node _maps;
    protected Node _objects;
    protected Player _player;

    // 属性
    protected bool cameraLocked = false;

    // GD方法
    public override void _Ready() {
        // 获取主节点
        _camera = GetNode<Camera2D>("Camera");
        _maps = GetNode<Node>("Maps");
        _objects = GetNode<Node>("Objects");
        _player = GetNode<Player>("Player");
    }

    public override void _Process(float delta)
    {
        // 镜头跟随
        if (!cameraLocked) {
            CameraFollow();
        }
    }

    // 镜头跟随
    protected virtual void CameraFollow() {
        _camera.Position = _player.Position;
    }
}
