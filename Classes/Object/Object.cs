using Godot;
using System;

public class Object : Node2D
{
    // 主节点
    protected Trigger trigger;

    // GD方法
    public override void _Ready()
    {
        // 获取主节点
        trigger = GetNode<Trigger>("Trigger");

        // 连接目标触发方法
        trigger.Connect("area_entered", this, nameof(triggered));
    }

    // 目标触发方法
    public virtual void triggered(Area2D area) {}
}
