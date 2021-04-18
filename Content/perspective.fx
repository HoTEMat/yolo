#if OPENGL
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
//#define VS_SHADERMODEL vs_4_0
//#define PS_SHADERMODEL ps_4_0
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float4x4 view_projection;
float4 tone, borderColor;
//float3 sun_direction = float3(1,2,4);
float intensity = 1;

Texture2D SpriteTexture : register(t0);
sampler TextureSampler : register(s0) = sampler_state {
	Texture = <SpriteTexture>;
	Filter = Point;
};

struct VertexInput {
	float4 Position : POSITION0;
	float4 Normal : NORMAL0;
	float4 Color : COLOR0;
	float4 TexCoord : TEXCOORD0;
};
struct PixelInput {
	float4 Position : SV_POSITION;
	float4 Normal : NORMAL0;
	float4 Color : COLOR0;
	float4 TexCoord : TEXCOORD0;
};

PixelInput WorldShader(VertexInput v) {
	PixelInput output;

	float4 wPos = v.Position;

	output.Position = mul(wPos, view_projection);
	//output.wPos = wPos;
	output.Normal = v.Normal;
	output.Color = v.Color;
	output.TexCoord = v.TexCoord;
	return output;
}


float4 TerrainPixel(PixelInput p) : COLOR0{
	float4 diffuse = tex2D(TextureSampler, p.TexCoord.xy);

	float3 normal = normalize(p.Color.rgb * 2 - float3(1,1,1));

	float3 sun_direction = normalize(float3(-1, 2, -4));
	float light = max(0.1, dot(sun_direction, normal));
	diffuse.rgb *= pow(light, 0.2) * 1.1;

	return diffuse * tone;
}

float2 pixelSize;

float4 EntityPixel(PixelInput p) : COLOR0{
	float4 diffuse = tex2D(TextureSampler, p.TexCoord.xy);

	diffuse.rgb *= intensity;
	diffuse *= tone;

	return diffuse;
}

technique Terrain {
	pass {
		VertexShader = compile VS_SHADERMODEL WorldShader();
		PixelShader = compile PS_SHADERMODEL TerrainPixel();
	}
}

technique Entity {
	pass {
		VertexShader = compile VS_SHADERMODEL WorldShader();
		PixelShader = compile PS_SHADERMODEL EntityPixel();
	}
}

