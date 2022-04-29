using Godot;
using System;

public class Frame : StaticBody2D
{
    // 构造器
    public Frame(float length) {
        // 创建矩形碰撞
        RectangleShape2D shape = new RectangleShape2D();
        shape.Extents = new Vector2(length, 2.5f);

        // 设置碰撞形状
        this._shape.Shape = shape;
    }

    // 主节点
    private CollisionShape2D _shape = new CollisionShape2D();
}
