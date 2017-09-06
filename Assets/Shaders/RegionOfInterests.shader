Shader "Custom/RegionOfInterests" {
	Properties {
		_MainTex ("NowTexture", 2D) = "black" {}
		_BufferTex ("PastTexture", 2D) = "black" {}
		_Threshold ("Threshold", Range(0,1)) = 0.5
	}
	
	SubShader {
		Pass{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _BufferTex;
			uniform float _Threshold;

			fixed4 frag(v2f_img i) : SV_Target{
				fixed4 nowColor = tex2D(_MainTex, i.uv);
				fixed4 pastColor = tex2D(_BufferTex, i.uv);
				fixed4 color = fixed4(0.0, 0.0, 0.0, 1.0);

				float r = nowColor.r - pastColor.r;
				float g = nowColor.g - pastColor.g;
				float b = nowColor.b - pastColor.b;
				float dist = sqrt(r * r + g * g + b + b);

				if (dist > _Threshold) {
					color.r = 1.0;
				}

				return color;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
