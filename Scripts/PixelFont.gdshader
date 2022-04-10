shader_type canvas_item;

uniform float level: hint_range(0, 1) = 0.4;

void fragment() {
	if (level > 0f && level < 1f) {
		COLOR.a = step(level, texture(TEXTURE, UV).a);
	} else {
		COLOR = texture(TEXTURE, UV);
	}
}
