using Godot;
using System;

public class Sound : AudioStreamPlayer
{
    // 构造器
    public Sound(AudioStream audio) {
        this.Stream = audio;
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
