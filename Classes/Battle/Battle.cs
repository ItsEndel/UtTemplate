using Godot;
using System.Collections.Generic;

public class Battle : Node2D
{
    // 主节点
    private Camera2D _camera;
    private CanvasModulate _modulate;
    private BattleEnemies _enemies;
    private BattleMenu _menu;
    private Boarder _boarder;
    private Node _attacksParent;
    private Node _inFrameAttacks;

    // 属性
    private List<Attack> _attacks;
    //// 回合属性
    private int _round; // 回合数
    private bool _playerRound; // 是否为玩家回合
    private float _roundTimer = 0; // 回合计时器
    private float _roundEndTime = 0; // 回合结束时间

    // GD方法
    public override void _Ready()
    {
        // 获取主节点
        _camera = GetNode<Camera2D>("Camera");
        _modulate = GetNode<CanvasModulate>("Modulate");
        _enemies = GetNode<BattleEnemies>("Enemies");
        _menu = GetNode<BattleMenu>("Menu");
        _boarder = GetNode<Boarder>("Boarder");
        _attacksParent = GetNode("Attacks");
        _inFrameAttacks = GetNode("Attacks/InFrame");
    }

    public override void _Process(float delta)
    {
        // 回合控制
        if (_playerRound) { // 玩家回合
            
        } else { // 敌人回合

        }
    }

    // 攻击方法
    public void CreateAttack(Attack attack, bool inFrame = true) {
        if (inFrame) {
            _inFrameAttacks.AddChild(attack);
        } else {
            _attacksParent.AddChild(attack);
        }

        _attacks.Add(attack);
    }
    public void ClearAttack() {
        foreach (Attack i in _attacks)
        {
            i.QueueFree();
        }

        _attacks.Clear();
    }
    public void AttackCollection() {
        foreach (Attack i in _attacks)
        {
            if (!i.IsInsideTree()) {
                _attacks.Remove(i);
            }
        }
    }
}
