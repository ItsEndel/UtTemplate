shader_type canvas_item;

uniform float level: hint_range(0, 1) = 0.001;

void fragment() {
	if (COLOR.a > 0f) {
		COLOR.a = step(level, texture(TEXTURE, UV).a);
	}
}
