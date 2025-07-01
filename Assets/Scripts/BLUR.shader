// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/BLUR" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _BumpAmt  ("Distortion", Range (0,128)) = 10
        _MainTex ("Tint Color (RGB)", 2D) = "white" {}
        _BumpMap ("Normalmap", 2D) = "bump" {}
        _Size ("Size", Range(0, 20)) = 1
    }
 
    Category {
 
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Opaque" }
 
 
        SubShader {
     
            GrabPass {                    
                Tags { "LightMode" = "Always" }
            }
            Pass {
                Tags { "LightMode" = "Always" }
             
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
             
                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 texcoord: TEXCOORD0;
                };
             
                struct v2f {
                    float4 vertex : POSITION;
                    float4 uvgrab : TEXCOORD0;
                };
             
                v2f vert (appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    #if UNITY_UV_STARTS_AT_TOP
                    float scale = -1.0;
                    #else
                    float scale = 1.0;
                    #endif
                    o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
                    o.uvgrab.zw = o.vertex.zw;
                    return o;
                }
             
                sampler2D _GrabTexture;
                float4 _GrabTexture_TexelSize;
                float _Size;
             
                half4 frag( v2f i ) : COLOR {
                 
                    // Gaussian weights for 9 samples
                    float weights[5] = { 0.18, 0.15, 0.12, 0.09, 0.05 };

                    half4 sum = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(i.uvgrab)) * weights[0];

                    for (int j = 1; j < 5; j++) {
                        float kernelx = (float)j * _Size;
                        float2 offset = float2(_GrabTexture_TexelSize.x * kernelx, 0);
                        
                        sum += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.xy + offset, i.uvgrab.z, i.uvgrab.w))) * weights[j];
                        sum += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.xy - offset, i.uvgrab.z, i.uvgrab.w))) * weights[j];
                    }
                 
                    return sum;
                }
                ENDCG
            }
 
            GrabPass {                        
                Tags { "LightMode" = "Always" }
            }
            Pass {
                Tags { "LightMode" = "Always" }
             
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
             
                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 texcoord: TEXCOORD0;
                };
             
                struct v2f {
                    float4 vertex : POSITION;
                    float4 uvgrab : TEXCOORD0;
                };
             
                v2f vert (appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    #if UNITY_UV_STARTS_AT_TOP
                    float scale = -1.0;
                    #else
                    float scale = 1.0;
                    #endif
                    o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
                    o.uvgrab.zw = o.vertex.zw;
                    return o;
                }
             
                sampler2D _GrabTexture;
                float4 _GrabTexture_TexelSize;
                float _Size;
             
                half4 frag( v2f i ) : COLOR {
                 
                    half4 sum = half4(0,0,0,0);
                    // Gaussian weights for 9 samples
                    float weights[5] = { 0.18, 0.15, 0.12, 0.09, 0.05 };

                    sum = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(i.uvgrab)) * weights[0];

                    for (int j = 1; j < 5; j++) {
                        float kernely = (float)j * _Size;
                        float2 offset = float2(0, _GrabTexture_TexelSize.y * kernely);
                        
                        sum += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.xy + offset, i.uvgrab.z, i.uvgrab.w))) * weights[j];
                        sum += tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.xy - offset, i.uvgrab.z, i.uvgrab.w))) * weights[j];
                    }
                 
                    return sum;
                }
                ENDCG
            }
         
            GrabPass {                        
                Tags { "LightMode" = "Always" }
            }
            Pass {
                Tags { "LightMode" = "Always" }
             
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
             
                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 texcoord: TEXCOORD0;
                };
             
                struct v2f {
                    float4 vertex : POSITION;
                    float4 uvgrab : TEXCOORD0;
                    float2 uvbump : TEXCOORD1;
                    float2 uvmain : TEXCOORD2;
                };
             
                float _BumpAmt;
                float4 _BumpMap_ST;
                float4 _MainTex_ST;
             
                v2f vert (appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    #if UNITY_UV_STARTS_AT_TOP
                    float scale = -1.0;
                    #else
                    float scale = 1.0;
                    #endif
                    o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
                    o.uvgrab.zw = o.vertex.zw;
                    o.uvbump = TRANSFORM_TEX( v.texcoord, _BumpMap );
                    o.uvmain = TRANSFORM_TEX( v.texcoord, _MainTex );
                    return o;
                }
             
                fixed4 _Color;
                sampler2D _GrabTexture;
                float4 _GrabTexture_TexelSize;
                sampler2D _BumpMap;
                sampler2D _MainTex;
             
                half4 frag( v2f i ) : COLOR {
              
                    half2 bump = UnpackNormal(tex2D( _BumpMap, i.uvbump )).rg;
                    float2 offset = bump * _BumpAmt * _GrabTexture_TexelSize.xy;
                    i.uvgrab.xy = offset * i.uvgrab.z + i.uvgrab.xy;
                 
                    half4 col = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(i.uvgrab));
                    half4 tint = tex2D( _MainTex, i.uvmain ) * _Color;
                 
                    return col * tint;
                }
                ENDCG
            }
        }
    }
}