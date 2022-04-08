using Godot;
using System;

public class TextDisplayer : Node2D
{
    // 构造器
    public TextDisplayer() {}
    public TextDisplayer(string text) {
        this._text = text;
    }

    // 主节点
    private Timer _timer;
    // 属性
    public bool Paused {
        get {
            return _paused;
        }
        set {
            if (_paused != value) {
                if (value) {
                    _timer.WaitTime = _timer.TimeLeft;
                    _timer.Stop();
                } else {
                    _timer.Start();
                }
            }
            _paused = Paused;
        }
    }
    private bool _paused = false;

    private string _text; // 将显示的文本

    // GD方法
    public override void _Ready()
    {
        // 获取主节点
        _timer = GetNode<Timer>("Timer");

        // 连接计时器超时
        _timer.Connect("timeout", this, "Timeout");

        // test
        _timer.WaitTime = 5;
        _timer.Start();
    }

    // 计时器超时
    public void Timeout() {
    }
}
