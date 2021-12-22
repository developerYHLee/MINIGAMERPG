Shader "Custom/CompassShader"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
            LOD 200

            cull off
            CGPROGRAM
            #pragma surface surf Lambert noambient alpha:fade

            sampler2D _MainTex;
            float _Compass;

            struct Input
            {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutput o)
            {
                _Compass += 202;
                _Compass /= 360;
                fixed4 c = tex2D(_MainTex, float2(IN.uv_MainTex.x + _Compass, IN.uv_MainTex.y));
                o.Emission = c.rgb;
                o.Alpha = c.a;
            }

            ENDCG
        }
}
