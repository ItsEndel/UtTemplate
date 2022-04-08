using Godot;
using System;
using System.Collections.Generic;

public class Player : KinematicBody2D
{
    // 主节点
    private CollisionShape2D shape;
    private AnimatedSprite sprite;
    private Trigger interaction;

    // 属性
    //// 外观属性
    [Export] private Dictionary<string, SpriteFrames> skins = new Dictionary<string, SpriteFrames>{
      //{"皮肤名字", GD.Load<SpriteFrames>("res://Classes/Player/SpriteFrames/皮肤文件")},//
        {"normal", GD.Load<SpriteFrames>("res://Classes/Player/SpriteFrames/normal.tres")},
        {"shadow", GD.Load<SpriteFrames>("res://Classes/Player/SpriteFrames/shadow.tres")},
    };  // 外观字典
    //// 移动属性
    private bool movable = true;
    private bool moving = false;
    public bool Moved = false;
    public int Speed = 180;
    //// 互动属性
    public bool CanInteract = true;

    // GD方法
    public override void _Ready()
    {
        shape = GetNode<CollisionShape2D>("Shape");
        sprite = GetNode<AnimatedSprite>("Sprite");
        interaction = GetNode<Trigger>("Trigger");
    }

    public override void _Process(float delta)
    {
        // 帧属性
        Vector2 startPos = Position; // 帧开始位置
        float speed = Speed * delta; // 帧速度

        // 玩家移动
        if (movable) {
            bool moved = false;
            string animation = sprite.Animation;
            if (Input.IsActionPressed("movement_up")) {
                moved = true;
                animation = "up";
                this.MoveAndCollide(speed * Vector2.Up);
                interaction.Position = 32 * Vector2.Up;
            }
            if (Input.IsActionPressed("movement_down")) {
                if (Input.IsActionPressed("movement_up")) {
                    if (startPos == this.Position) {
                        moved = true;
                        animation = "down";
                        this.MoveAndCollide(speed * Vector2.Down);
                        interaction.Position = 32 * Vector2.Down;
                    }
                } else {
                    moved = true;
                    animation = "down";
                    this.MoveAndCollide(speed * Vector2.Down);
                    interaction.Position = 32 * Vector2.Down;
                }
            }
            if (Input.IsActionPressed("movement_right")) {
                moved = true;
                animation = "right";
                this.MoveAndCollide(speed * Vector2.Right);
                interaction.Position = 32 * Vector2.Right;
            }
            if (Input.IsActionPressed("movement_left")) {
                moved = true;
                animation = "left";
                this.MoveAndCollide(speed * Vector2.Left);
                interaction.Position = 32 * Vector2.Left;
            }
            if (moved) {
                sprite.Animation = animation;
                if (startPos != this.Position) {
                    setMoving(true);
                } else {
                    setMoving(false);
                }
            } else {
                setMoving(false);
            }
        }

        // 玩家互动
        interaction.Disable();
        if (CanInteract) {
            if (Input.IsActionJustPressed("movement_interact")) {
                interaction.Enable();
            }
        }

        // 更新属性
        ZIndex = (int)this.Position.y;
        Moved = (startPos == this.Position);
    }

    // 外观方法
    public void SetSkin(string name) {
        if (skins.ContainsKey(name)) {
            sprite.Frames = skins[name];
        } else {
            throw new Exception("Specified skin not found: " + name);
        }
    }
    // 移动方法
    public bool GetMovable() {
        return movable;
    }
    public void SetMovable(bool value) {
        if (moving && !value) {
            setMoving(false);
        }
        movable = value;
    }
    private void setMoving(bool value) {
        if (moving) {
            if (!value) {
                sprite.Frame = 0;
                sprite.Stop();
            }
        } else {
            if (value) {
                sprite.Frame += 1;
                sprite.Play();
            }
        }
        moving = value;
    }
}
