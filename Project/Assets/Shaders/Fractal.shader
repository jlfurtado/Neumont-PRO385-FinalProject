// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Fractal"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_myRVX("RVX", Float) = 5.375
		_myRVY("RVY", Float) = 5.4
		_myRPS("Repeat Scale", Float) = 1.0
		_myNumIts("Num Iterations", Int) = 100

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
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 fPos : POSITION1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _myRVX;
			float _myRVY;
			float _myRPS;
			int _myNumIts;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				o.fPos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
				float rpsOverTwo = _myRPS / 2.0;
				int xI = int(floor(i.uv.x / rpsOverTwo));
				int zI = int(floor(i.uv.y / rpsOverTwo));

				float2 z = (fmod(i.uv, _myRPS) - float2(rpsOverTwo, rpsOverTwo));

				if (fmod(floor(xI + zI), 2) == 0.0) { z = mul(float2x2(-1.0, 0.0, 0.0, 1.0), z); } // rotate 180

				int j;
				float f2;
				for (j = 0; j<_myNumIts; j++) {
					float x = (z.x * z.x - z.y * z.y) + _myRVX;
					float y = (z.y * z.x + z.x * z.y) + _myRVY;

					f2 = x * x + y * y;
					if (f2 > 4.0) { break; }

					z.x = x;
					z.y = y;
				}

				if (j == _myNumIts) { discard; }

				float f = (j + f2) / 100.0f;
				float4 clr = tex2D(_MainTex, float2(f, 0.5));
				return clr;
			}
			ENDCG
		}
	}
}
