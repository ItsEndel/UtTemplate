using Godot;
using System;

public class Menu : Node2D
{
    // 主节点
    public BattleButtons Buttons {
        get {
            return _buttons;
        }
    }
    private BattleButtons _buttons;
    private HpName _hpName;
    private TextDisplayer _printer;

    // GD方法
    public override void _Ready()
    {
        // 获取主节点
        _buttons = GetNode("Buttons") as BattleButtons;
        _hpName = GetNode("HpName") as HpName;
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
