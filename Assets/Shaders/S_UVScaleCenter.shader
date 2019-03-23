// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-7241-RGB,clip-1185-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:31952,y:32510,ptovrint:False,ptlb:ColorA,ptin:_ColorA,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Tex2d,id:2627,x:32200,y:32922,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_2627,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a4cdca73d61814d33ac1587f6c163bca,ntxv:0,isnm:False|UVIN-4816-OUT;n:type:ShaderForge.SFN_TexCoord,id:4719,x:31479,y:32813,varname:node_4719,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:9228,x:31682,y:32966,varname:node_9228,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:0.5|IN-4719-UVOUT;n:type:ShaderForge.SFN_Multiply,id:5370,x:31854,y:32971,varname:node_5370,prsc:2|A-9228-OUT,B-6984-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6984,x:31577,y:33189,ptovrint:False,ptlb:Scale,ptin:_Scale,varname:node_6984,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:4816,x:32069,y:33007,varname:node_4816,prsc:2|A-5370-OUT,B-8706-OUT;n:type:ShaderForge.SFN_Vector2,id:8706,x:31820,y:33227,varname:node_8706,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_OneMinus,id:1185,x:32478,y:32999,varname:node_1185,prsc:2|IN-2627-R;proporder:2627-6984-7241;pass:END;sub:END;*/

Shader "Shader Forge/S_UVScaleCenter" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Scale ("Scale", Float ) = 1
        _ColorA ("ColorA", Color) = (0.07843138,0.3921569,0.7843137,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _ColorA;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Scale;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float2 node_4816 = (((i.uv0*1.0+-0.5)*_Scale)+float2(0.5,0.5));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_4816, _MainTex));
                clip((1.0 - _MainTex_var.r) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = _ColorA.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Scale;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float2 node_4816 = (((i.uv0*1.0+-0.5)*_Scale)+float2(0.5,0.5));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_4816, _MainTex));
                clip((1.0 - _MainTex_var.r) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
