public class ShakingEffect: CharEffect {
    // 构造器
    public ShakingEffect(Char effectingChar, float frequency = 0.1f, float amplitude = 5f) {
        this.EffectingChar = effectingChar;

        this._frequency = frequency;
        this._amplitude = amplitude;
    }

    // 属性
    private float _frequency; // 频率
    private float _amplitude; // 振幅
    private float _timer = 0;

    // 更新
    public override void Update()
    {
        
    }
}
