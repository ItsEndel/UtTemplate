using Godot;
using System;

public class Savepoint : Object
{
    // 触发方法
    public override void triggered(Area2D area)
    {
        GD.Print("triggered");
    }
}
