#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float4x4 view_projection;
sampler TextureSampler : register(s0);

struct VertexInput
{
	float4 Position : POSITION0;
    float4 TexCoord : TEXCOORD0;
	float4 Color : COLOR0;
};

struct PixeInput
{
	float4 Position : SV_Position0;
    float4 TexCoord : TEXCOORD0;
	float4 Color : COLOR0;
};

PixeInput SpriteVertexShader(VertexInput v)
{
    PixeInput output;
	
    output.Position = mul(v.Position, view_projection);
    output.TexCoord = v.TexCoord;
    output.Color = v.Color;
	
    return output;
}

float4 SpritePixelShader(PixeInput p) : SV_Target
{
    float4 diffuse = tex2D(TextureSampler, p.TexCoord.xy);
    return diffuse * p.Color;
}

technique SpriteBatch
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL SpriteVertexShader();
		PixelShader = compile PS_SHADERMODEL SpritePixelShader();
	}
};