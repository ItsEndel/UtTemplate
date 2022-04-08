using Godot;
using System;

public class Trigger : Area2D
{
    // 主节点
    private CollisionShape2D shape;

    // GD方法
    public override void _Ready()
    {
        shape = GetNode<CollisionShape2D>("Shape");
    }

    // 互动器方法
    public void Enable() {
        shape.Disabled = false;
    }
    public void Disable() {
        shape.Disabled = true;
    }
}
