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
float3 sun_direction = float3(1,2,4);

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
    //float4 wPos : POSITION1;
    float4 Normal : NORMAL0;
    float4 Color : COLOR0;
    float4 TexCoord : TEXCOORD0;
};

PixelInput FloorPlane(VertexInput v) {
    PixelInput output;

    float4 wPos = v.Position;

    output.Position = mul(wPos, view_projection);
    //output.wPos = wPos;
    output.Normal = v.Normal;
    output.Color = v.Color;
    output.TexCoord = v.TexCoord;
    return output;
}


float4 SpritePixelShader(PixelInput p) : COLOR0{
    float4 diffuse = tex2D(TextureSampler, p.TexCoord.xy);
    
    float intensity = max(dot(sun_direction, p.Normal.xyz), 0.1);
    intensity = 1;
    return diffuse * intensity;
}

technique FloorPlane {
    pass {
        VertexShader = compile VS_SHADERMODEL FloorPlane();
        PixelShader = compile PS_SHADERMODEL SpritePixelShader();
    }
}