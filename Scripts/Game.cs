using Godot;

public static class Game {
    // 主场景节点
    public static Main MainScene;

    // 获取字体
    public static DynamicFont GetFont(string fontName) {
        DynamicFont font = new DynamicFont();
        DynamicFontData fontData = GD.Load<DynamicFontData>("res://Assets/Fonts/" + fontName);
        font.FontData = fontData;

        return font;
    }

    // 获取音频
    public static AudioStream GetAudio(string audioName) {
        return GD.Load<AudioStream>("res://Assets/Audios/" + audioName);
    }

    // 是半角字符
    public static bool IsHalf(char chr) {
        return (chr > 31 && chr < 127);
    }
}
