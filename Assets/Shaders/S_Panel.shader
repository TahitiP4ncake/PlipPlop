// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-2348-OUT;n:type:ShaderForge.SFN_Tex2d,id:3501,x:31816,y:32913,ptovrint:False,ptlb:Panel01,ptin:_Panel01,varname:node_3501,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:34f8a3936f72622488fddfa3977c522a,ntxv:0,isnm:False|UVIN-3081-OUT;n:type:ShaderForge.SFN_Tex2d,id:3658,x:31886,y:32548,ptovrint:False,ptlb:Panel02,ptin:_Panel02,varname:node_3658,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bffd413c3697e5e49bf210cc2c14f157,ntxv:0,isnm:False|UVIN-3081-OUT;n:type:ShaderForge.SFN_Slider,id:1526,x:30832,y:33051,ptovrint:False,ptlb:Scroll,ptin:_Scroll,varname:node_1526,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_TexCoord,id:8197,x:31252,y:32853,varname:node_8197,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:3953,x:31447,y:32895,varname:node_3953,prsc:2|A-8197-V,B-4877-OUT;n:type:ShaderForge.SFN_Append,id:3081,x:31616,y:32863,varname:node_3081,prsc:2|A-8197-U,B-3953-OUT;n:type:ShaderForge.SFN_Add,id:2348,x:32301,y:32825,varname:node_2348,prsc:2|A-5114-OUT,B-8608-OUT;n:type:ShaderForge.SFN_Multiply,id:5114,x:32109,y:32648,varname:node_5114,prsc:2|A-3658-RGB,B-8756-OUT;n:type:ShaderForge.SFN_Multiply,id:8608,x:32109,y:32879,varname:node_8608,prsc:2|A-3501-RGB,B-4025-OUT;n:type:ShaderForge.SFN_Step,id:8756,x:31816,y:33119,varname:node_8756,prsc:2|A-3953-OUT,B-7821-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7821,x:31563,y:33233,ptovrint:False,ptlb:value,ptin:_value,varname:node_7821,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_OneMinus,id:4025,x:31970,y:33110,varname:node_4025,prsc:2|IN-8756-OUT;n:type:ShaderForge.SFN_RemapRange,id:4877,x:31163,y:33048,varname:node_4877,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:0|IN-1526-OUT;proporder:3501-1526-3658-7821;pass:END;sub:END;*/

Shader "Shader Forge/S_Panel" {
    Properties {
        _Panel01 ("Panel01", 2D) = "white" {}
        _Scroll ("Scroll", Range(0, 1)) = 0
        _Panel02 ("Panel02", 2D) = "white" {}
        _value ("value", Float ) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
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
            uniform sampler2D _Panel01; uniform float4 _Panel01_ST;
            uniform sampler2D _Panel02; uniform float4 _Panel02_ST;
            uniform float _Scroll;
            uniform float _value;
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
////// Lighting:
////// Emissive:
                float node_3953 = (i.uv0.g+(_Scroll*1.0+-1.0));
                float2 node_3081 = float2(i.uv0.r,node_3953);
                float4 _Panel02_var = tex2D(_Panel02,TRANSFORM_TEX(node_3081, _Panel02));
                float node_8756 = step(node_3953,_value);
                float4 _Panel01_var = tex2D(_Panel01,TRANSFORM_TEX(node_3081, _Panel01));
                float3 emissive = ((_Panel02_var.rgb*node_8756)+(_Panel01_var.rgb*(1.0 - node_8756)));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
