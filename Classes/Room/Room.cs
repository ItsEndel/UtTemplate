using Godot;
using System;

public class Room : Node
{
    // 主节点
    protected Camera2D camera;
    protected Node maps;
    protected Node objects;
    protected Player player;

    // 属性
    protected bool cameraLocked = false;

    // GD方法
    public override void _Ready() {
        // 获取主节点
        camera = GetNode<Camera2D>("Camera");
        maps = GetNode<Node>("Maps");
        objects = GetNode<Node>("Objects");
        player = GetNode<Player>("Player");
    }

    public override void _Process(float delta)
    {
        // 镜头跟随
        if (!cameraLocked) {
            camera.Position = player.Position;
        }
    }
}
