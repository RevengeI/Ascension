Shader "Custom/Blending/Overlay" {

	Properties{
			_Opacity("Opacity", Range(0,1)) = 1.0
			_MainTex("Texture", 2D) = "white" {}
	}

		SubShader{
			Tags {
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
			}

			Blend SrcAlpha OneMinusSrcAlpha

			Cull Off
			ZWrite Off
			Lighting Off
			Fog { Mode Off }

			GrabPass {
				"_sharedGrabTexture"
			}

			Pass {

				CGPROGRAM

				#include "UnityCG.cginc"

				#pragma vertex vert
				#pragma fragment frag

				sampler2D _MainTex;
				sampler2D _sharedGrabTexture;
				float _Opacity;

				struct vertInput {
					float4 vertex : POSITION;
					float4 color : COLOR;
					float4 uv : TEXCOORD0;
				};

				struct vertOutput {
					float4 vertex : SV_POSITION;
					float4 color : COLOR;
					float4 grab_uv: TEXCOORD1;
					float2 uv : TEXCOORD0;
				};

				vertOutput vert(vertInput input) {

					vertOutput output;

					output.vertex = UnityObjectToClipPos(input.vertex);
					output.grab_uv = ComputeGrabScreenPos(output.vertex);
					output.uv = input.uv;
					output.color = input.color;

					return output;
				}

				float4 Overlay(float4 a, float4 b)
				{
					float4 o = 1 - (1-a)*(1-b);
					o.a = b.a;
					return o;
				}

				float4 frag(vertOutput input) : SV_Target {

					float4 texture_color = tex2D(_MainTex, input.uv);

					#if UNITY_UV_STARTS_AT_TOP
						//input.grab_uv.y = 1.0 - input.grab_uv.y;
					#endif

					float4 scene_color = tex2D(_sharedGrabTexture, input.grab_uv);

					float4 output = Overlay(scene_color, texture_color);

					output = lerp(texture_color,output,_Opacity);

					return output;
				}

				ENDCG

			}

	}

		Fallback Off
}

