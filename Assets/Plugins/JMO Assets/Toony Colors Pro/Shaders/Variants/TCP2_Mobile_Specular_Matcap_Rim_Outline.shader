
// Toony Colors Pro+Mobile 2
// (c) 2014-2019 Jean Moreno


Shader "Hidden/Toony Colors Pro 2/Variants/Mobile Specular Matcap Rim Outline"
{
	Properties
	{
		//TOONY COLORS
		_Color ("Color", Color) = (1,1,1,1)
		_HColor ("Highlight Color", Color) = (0.785,0.785,0.785,1.0)
		_SColor ("Shadow Color", Color) = (0.195,0.195,0.195,1.0)
		
		//DIFFUSE
		_MainTex ("Main Texture (RGB) Spec/MatCap Mask (A) ", 2D) = "white" {}
		
		//TOONY COLORS RAMP
		[TCP2Gradient] _Ramp ("#RAMPT# Toon Ramp (RGB)", 2D) = "gray" {}
		_RampThreshold ("#RAMPF# Ramp Threshold", Range(0,1)) = 0.5
		_RampSmooth ("#RAMPF# Ramp Smoothing", Range(0.01,1)) = 0.1
		
		//BUMP
		_BumpMap ("#NORM# Normal map (RGB)", 2D) = "bump" {}
		
		//SPECULAR
		_SpecColor ("#SPEC# Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("#SPEC# Shininess", Range(0.01,2)) = 0.1
		_SpecSmooth ("#SPECT# Smoothness", Range(0,1)) = 0.05
		
		//RIM LIGHT
		_RimColor ("#RIM# Rim Color", Color) = (0.8,0.8,0.8,0.6)
		_RimMin ("#RIM# Rim Min", Range(0,1)) = 0.5
		_RimMax ("#RIM# Rim Max", Range(0,1)) = 1.0
		
		//RIM DIRECTION
		_RimDir ("#RIMDIR# Rim Direction", Vector) = (0.0,0.0,1.0,0.0)
		
		//MATCAP
		_MatCap ("#MC# MatCap (RGB)", 2D) = "black" {}
		
		//OUTLINE
		_OutlineColor ("#OUTLINE# Outline Color", Color) = (0.2, 0.2, 0.2, 1.0)
		_Outline ("#OUTLINE# Outline Width", Float) = 1
		
		//Outline Textured
		_TexLod ("#OUTLINETEX# Texture LOD", Range(0,10)) = 5
		
		//ZSmooth
		_ZSmooth ("#OUTLINEZ# Z Correction", Range(-3.0,3.0)) = -0.5
		
		//Z Offset
		_Offset1 ("#OUTLINEZ# Z Offset 1", Float) = 0
		_Offset2 ("#OUTLINEZ# Z Offset 2", Float) = 0
		
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		
		#include "../Include/TCP2_Include.cginc"
		
		#pragma surface surf ToonyColorsSpec nodirlightmap vertex:vert noforwardadd halfasview
		#pragma target 2.0
		
		#pragma shader_feature TCP2_DISABLE_WRAPPED_LIGHT
		#pragma shader_feature TCP2_RAMPTEXT
		#pragma shader_feature TCP2_BUMP
		#pragma shader_feature TCP2_SPEC_TOON
		#pragma shader_feature TCP2_RIMDIR
		#pragma shader_feature TCP2_MC TCP2_MCMASK
		
		//================================================================
		// VARIABLES
		
		fixed4 _Color;
		sampler2D _MainTex;
	#if TCP2_MC || TCP2_MCMASK
		sampler2D _MatCap;
	#endif
		
	#if TCP2_BUMP
		sampler2D _BumpMap;
	#endif
		fixed _Shininess;
		fixed4 _RimColor;
		fixed _RimMin;
		fixed _RimMax;
		#if TCP2_RIMDIR
		float4 _RimDir;
		#endif
		
		struct Input
		{
			half2 uv_MainTex : TEXCOORD0;
	#if TCP2_BUMP
			half2 uv_BumpMap : TEXCOORD1;
	#endif
			fixed rim;
	#if TCP2_MC || TCP2_MCMASK
			half2 matcap;
	#endif
		};
		
		//================================================================
		// VERTEX FUNCTION
		
		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
		#if TCP2_RIMDIR
			_RimDir.x += UNITY_MATRIX_MV[0][3] * (1/UNITY_MATRIX_MV[2][3]) * (1-UNITY_MATRIX_P[3][3]);
			_RimDir.y += UNITY_MATRIX_MV[1][3] * (1/UNITY_MATRIX_MV[2][3]) * (1-UNITY_MATRIX_P[3][3]);
			float3 viewDir = normalize(UNITY_MATRIX_V[0].xyz * _RimDir.x + UNITY_MATRIX_V[1].xyz * _RimDir.y + UNITY_MATRIX_V[2].xyz * _RimDir.z);
		#else
			float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
		#endif
			half rim = 1.0f - saturate( dot(viewDir, v.normal) );
			o.rim = smoothstep(_RimMin, _RimMax, rim) * _RimColor.a;
			
	#if TCP2_MC || TCP2_MCMASK
			//MATCAP
			float3 worldNorm = normalize(unity_WorldToObject[0].xyz * v.normal.x + unity_WorldToObject[1].xyz * v.normal.y + unity_WorldToObject[2].xyz * v.normal.z);
			worldNorm = mul((float3x3)UNITY_MATRIX_V, worldNorm);
			o.matcap.xy = worldNorm.xy * 0.5 + 0.5;
	#endif
		}
		
		//================================================================
		// SURFACE FUNCTION
		
		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 main = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = main.rgb * _Color.rgb;
			o.Alpha = main.a * _Color.a;
			
			//Specular
			o.Gloss = main.a;
			o.Specular = _Shininess;
	#if TCP2_BUMP
			//Normal map
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	#endif
			o.Emission += IN.rim * _RimColor.rgb;
	#if TCP2_MC || TCP2_MCMASK
			fixed3 matcap = tex2D(_MatCap, IN.matcap).rgb;
		#if TCP2_MCMASK
			matcap *= main.a * _HColor.a;
		#endif
			o.Emission += matcap;
	#endif
		}
		
		ENDCG
		
		//Outlines
		UsePass "Hidden/Toony Colors Pro 2/Outline Only (Shader Model 2)/OUTLINE"
	}
	
	Fallback "Diffuse"
	CustomEditor "TCP2_MaterialInspector"
}