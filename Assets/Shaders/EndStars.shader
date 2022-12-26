Shader "Unlit/EndStars"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 view : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Zoom;
            float _Speed;

#define iterations 15
#define formuparam 0.53

#define volsteps 6
#define stepsize 0.1

#define zoom   10.100
#define tile   0.850
#define speed  0.01

#define brightness 0.0015
#define darkmatter 0.300
#define distfading 0.730
#define saturation 0.850

            //https://www.shadertoy.com/view/XlfGRj
            float4 stars(float2 uv) {
  
               
                //uv.y *= iResolution.y / iResolution.x;
                float3 dir = float3(uv * _Zoom, 1.);
                float time = _Time.y * speed + .25;


                //volumetric rendering
                float s = 0.1, fade = 1.;
                float3 v = float3(0., 0., 0.);
                for (int r = 0; r < volsteps; r++) {
                    float3 p = s *dir * .5;
                    p = abs(float3(tile, tile, tile) - fmod(p, float3(tile * 2., tile * 2., tile * 2.))); // tiling fold
                    float pa, a = pa = 0.;
                    for (int i = 0; i < iterations; i++) {
                        p = abs(p) / dot(p, p) - formuparam; // the magic formula
                        a += abs(length(p) - pa); // absolute sum of average change
                        pa = length(p);
                    }
                    float dm = max(0., darkmatter - a * a * .001); //dark matter
                    a *= a * a; // add contrast
                    if (r > 6) fade *= 1. - dm; // dark matter, don't render near
                    //v+=float3(dm,dm*.5,0.);
                    v += fade;
                    v += float3(s, s * s, s * s * s * s) * a * brightness * fade; // coloring based on distance
                    fade *= distfading; // distance fading
                    s += stepsize;
                }
                float lengthV = length(v);
                v = lerp(float3(lengthV, lengthV, lengthV), v, saturation); //color adjust
                return float4(v * .01, 1.);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.view = UnityObjectToViewPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = stars(i.view.xy);
                
                return col;
            }
            ENDHLSL
        }
    }
}
