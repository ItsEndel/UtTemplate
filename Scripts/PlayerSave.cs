public class PlayerSave {
    // 构造器
    public PlayerSave(string name) {
        this.Name = name;
    }

    // 玩家名称
    public string Name;

    // 玩家等级数据
    public int Level = 1;
    public int Exp = 0;

    // 玩家血量数据
    public int MaxHp = 20;
    public int Hp = 20;
    public int ExtraHp = 0;
}
