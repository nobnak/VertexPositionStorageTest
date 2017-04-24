Shader "VertexBlend/Unlit" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
        _PositionBlend ("Position Blend", Range(0,1)) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }

		Pass {
			CGPROGRAM
            #pragma target 5.0
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata {
                uint vid : SV_VertexID;
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
            float _PositionBlend;

            #ifdef SHADER_API_D3D11
            StructuredBuffer<float3> _VertexPositions;
            #endif
			
			v2f vert (appdata v) {
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
                #ifdef SHADER_API_D3D11
                float3 referencePos = _VertexPositions[v.vid];
                worldPos = lerp(worldPos, referencePos, _PositionBlend);
                #endif

				v2f o;
				o.vertex = mul(UNITY_MATRIX_VP, float4(worldPos, 1));
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target {
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}
