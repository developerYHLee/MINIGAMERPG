Shader "Custom/GroundShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump"{}
        _NoiseTex("Noise Texture", 2D) = "white" {}
        _Glossiness("Rain", Range(0,3)) = 0.5
        _Snow("Snow", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags{"RenderType"="Opaque"}
        //Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
        LOD 200

        //zwrite off
        CGPROGRAM
        #pragma surface surf Standard// alpha:fade
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _NoiseTex;
        half _Glossiness;
        half _Snow;
        float _Wind, _Rain;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float2 uv_NoiseTex;
            float4 color:COLOR;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            float3 d = tex2D(_NoiseTex, 
                float2(IN.uv_NoiseTex.x + _Time.y * _Glossiness / 5 * sin(_Wind) * _Rain, 
                    IN.uv_NoiseTex.y + _Time.y * _Glossiness / 5 * cos(_Wind) * _Rain));
            half4 n = tex2D(_BumpMap, IN.uv_BumpMap);
            o.Normal = UnpackNormal(normalize(pow(n, 2)));
            o.Smoothness = (max(0, _Glossiness)) + abs(sin(_Time.y * _Glossiness) * d.r + 0.1);
            o.Albedo = c.rgb + _Snow;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
    //Fallback "Legacy Shaders/Transparent/VertexLit"
}
