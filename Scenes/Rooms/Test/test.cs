using Godot;
using System;

public class test : Node2D
{
    private Vector2[] _points = new Vector2[] {
        new Vector2(100, 250), new Vector2(400, 250), new Vector2(160, 450), new Vector2(250, 125), new Vector2(340, 450), new Vector2(100, 250)
    };

    public override void _Ready()
    {
        // 测试
        Update();
        
        // 测试
        string text = "[wait=5]";
        text = text + "测 试 测 试 TesT tESt";
        text = text + "\n换行测试";
        text = text + "\n延迟[wait=1]测试";
        text = text + "\n[size=24][line_space=27][full_char_space=27][half_char_space=15][half_offset=(0, -1)]尺寸测试 TEST test";
        text = text + "\n[size=36][line_space=40][full_char_space=40][half_char_space=22][half_offset=(0, 0)]尺寸测试 TEST test";
        text = text + "\n[color=(1, 0, 0, 1)]C[color=(0, 1, 0, 1)]O[color=(0, 1, 0, 1)]L[color=(1, 1, 0, 1)]O[color=(0, 1, 1, 1)]R[color=(1, 1, 1, 1)]";
        text = text + "\n[outline_size=2][outline_color=(1, 0, 0, 1)]OUTLINE TEST 轮廓线测试[outline_size=0]";
        text = text + "\n[shake=()]default shake[/shake=]";
        text = text + "\n[shake=(frequency=1000)]faster shake[/shake=]";
        text = text + "\n[shake=(amplitude=5,max_amplitude=25)]bigger shake[/shake=]";
        text = text + "\n[position=(200, 200)]POSITION TEST\n";
        text = text + "\n!THIS IS SKIP TEST\\!!";
        text = text + "\n[theme=sans_dialog]预设测试 theme test[half_char_space=15]";
        text = text + "\n[voice=Voice/voc_flowey.wav]";
        for (int i = 0; i < 100; i ++) {
            Color color = Color.FromHsv(i / 100f, 1, 1);
            text = text + "[color=(" + color.r + "," + color.g + "," + color.b + "," + color.a + ")]O";
        }
        TextDisplayer textDisplayer = new TextDisplayer(text);
        AddChild(textDisplayer);
    }

    public override void _Draw()
    {
        for (int i = 0; i + 1 < _points.Length; i++)
        {
            DrawLine(_points[i], _points[i + 1], new Color(0, 0, 1));
        }

        for (float i = 0; i + 0.01f < 1; i += 0.01f) {
            DrawLine(BezierCurve.Get(_points, i), BezierCurve.Get(_points, i + 0.01f), new Color(1, 0, 0));
        }
    }
}
