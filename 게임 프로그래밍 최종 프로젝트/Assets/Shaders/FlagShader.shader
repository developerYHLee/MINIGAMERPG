Shader "Custom/FlagShader"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _WindPower("Wind Power", Range(1, 20)) = 1
        _Degree("Rotation", Range(0, 360)) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
            LOD 200

            zwrite off
            ColorMask 0
            CGPROGRAM
            #pragma surface surf nolight noambient noforwardadd nolightmap novertexlights noshadow 
            struct Input {
               float4 color:COLOR;
            };
            void surf(Input IN, inout SurfaceOutput o) {
            }
            float4 Lightingnolight(SurfaceOutput s, float3 lightDir, float atten) {
                return float4(0,0,0,0);
            }
            ENDCG

            cull off
            CGPROGRAM
            #pragma surface surf Lambert vertex:vert alpha:fade

            sampler2D _MainTex;
            float _WindPower, _Degree;

            float2 rotateUV(float2 uv, float degrees) {
                const float Deg2Rad = (UNITY_PI * 2.0) / 360.0; //1도의 라디안 값을 구한다 
                float rotationRadians = degrees * Deg2Rad; //원하는 각도의 라디안 값을 구한다 
                float u = cos(rotationRadians);
                float v = sin(rotationRadians);
                float2x2 rotationMatrix = float2x2(v, -u, u, v);//회전 2차원 행렬을 만든다 
                uv = mul(rotationMatrix, uv);
                return uv;
            }

            void vert(inout appdata_full v) {
                //깃발 모양
                float shape = max(10 - _WindPower, 0);
                v.vertex.x -= v.color.b * shape * distance(v.texcoord.x, 1) * 1.5;
                v.vertex.z += shape * 1.5 * distance(v.texcoord.x, 1);
                v.vertex.y -= v.color.b * distance(v.texcoord.x, 1);

                //깃발 방향
                float r = _WindPower < 13 ? (UNITY_PI * 2.0) / 360.0 * _WindPower * 7 : UNITY_PI / 2; //반지름
                float2 rotate = rotateUV(float2(v.vertex.x + 5.2, v.vertex.y) * sin(r), _Degree);
                v.vertex.x = rotate.x;
                v.vertex.y = rotate.y;

                //흩날리는 정도
                v.vertex.x -= sin((_Time.z + distance(v.texcoord.x, 0)) * _WindPower) * pow(v.color.r, _WindPower / 10) * 0.5;
                v.vertex.y += sin((_Time.z + distance(v.texcoord.x, 0)) * _WindPower) * pow(v.color.r, _WindPower / 10) * 0.5;
                v.vertex.z -= sin((_Time.z + distance(v.texcoord.x, 0)) * _WindPower) * pow(v.color.r, _WindPower / 10) * 0.5;
            }

            struct Input
            {
                float2 uv_MainTex;
                float4 color:COLOR;
            };

            void surf(Input IN, inout SurfaceOutput o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
                //o.Emission = IN.color.r;
                o.Alpha = c.a;
            }
            ENDCG
        }
            Fallback "Legacy Shaders/Transparent/VertexLit"
}