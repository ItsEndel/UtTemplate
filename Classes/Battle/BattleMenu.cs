using Godot;
using System;

public class BattleMenu : Node2D
{
    // 主节点
    private BattleButtons _buttons;
    private HpName _hpName;
    private TextDisplayer _printer;

    // GD方法
    public override void _Ready()
    {
        // 获取主节点
        _buttons = GetNode<BattleButtons>("Buttons");
        _hpName = GetNode<HpName>("HpName");
    }

    // 打印方法
    public void Print(TextDisplayer printer) {
        ClearPrint();

        _printer = printer;
    }
    public void ClearPrint() {
        if (_printer != null) {
            _printer.QueueFree();
            _printer = null;
        }
    }
}
