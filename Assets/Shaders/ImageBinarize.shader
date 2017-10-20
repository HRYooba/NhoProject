Shader "Custom/ImageBinarize" {
	Properties {
		_MainTex ("NowTexture", 2D) = "black" {}
		_Threshold ("Threshold", Range(0,1)) = 0.7
	}
	
	SubShader {
		Pass{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _Threshold;

			fixed4 frag(v2f_img i) : SV_Target{
				fixed4 mainTex = tex2D(_MainTex, i.uv);
				fixed4 color = fixed4(0.0, 0.0, 0.0, 1.0);

				float r = mainTex.r;
				if (r > _Threshold) {
					color = fixed4(1.0, 1.0, 1.0, 1.0);
				}

				return color;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
