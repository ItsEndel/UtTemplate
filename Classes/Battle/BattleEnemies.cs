using Godot;
using System;

public class BattleEnemies : Node2D
{
    // 主节点
    public Enemy[] Enemies {
        get {
            return _enemies;
        }
    }
    private Enemy[] _enemies;

    // GD方法
    public override void _Ready()
    {
        // 获取子节点
        Godot.Collections.Array enemies = GetChildren();
        _enemies = new Enemy[enemies.Count];
        enemies.CopyTo(_enemies, 0);
    }
}
