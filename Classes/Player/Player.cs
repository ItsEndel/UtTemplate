using Godot;
using System;
using System.Collections.Generic;

public class Player : KinematicBody2D
{
    // 主节点
    private CollisionShape2D _shape;
    private AnimatedSprite _sprite;
    private Trigger _trigger;

    // 属性
    //// 外观属性
    [Export] private Dictionary<string, SpriteFrames> _skins = new Dictionary<string, SpriteFrames>{
      //{"皮肤名字", GD.Load<SpriteFrames>("res://Classes/Player/SpriteFrames/皮肤文件")},//
        {"normal", GD.Load<SpriteFrames>("res://Classes/Player/SpriteFrames/normal.tres")},
        {"shadow", GD.Load<SpriteFrames>("res://Classes/Player/SpriteFrames/shadow.tres")},
    };  // 外观字典
    //// 移动属性
    public bool Movable {
        get {
            return _movable;
        }
        set {
            if (_moving && !value) {
                SetMoving(false);
            }
            _movable = value;
        }
    }
    private bool _movable = true;
    private bool _moving = false;
    public bool Moved = false;
    public int Speed = 180;
    //// 互动属性
    public bool CanInteract = true;

    // GD方法
    public override void _Ready()
    {
        // 获取主节点
        _shape = GetNode<CollisionShape2D>("Shape");
        _sprite = GetNode<AnimatedSprite>("Sprite");
        _trigger = GetNode<Trigger>("Trigger");
    }

    public override void _Process(float delta)
    {
        // 帧属性
        Vector2 startPos = Position; // 帧开始位置
        float speed = Speed * delta; // 帧速度

        // 玩家移动
        if (_movable) {
            bool moved = false;
            string animation = _sprite.Animation;
            if (Input.IsActionPressed("movement_up")) {
                moved = true;
                animation = "up";
                this.MoveAndCollide(speed * Vector2.Up);
                _trigger.Position = 32 * Vector2.Up;
            }
            if (Input.IsActionPressed("movement_down")) {
                if (Input.IsActionPressed("movement_up")) {
                    if (startPos == this.Position) {
                        moved = true;
                        animation = "down";
                        this.MoveAndCollide(speed * Vector2.Down);
                        _trigger.Position = 32 * Vector2.Down;
                    }
                } else {
                    moved = true;
                    animation = "down";
                    this.MoveAndCollide(speed * Vector2.Down);
                    _trigger.Position = 32 * Vector2.Down;
                }
            }
            if (Input.IsActionPressed("movement_right")) {
                moved = true;
                animation = "right";
                this.MoveAndCollide(speed * Vector2.Right);
                _trigger.Position = 32 * Vector2.Right;
            }
            if (Input.IsActionPressed("movement_left")) {
                moved = true;
                animation = "left";
                this.MoveAndCollide(speed * Vector2.Left);
                _trigger.Position = 32 * Vector2.Left;
            }
            if (moved) {
                _sprite.Animation = animation;
                if (startPos != this.Position) {
                    SetMoving(true);
                } else {
                    SetMoving(false);
                }
            } else {
                SetMoving(false);
            }
        }

        // 玩家互动
        _trigger.Disable();
        if (CanInteract) {
            if (Input.IsActionJustPressed("movement_interact")) {
                _trigger.Enable();
            }
        }

        // 更新属性
        ZIndex = (int)this.Position.y;
        Moved = (startPos == this.Position);
    }

    // 外观方法
    public void SetSkin(string name) {
        if (_skins.ContainsKey(name)) {
            _sprite.Frames = _skins[name];
        } else {
            throw new Exception("Specified skin not found: " + name);
        }
    }
    // 移动方法
    private void SetMoving(bool value) {
        if (_moving) {
            if (!value) {
                _sprite.Frame = 0;
                _sprite.Stop();
            }
        } else {
            if (value) {
                _sprite.Frame += 1;
                _sprite.Play();
            }
        }
        _moving = value;
    }
}
