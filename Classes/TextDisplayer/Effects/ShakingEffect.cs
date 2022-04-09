using Godot;

public class ShakingEffect : CharEffect
{
    public new class Builder : CharEffect.Builder
    {
        public Builder(float frequency = 10f, float amplitude = 0f, float maxAmplitude = 5f) {
            this._frequency = frequency;
            this._amplitude = amplitude;
            this._maxAmplitude = maxAmplitude;
        }
        // 属性
        private float _frequency = 10f; // 频率
        private float _amplitude = 0f; // 振幅
        private float _maxAmplitude = 5f; // 最大振幅
        public CharEffect Build(Char effectingChar)
        {
            return new ShakingEffect(effectingChar, _frequency, _amplitude, _maxAmplitude);
        }
    }
    // 构造器
    public ShakingEffect(Char effectingChar, float frequency = 0.1f, float amplitude = 0f, float maxAmplitude = 5f)
    {
        this.EffectingChar = effectingChar;

        this._startPos = effectingChar.RectPosition;
        this._frequency = frequency;
        this._amplitude = amplitude;
    }

    // 属性
    private Vector2 _startPos;
    private float _frequency = 10f; // 频率
    private float _amplitude = 0f; // 振幅
    private float _maxAmplitude = 5f; // 最大振幅
    private float _timer = 0f;

    // GD方法
    public override void _Process(float delta)
    {
        float time = 1 / _frequency;

        _timer += delta;
        if (_timer >= time)
        {
            Shake();
            _timer -= time;
        }
    }

    // 效果方法
    private void Shake()
    {
        float[] randoms = new float[2] {
            (GD.Randf() - 0.5f) * 2,
            (GD.Randf() - 0.5f) * 2
            }; // 获取两个随机数
        EffectingChar.RectPosition = _startPos + new Vector2(
            randoms[0] * (_maxAmplitude - _amplitude) + _amplitude,
            randoms[1] * (_maxAmplitude - _amplitude) + _amplitude
            ); // 改变位置
    }
}
