// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32869,y:32538,varname:node_3138,prsc:2|emission-8588-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32165,y:32269,ptovrint:False,ptlb:ColorA,ptin:_ColorA,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:1567,x:32300,y:32805,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_1567,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8023-OUT;n:type:ShaderForge.SFN_TexCoord,id:8961,x:31498,y:32700,varname:node_8961,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:6099,x:31279,y:32877,varname:node_6099,prsc:2;n:type:ShaderForge.SFN_Frac,id:6592,x:31671,y:32900,varname:node_6592,prsc:2|IN-7754-OUT;n:type:ShaderForge.SFN_Multiply,id:6758,x:31853,y:32898,varname:node_6758,prsc:2|A-6592-OUT,B-2446-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1811,x:31520,y:33067,ptovrint:False,ptlb:x,ptin:_x,varname:node_1811,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:2222,x:31510,y:33146,ptovrint:False,ptlb:y,ptin:_y,varname:node_2222,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Append,id:2446,x:31710,y:33048,varname:node_2446,prsc:2|A-1811-OUT,B-2222-OUT;n:type:ShaderForge.SFN_Add,id:8023,x:31974,y:32716,varname:node_8023,prsc:2|A-8961-UVOUT,B-6758-OUT;n:type:ShaderForge.SFN_Multiply,id:7754,x:31476,y:32889,varname:node_7754,prsc:2|A-6099-T,B-482-OUT;n:type:ShaderForge.SFN_ValueProperty,id:482,x:31291,y:33042,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_482,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Lerp,id:8588,x:32459,y:32435,varname:node_8588,prsc:2|A-7241-RGB,B-7011-RGB,T-1567-A;n:type:ShaderForge.SFN_Color,id:7011,x:32174,y:32453,ptovrint:False,ptlb:ColorB,ptin:_ColorB,varname:node_7011,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:7241-1567-1811-2222-482-7011;pass:END;sub:END;*/

Shader "Shader Forge/S_PanTex" {
    Properties {
        _ColorA ("ColorA", Color) = (1,1,1,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _x ("x", Float ) = 1
        _y ("y", Float ) = 1
        _Speed ("Speed", Float ) = 1
        _ColorB ("ColorB", Color) = (0.5,0.5,0.5,1)
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
            uniform float4 _ColorA;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _x;
            uniform float _y;
            uniform float _Speed;
            uniform float4 _ColorB;
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
                float4 node_6099 = _Time;
                float2 node_8023 = (i.uv0+(frac((node_6099.g*_Speed))*float2(_x,_y)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_8023, _MainTex));
                float3 emissive = lerp(_ColorA.rgb,_ColorB.rgb,_MainTex_var.a);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
