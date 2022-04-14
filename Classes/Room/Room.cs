using Godot;
using System;

public class Room : Node2D
{
    // 主节点
    protected Camera2D _camera;
    protected Node _maps;
    protected Node _objects;
    protected Player _player;

    // 属性
    protected bool _cameraLocked = false;

    // 场景动画
    private byte _sceneAnimation = 0;
    private float _sceneTimer = 0;
    private PackedScene _enteringScene;

    // GD方法
    public override void _Ready() {
        // 获取主节点
        _camera = GetNode<Camera2D>("Camera");
        _maps = GetNode<Node>("Maps");
        _objects = GetNode<Node>("Objects");
        _player = GetNode<Player>("Player");

        // 播放房间进入动画
        EnterRoom();
    }

    public override void _Process(float delta)
    {
        // 镜头跟随
        if (!_cameraLocked) {
            CameraFollow();
        }
        // 场景切换动画
        switch (_sceneAnimation) {
            case 1:
                {
                    RoomEnterAnimation(delta);
                    break;
                }
            case 2:
                {
                    RoomExitAnimation(delta);
                    break;
                }
        }
    }

    // 镜头跟随
    protected virtual void CameraFollow() {
        _camera.Position = _player.Position;
    }

    // 切换场景
    protected void EnterRoom() {
        this.Modulate = new Color(0, 0, 0, 1);
        _sceneAnimation = 1;
        _sceneTimer = 0;
    }
    protected void ExitRoom(PackedScene enteringScene) {
        this.Modulate = new Color(1, 1, 1, 1);
        _sceneAnimation = 2;
        _sceneTimer = 0;
        _enteringScene = enteringScene;
    }
    protected void RoomEnterAnimation(float delta) {
        _sceneTimer += delta;

        if (_sceneTimer > 1) {
            this.Modulate = new Color(1, 1, 1, 1);
            _sceneAnimation = 0;
        } else {
            this.Modulate = new Color(0, 0, 0, 1).LinearInterpolate(new Color(1, 1, 1, 1), _sceneTimer);
        }
    }
    protected void RoomExitAnimation(float delta) {
        _sceneTimer += delta;

        if (_sceneTimer > 1) {
            this.Modulate = new Color(0, 0, 0, 1);
            _sceneAnimation = 0;
            Game.MainScene.SetScene(_enteringScene);
        } else {
            this.Modulate = new Color(1, 1, 1, 1).LinearInterpolate(new Color(0, 0, 0, 1), _sceneTimer);
        }
    }
}
