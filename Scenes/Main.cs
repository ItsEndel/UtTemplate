using Godot;
using System;

public class Main : Node
{
    private PackedScene roomScene = GD.Load<PackedScene>("res://Scenes/Rooms/Test/TestRoom.tscn");

    public override void _Ready()
    {
        OS.SetWindowTitle("UNDERTALE");
        this.AddChild(roomScene.Instance());
    }
}
