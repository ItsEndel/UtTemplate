using System.Text.RegularExpressions;

public static class RegExp {
    // r = Group1
    // g = Group2
    // b = Group3
    // a = Group4
    public static Regex ColorParam = new Regex("\\( *([\\d\\.]+) *, *([\\d\\.]+) *, *([\\d\\.]+) *, *([\\d\\.]+) *\\)");

    // x = Group1
    // y = Group2
    public static Regex Vector2Param = new Regex("\\( *(\\-?[\\d\\.]+) *, *(\\-?[\\d\\.]+) *\\)");

    // frequency = Group2
    // amplitude = Group4
    // max_amplitude = Group6
    public static Regex ShakeEffectParam = new Regex("\\( *(frequency *= *([\\d\\.]+))? *,? *(amplitude *= *([\\d\\.]+))? *,? *(max_amplitude *= *([\\d\\.]+))? *\\)");
}
