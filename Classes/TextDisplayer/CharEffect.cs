using Godot;

public abstract class CharEffect : Node
{
    // 属性
    protected Char EffectingChar;

    public interface Builder
    {
        CharEffect Build(Char effectingChar);
    }
}
