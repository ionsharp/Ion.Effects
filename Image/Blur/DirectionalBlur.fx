/// <class>DirectionalBlurEffect</class>

/// <description>An effect that blurs in a single direction.</description>

sampler2D  Texture1Sampler : register(S0);

/// <summary>The scale of the blur (as a fraction of the input size).</summary>
/// <minValue>0</minValue>
/// <maxValue>0.01</maxValue>
/// <defaultValue>0.003</defaultValue>
float Amount : register(C0);

/// <summary>The direction of the blur (in degrees).</summary>
/// <minValue>0</minValue>
/// <maxValue>360</maxValue>
/// <defaultValue>0</defaultValue>
float Angle : register(C1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 c = 0;
    float rad = Angle * 0.0174533f;
    float xOffset = cos(rad);
    float yOffset = sin(rad);

    for(int i = 0; i < 16; i++)
    {
        uv.x = uv.x - Amount * xOffset;
        uv.y = uv.y - Amount * yOffset;
        c += tex2D(Texture1Sampler, uv);
    }
    c /= 16;
    return c;
}
