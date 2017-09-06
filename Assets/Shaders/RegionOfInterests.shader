Shader "Custom/RegionOfInterests" {
	Properties {
		_NowTex ("NowTexture", 2D) = "black" {}
		_PastTex ("PastTexture", 2D) = "black" {}
		_Threshold ("Threshold", Range(0,1)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _NowTex;
		sampler2D _PastTex;

		float _Threshold;

		struct Input {
			float2 uv_NowTex;
		};

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 nowColor = tex2D (_NowTex, IN.uv_NowTex);
			fixed4 pastColor = tex2D (_PastTex, IN.uv_NowTex);
			fixed4 color = fixed4(0.0, 0.0, 0.0, 1.0);

			float r = nowColor.r - pastColor.r;
            float g = nowColor.g - pastColor.g;
            float b = nowColor.b - pastColor.b;
			float dist = sqrt(r * r + g * g + b + b);

			if (dist > _Threshold)
            {
				color.rgb = float3(1.0, 1.0, 1.0);
            }
			
			o.Albedo = color.rgb;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
