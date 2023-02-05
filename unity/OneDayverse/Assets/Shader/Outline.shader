Shader "Custom/Outline"
{
	Properties // Variables
	{
		_MainTex("Main Texture (RBG)", 2D) = "white" {} // Allows for a texture property
		_Color("Color", Color) = (30,128,99,255) // Allows for a color property

		_OutlineTex("Outline Texture", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (30,128,99,255)
		_OutlineWidth("Outline Width", Range(1.0,10.0)) = 1.1
	}

		SubShader
		{
			Tags
			{
				"Queue" = "transparent"
			}

			Pass
			{
				Name "OUTLINE"

				ZWrite Off

				CGPROGRAM // Allows talk between two languages : shader lab and nvidia C for graphics

				#pragma vertex vert // Define for the building function
				#pragma fragment frag // Define for coloring function

				#include "UnityCG.cginc" // Built in shader functions

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				float _OutlineWidth;
				float4 _OutlineColor;
				sampler2D _OutlineTex;

				v2f vert(appdata IN)
				{
					IN.vertex.xyz *= _OutlineWidth;
					v2f OUT;

					OUT.pos = UnityObjectToClipPos(IN.vertex);
					OUT.uv = IN.uv;

					return OUT;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					float4 texColor = tex2D(_OutlineTex, IN.uv);

					return texColor * _OutlineColor;
				}

				ENDCG
			}

			Pass
			{
				Name "OBJECT"

				CGPROGRAM // Allows talk between two languages : shader lab and nvidia C for graphics

				#pragma vertex vert // Define for the building function
				#pragma fragment frag // Define for coloring function

				#include "UnityCG.cginc" // Built in shader functions

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				float4 _Color;
				sampler2D _MainTex;

				v2f vert(appdata IN)
				{
					v2f OUT;

					OUT.pos = UnityObjectToClipPos(IN.vertex);
					OUT.uv = IN.uv;

					return OUT;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					float4 texColor = tex2D(_MainTex, IN.uv);

					return texColor * _Color;
				}

				ENDCG
			}
		}
}