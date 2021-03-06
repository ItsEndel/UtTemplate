using Godot;
using System;

public class BattleButtons : Node2D
{
    // 主节点
    private AnimatedSprite _fightButton;
    private AnimatedSprite _actButton;
    private AnimatedSprite _itemButton;
    private AnimatedSprite _mercyButton;

    // 属性
    public int Selected {
        get {
            return _selected;
        }
        set {
            Select(value);
        }
    }
    private int _selected = 0;
    public int LastSelected {
        get {
            return _lastSelected;
        }
    }
    private int _lastSelected = 0;

    // 选择按钮方法
    public void Select(int selected) {
        _selected = selected;
        switch (selected) {
            case 0: {
                _fightButton.Frame = 1;
                _actButton.Frame = 0;
                _itemButton.Frame = 0;
                _mercyButton.Frame = 0;
                _lastSelected = selected;
                break;
            }
            case 1: {
                _fightButton.Frame = 0;
                _actButton.Frame = 1;
                _itemButton.Frame = 0;
                _mercyButton.Frame = 0;
                _lastSelected = selected;
                break;
            }
            case 2: {
                _fightButton.Frame = 0;
                _actButton.Frame = 0;
                _itemButton.Frame = 1;
                _mercyButton.Frame = 0;
                _lastSelected = selected;
                break;
            }
            case 3: {
                _fightButton.Frame = 0;
                _actButton.Frame = 0;
                _itemButton.Frame = 0;
                _mercyButton.Frame = 1;
                _lastSelected = selected;
                break;
            }
            default: {
                _fightButton.Frame = 0;
                _actButton.Frame = 0;
                _itemButton.Frame = 0;
                _mercyButton.Frame = 0;
                break;
            }
        }
    }
}
