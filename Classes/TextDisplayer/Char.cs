using Godot;
using System;

public class Char : Label
{
    // 构造器
    public Char(char text) {
        _char = text;
        Text = text.ToString();
    }

    // 属性
    public char _Char {
        get {
            return _char;
        }
    }
    private char _char;
}
