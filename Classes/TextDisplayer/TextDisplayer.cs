using Godot;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TextDisplayer : Node2D
{
    // 构造器
    public TextDisplayer(string text = "")
    {
        this._text = text;
        this._fullFont = Game.GetFont("Menu Chinese.TTF");
        this._halfFont = Game.GetFont("Menu.otf");
        this._voice = Game.GetAudio("Voice/voc_chara.wav");
    }
    public TextDisplayer(string text = "", float interval = 0.1f, int textSize = 16, float fullCharSpace = 18f, float halfCharSpace = 10f, float lineSpace = 18f, string fullFont = "Menu Chinese.TTF", string halfFont = "Menu.otf", string voice = "Voice/voc_chara.wav")
    {
        this._text = text;
        this._interval = interval;
        this._textSize = textSize;
        this._lineSpace = lineSpace;
        this._fullCharSpace = fullCharSpace;
        this._halfCharSpace = halfCharSpace;
        this._fullFont = Game.GetFont(fullFont);
        this._halfFont = Game.GetFont(halfFont);
        this._voice = Game.GetAudio(voice);

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
    private float _interval = 0.1f; // 打印时间间隔
    private Color _textColor = new Color(1, 1, 1); // 字符颜色
    private int _outlineSize = 0; // 轮廓线厚度
    private Color _outlineColor = new Color(0, 0, 0); // 轮廓线颜色
    private int _textSize = 16; // 字符尺寸
    private Vector2 _printingPos = new Vector2(0, 0); // 打印位置
    private Vector2 _halfOffset = new Vector2(0, -1); // 半角字符位置补正
    private float _lineSpace = 18f; // 行间距
    private float _fullCharSpace = 18f; // 全角字间距
    private float _halfCharSpace = 10f; // 半角字间距
    private DynamicFont _fullFont; // 全角字体
    private DynamicFont _halfFont; // 半角字体
    private List<CharEffect.Builder> _effects = new List<CharEffect.Builder>(); // 字符效果
    private AudioStream _voice; // 打印音效

    //// 特殊文本格式
    private bool _escape = false; // 转义（\）
    private bool _inBracket = false; // 读取参数名（[）
    private bool _afterEqual = false; // 读取参数值（=）
    private bool _skipping = false; // 跳过打印间隔（!）
    private string _paramName = ""; // 参数名
    private string _paramValue = ""; // 参数值


    // GD方法
    public override void _Ready()
    {
        // 获取主节点
        _timer = new Timer();
        AddChild(_timer);

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
        _timer.WaitTime = _interval;
    }

    // 检查下一个字符
    private void Next()
    {
        if (!_finished)
        {
            char chr = _text[_printingIndex];
            _printingIndex += 1;
            if (_printingIndex >= _text.Length)
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
                                _inBracket = false;
                                _afterEqual = false;
                                Param(_paramName, _paramValue);
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
                    case '\n': // 换行
                        {
                            _printingPos.x = 0;
                            _printingPos.y += _lineSpace;
                            Next();
                            break;
                        }
                    default:
                        {
                            Print(NewChar(chr));
                            if (_skipping || _interval <= 0)
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
        GD.Print(name + " " + value);
        switch (name)
        {
            case "wait":
                {
                    _timer.WaitTime = (float)Convert.ToDouble(value);
                    _timer.Start();
                    break;
                }
            case "interval":
                {
                    _interval = (float)Convert.ToDouble(value);
                    _timer.WaitTime = _interval;
                    _timer.Start();
                    break;
                }
            case "color":
                {
                    Match match = RegExp.ColorParam.Match(value);
                    float r = (float)Convert.ToDouble(match.Groups[1].Value);
                    float g = (float)Convert.ToDouble(match.Groups[2].Value);
                    float b = (float)Convert.ToDouble(match.Groups[3].Value);
                    float a = (float)Convert.ToDouble(match.Groups[4].Value);

                    _textColor = new Color(r, g, b, a);

                    Next();
                    break;
                }
            case "outline_size":
                {
                    _outlineSize = Convert.ToInt32(value);

                    Next();
                    break;
                }
            case "outline_color":
                {
                    Match match = RegExp.ColorParam.Match(value);
                    float r = (float)Convert.ToDouble(match.Groups[1].Value);
                    float g = (float)Convert.ToDouble(match.Groups[2].Value);
                    float b = (float)Convert.ToDouble(match.Groups[3].Value);
                    float a = (float)Convert.ToDouble(match.Groups[4].Value);

                    _outlineColor = new Color(r, g, b, a);

                    Next();
                    break;
                }
            case "size":
                {
                    _textSize = Convert.ToInt32(value);

                    Next();
                    break;
                }
            case "position":
                {
                    Match match = RegExp.Vector2Param.Match(value);
                    float x = (float)Convert.ToDouble(match.Groups[1].Value);
                    float y = (float)Convert.ToDouble(match.Groups[2].Value);

                    _printingPos = new Vector2(x, y);

                    Next();
                    break;
                }
            case "half_offset":
                {
                    Match match = RegExp.Vector2Param.Match(value);
                    float x = (float)Convert.ToDouble(match.Groups[1].Value);
                    float y = (float)Convert.ToDouble(match.Groups[2].Value);

                    _halfOffset = new Vector2(x, y);

                    Next();
                    break;
                }
            case "line_space":
                {
                    _lineSpace = (float)Convert.ToDouble(value);

                    Next();
                    break;
                }
            case "full_char_space":
                {
                    _fullCharSpace = (float)Convert.ToDouble(value);

                    Next();
                    break;
                }
            case "half_char_space":
                {
                    _halfCharSpace = (float)Convert.ToDouble(value);

                    Next();
                    break;
                }
            case "full_font":
                {
                    _fullFont = Game.GetFont(value);

                    Next();
                    break;
                }
            case "half_font":
                {
                    _halfFont = Game.GetFont(value);

                    Next();
                    break;
                }
            case "voice":
                {
                    _voice = Game.GetAudio(value);

                    Next();
                    break;
                }
            case "shake":
                {
                    Match match = RegExp.ShakeEffectParam.Match(value);
                    float frequency = 15f;
                    float amplitude = 0f;
                    float maxAmplitude = 5f;
                    if (match.Groups[2].Value != "")
                    {
                        frequency = (float)Convert.ToDouble(match.Groups[2].Value);
                    }
                    if (match.Groups[4].Value != "")
                    {
                        amplitude = (float)Convert.ToDouble(match.Groups[4].Value);
                    }
                    if (match.Groups[6].Value != "")
                    {
                        maxAmplitude = (float)Convert.ToDouble(match.Groups[6].Value);
                    }

                    ShakingEffect.Builder effect = new ShakingEffect.Builder(frequency, amplitude, maxAmplitude);
                    _effects.Add(effect);

                    Next();
                    break;
                }
            case "/shake":
                {
                    foreach (CharEffect.Builder i in _effects)
                    {
                        if (i is ShakingEffect.Builder)
                        {
                            _effects.Remove(i);

                            Next();
                            break;
                        }
                    }
                    Next();
                    break;
                }
            default:
                {
                    throw new InvalidCastException("Text param [" + name + "=" + value + "] not defined");
                }
        }
    }

    // 打印字符
    private Char NewChar(char chr)
    {
        bool isHalf = Game.IsHalf(chr);

        Char newChar = new Char(chr);

        newChar.AddColorOverride("font_color", _textColor);
        DynamicFont font = (DynamicFont)(Game.IsHalf(chr) ? _halfFont.Duplicate() : _fullFont.Duplicate());
        font.Size = _textSize;
        font.OutlineSize = _outlineSize;
        font.OutlineColor = _outlineColor;
        newChar.AddFontOverride("font", font);

        foreach (CharEffect.Builder i in _effects) {
            newChar.AddChild(i.Build(newChar));
        }

        return newChar;
    }
    private void Print(Char chr)
    {
        _printedChars.Add(chr);

        if (Game.IsHalf(chr._Char))
        {
            chr.RectPosition = _printingPos + _halfOffset;
            _printingPos.x += _halfCharSpace;
        }
        else
        {
            chr.RectPosition = _printingPos;
            _printingPos.x += _fullCharSpace;
        }

        if (_voice != null && chr._Char != ' ')
        {
            Game.MainScene.PlaySound(_voice);
        }

        AddChild(chr);
    }
}
