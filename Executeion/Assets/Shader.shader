Shader "Unlit/Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GlitchStrength ("Glitch Strength", Range(0,1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _GlitchStrength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float glitch = frac(sin(_Time.y * 50)* 10000.0);
                float2 uvOffset = float2(0,(glitch - 0.5) * _GlitchStrength);

                fixed4 col = text2D(_Maintex, i.uv + uvOffset);

                if (glitch > 0.95) {
                    col.rgb = float3(1,0,0);
                }
                return col;
            }
            ENDCG
        }
    }
}
