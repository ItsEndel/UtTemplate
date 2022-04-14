using Godot;

public static class Game {
    // 主场景节点
    public static Main MainScene;

    // 玩家存档
    public static PlayerSave Player;

    // 资源方法
    //// 获取字体资源
    public static DynamicFont GetFont(string fontName) {
        DynamicFont font = new DynamicFont();
        DynamicFontData fontData = GD.Load<DynamicFontData>("res://Assets/Fonts/" + fontName);
        fontData.Antialiased = false;
        font.FontData = fontData;

        return font;
    }
    //// 获取音频资源
    public static AudioStream GetAudio(string audioName) {
        return GD.Load<AudioStream>("res://Assets/Audios/" + audioName);
    }

    // 是半角字符
    public static bool IsHalf(char chr) {
        return (chr > 31 && chr < 127);
    }
}
