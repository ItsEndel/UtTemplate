using Godot;
using System;

public class Sound : AudioStreamPlayer2D
{
    // 构造器
    public Sound(AudioStream audio) {
        this.Stream = audio;
        this.Position = new Vector2(320, 240);
    }
    public Sound(AudioStream audio, Vector2 pos) {
        this.Stream = audio;
        this.Position = pos;
    }

    // GD方法
    public override void _Ready()
    {
        // 连接信号
        Connect("finished", this, "Finished");
        // 播放音频
        Play();
    }

    // 信号方法
    public void Finished() {
        QueueFree();
    }
}
