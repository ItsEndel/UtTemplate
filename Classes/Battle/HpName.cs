using Godot;
using System;

public class HpName : Control
{
    // 主节点
    private Label _name;
    private Label _hpNum;
    private TextureRect _hp;
    private TextureRect _kr;
    private TextureProgress _hpBar;
    private TextureProgress _hpOver;

    // GD方法
    public override void _Ready()
    {
        // 获取子节点
        _name = GetNode("Name") as Label;
        _hpNum = GetNode("HpNum") as Label;
        _hp = GetNode("Hp") as TextureRect;
        _kr = GetNode("Kr") as TextureRect;
        _hpBar = GetNode("HpBar") as TextureProgress;
        _hpOver = GetNode("HpBar/HpOver") as TextureProgress;
    }

    // 设置方法
    public void Set(string name, int level, int maxHp, int hp, int kr) {
        // 更新名字
        _name.Text = name + "   LV " + level;

        // 更新最大生命值
        _hpBar.RectScale = new Vector2(maxHp, 20);
        _hpBar.MaxValue = maxHp;
        _hpOver.MaxValue = maxHp;

        // 更新生命值
        _hpBar.Value = hp;
        _hpOver.Value = hp - kr;

        // 更新血条位置
        _kr.RectPosition = new Vector2(
            _hpBar.RectPosition.x + _hpBar.RectScale.x + 5,
            6
        );
        _hpNum.RectPosition = new Vector2(
            _kr.RectPosition.x + _kr.RectSize.x + 15,
            0
        );
    }
}
