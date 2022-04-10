using Godot;
using System;
using System.Collections.Generic;

public class TextDisplayer : Node2D
{
    // 构造器
    public TextDisplayer(string text = "", float interval = 0.2f, int textSize = 16, float fullTextSpace = 18, float halfTextSpace = 10, float lineSpace = 8, string fullFont = "Menu Chinese.TTF", string halfFont = "Menu.otf")
    {
        this._text = text;
        this._interval = interval;
        this._textSize = textSize;
        this._lineSpace = lineSpace;
        this._fullTextSpace = fullTextSpace;
        this._halfTextSpace = halfTextSpace;
        this._fullFont = Game.GetFont(fullFont);
        this._halfFont = Game.GetFont(halfFont);

        _timer.WaitTime = _interval;
        _timer.Start();
    }

    // 主节点
    private Timer _timer;
    // 属性
    public bool Paused
    {
        get
        {
            return _paused;
        }
        set
        {
            if (_paused != value)
            {
                if (value)
                {
                    _timer.WaitTime = _timer.TimeLeft;
                    _timer.Stop();
                }
                else
                {
                    _timer.Start();
                }
            }
            _paused = Paused;
        }
    }
    private bool _paused = false;
    public bool Finished
    {
        get
        {
            return _finished;
        }
    }
    private bool _finished = false;
    //// 打印属性
    private List<Char> _printedChars = new List<Char>(); // 已打印的字符
    private string _text; // 将显示的文本
    private int _printingIndex = 0; // 下个将被打印的字符的索引
    private float _interval; // 打印时间间隔
    private Color _textColor; // 字符颜色
    private int _outlineSize; // 轮廓线厚度
    private Color _outlineColor; // 轮廓线颜色
    private int _textSize; // 字符尺寸
    private Vector2 _printingPos = new Vector2(0, 0); // 打印位置
    private float _lineSpace; // 行间距
    private float _fullTextSpace; // 全角字间距
    private float _halfTextSpace; // 半角字间距
    private DynamicFont _fullFont; // 全角字体
    private DynamicFont _halfFont; // 半角字体
    private List<CharEffect.Builder> _effects = new List<CharEffect.Builder>(); // 字符效果

    //// 特殊文本格式
    private bool _escape = false; // 转义（\）
    private bool _inBracket = false; // 读取参数名（[）
    private bool _afterEqual = false; // 读取参数值（=）
    private bool  _skipping = false; // 跳过打印间隔（!）
    private string _paramName = ""; // 参数名
    private string _paramValue = ""; // 参数值


    // GD方法
    public override void _Ready()
    {
        // 获取主节点
        _timer = GetNode<Timer>("Timer");

        // 连接计时器超时
        _timer.Connect("timeout", this, "Timeout");

        // 开始计时器
        _timer.WaitTime = _interval;
        _timer.Start();
    }

    // 计时器超时
    public void Timeout()
    {
        Next();
    }

    // 检查下一个字符
    private void Next()
    {
        if (!_finished)
        {
            char chr = _text[_printingIndex];
            _printingIndex += 1;
            if (_printingIndex + 1 >= _text.Length)
            {
                _finished = true;
            }
            if (_inBracket)
            {
                if (!_afterEqual)
                {
                    switch (chr)
                    {
                        case '=':
                            {
                                _afterEqual = true;
                                Next();
                                break;
                            }
                        default:
                            {
                                _paramName = _paramName + chr;
                                Next();
                                break;
                            }
                    }
                }
                else
                {
                    switch (chr)
                    {
                        case ']':
                            {
                                Param(_paramName, _paramValue);
                                Next();
                                break;
                            }
                        default:
                            {
                                _paramValue = _paramValue + chr;
                                Next();
                                break;
                            }
                    }
                }
            }
            else if (_escape)
            {
                Print(NewChar(chr));
                _escape = false;
            }
            else
            {
                switch (chr)
                {
                    case '\\': // 转义
                        {
                            _escape = true;
                            Next();
                            break;
                        }
                    case '[': // 参数
                        {
                            _inBracket = true;
                            _paramName = "";
                            _paramValue = "";
                            Next();
                            break;
                        }
                    case '!': // 跳过
                        {
                            _skipping = !_skipping;
                            Next();
                            break;
                        }
                    default:
                        {
                            Print(NewChar(chr));
                            if (chr == '\n' || _skipping || _interval <= 0)
                            {
                                Next();
                            }
                            break;
                        }
                }
            }
        }

    }

    // 应用参数
    private void Param(string name, string value)
    {
        switch (name)
        {
            case "interval":
                {
                    _interval = (float)Convert.ToDouble(value);
                    _timer.WaitTime = _interval;
                    _timer.Start();
                    break;
                }
            case "shake":
                {
                    ShakingEffect.Builder effect = new ShakingEffect.Builder();
                    _effects.Add(effect);
                    break;
                }
            case "/shake":
                {
                    foreach (CharEffect.Builder i in _effects)
                    {
                        if (i is ShakingEffect)
                        {
                            _effects.Remove(i);
                            break;
                        }
                    }
                    break;
                }
            default:
                {
                    throw new InvalidCastException("Text param [" + name + "=" + value + "] not defined");
                }
        }
    }

    // 打印字符
    private Char NewChar(char chr) {
        bool isHalf = Game.IsHalf(chr);

        Char newChar = new Char(chr);

        newChar.AddColorOverride("font_color", _textColor);
        DynamicFont font = (DynamicFont)(Game.IsHalf(chr) ? _halfFont : _fullFont);
        font.Size = _textSize;
        font.OutlineSize = _outlineSize;
        font.OutlineColor = _outlineColor;
        newChar.AddFontOverride("font", font);

        return newChar;
    }
    private void Print(Char chr)
    {
        _printedChars.Add(chr);

        _printingPos.x += Game.IsHalf(chr._Char) ? _halfTextSpace : _fullTextSpace;
        chr.RectPosition = _printingPos;

        AddChild(chr);
    }
}
