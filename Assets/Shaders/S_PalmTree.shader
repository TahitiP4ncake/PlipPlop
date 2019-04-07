// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32719,y:32712,varname:node_2865,prsc:2|diff-116-RGB,spec-9793-OUT,gloss-5580-OUT,normal-9254-RGB,voffset-3255-OUT;n:type:ShaderForge.SFN_Tex2d,id:2791,x:31675,y:32746,ptovrint:True,ptlb:ARMC,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9254,x:31804,y:32899,ptovrint:True,ptlb:Normal Map,ptin:_BumpMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Time,id:5675,x:29900,y:33500,varname:node_5675,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8247,x:30520,y:33521,varname:node_8247,prsc:2|A-6045-OUT,B-4579-OUT,C-4270-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4579,x:30311,y:33640,ptovrint:False,ptlb:FPS,ptin:_FPS,varname:node_432,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:6;n:type:ShaderForge.SFN_Floor,id:5274,x:30716,y:33521,varname:node_5274,prsc:2|IN-8247-OUT;n:type:ShaderForge.SFN_Divide,id:2209,x:30892,y:33521,varname:node_2209,prsc:2|A-5274-OUT,B-4579-OUT;n:type:ShaderForge.SFN_Frac,id:6635,x:31050,y:33521,varname:node_6635,prsc:2|IN-2209-OUT;n:type:ShaderForge.SFN_VertexColor,id:6028,x:31246,y:33832,varname:node_6028,prsc:2;n:type:ShaderForge.SFN_Set,id:8608,x:31398,y:33849,varname:Horizontal,prsc:2|IN-6028-R;n:type:ShaderForge.SFN_Set,id:2595,x:31201,y:33521,varname:motion,prsc:2|IN-6635-OUT;n:type:ShaderForge.SFN_Multiply,id:3713,x:32267,y:33168,varname:node_3713,prsc:2|A-1231-OUT,B-7211-OUT;n:type:ShaderForge.SFN_Get,id:1231,x:32041,y:33168,varname:node_1231,prsc:2|IN-8608-OUT;n:type:ShaderForge.SFN_Vector3,id:6779,x:31431,y:33388,varname:node_6779,prsc:2,v1:1,v2:0,v3:0;n:type:ShaderForge.SFN_Get,id:4106,x:31029,y:33244,varname:node_4106,prsc:2|IN-2595-OUT;n:type:ShaderForge.SFN_Sin,id:7898,x:31628,y:33245,varname:node_7898,prsc:2|IN-8535-OUT;n:type:ShaderForge.SFN_Multiply,id:4096,x:31804,y:33245,varname:node_4096,prsc:2|A-7898-OUT,B-2558-XYZ,C-4095-OUT;n:type:ShaderForge.SFN_Multiply,id:8535,x:31479,y:33243,varname:node_8535,prsc:2|A-4106-OUT,B-4021-OUT;n:type:ShaderForge.SFN_Tau,id:4021,x:31321,y:33354,varname:node_4021,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:4095,x:31435,y:33498,ptovrint:False,ptlb:xOffset,ptin:_xOffset,varname:node_2192,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.05;n:type:ShaderForge.SFN_Vector3,id:911,x:31581,y:33692,varname:node_911,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Get,id:2936,x:31388,y:33560,varname:node_2936,prsc:2|IN-2595-OUT;n:type:ShaderForge.SFN_Sin,id:1581,x:31742,y:33561,varname:node_1581,prsc:2|IN-6850-OUT;n:type:ShaderForge.SFN_Multiply,id:95,x:31918,y:33561,varname:node_95,prsc:2|A-1581-OUT,B-3911-XYZ,C-7695-OUT;n:type:ShaderForge.SFN_Multiply,id:6850,x:31593,y:33559,varname:node_6850,prsc:2|A-2936-OUT,B-1136-OUT;n:type:ShaderForge.SFN_Tau,id:1136,x:31435,y:33670,varname:node_1136,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:7695,x:31581,y:33815,ptovrint:False,ptlb:zOffset,ptin:_zOffset,varname:node_336,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.05;n:type:ShaderForge.SFN_Add,id:7211,x:32072,y:33227,varname:node_7211,prsc:2|A-4096-OUT,B-95-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4270,x:30272,y:33757,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_7148,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:6045,x:30101,y:33534,varname:node_6045,prsc:2|A-5675-T,B-6756-OUT,C-8874-OUT;n:type:ShaderForge.SFN_ObjectPosition,id:8868,x:29184,y:33679,varname:node_8868,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6756,x:29852,y:33692,varname:node_6756,prsc:2|A-928-R,B-2794-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2794,x:29648,y:33860,ptovrint:False,ptlb:xScale,ptin:_xScale,varname:node_9612,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:8874,x:29909,y:33836,varname:node_8874,prsc:2|A-928-B,B-9616-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9616,x:29747,y:33977,ptovrint:False,ptlb:zScale,ptin:_zScale,varname:node_7490,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_OneMinus,id:5580,x:32176,y:32778,varname:node_5580,prsc:2|IN-2791-G;n:type:ShaderForge.SFN_Tex2d,id:116,x:32095,y:32450,ptovrint:False,ptlb:BC,ptin:_BC,varname:node_7308,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ComponentMask,id:928,x:29528,y:33657,varname:node_928,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-8868-XYZ;n:type:ShaderForge.SFN_Transform,id:2558,x:31603,y:33388,varname:node_2558,prsc:2,tffrom:1,tfto:0|IN-6779-OUT;n:type:ShaderForge.SFN_Transform,id:3911,x:31767,y:33706,varname:node_3911,prsc:2,tffrom:1,tfto:0|IN-911-OUT;n:type:ShaderForge.SFN_Slider,id:9793,x:31963,y:32675,ptovrint:False,ptlb:met,ptin:_met,varname:node_4534,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Vector3,id:303,x:31717,y:33956,varname:node_303,prsc:2,v1:0,v2:1,v3:0;n:type:ShaderForge.SFN_Transform,id:6589,x:31879,y:33956,varname:node_6589,prsc:2,tffrom:1,tfto:0|IN-303-OUT;n:type:ShaderForge.SFN_Multiply,id:3392,x:32330,y:33892,varname:node_3392,prsc:2|A-6589-XYZ,B-5512-OUT,C-5655-OUT,D-5879-OUT;n:type:ShaderForge.SFN_Set,id:6591,x:31429,y:33940,varname:Vertical,prsc:2|IN-6028-B;n:type:ShaderForge.SFN_Add,id:3255,x:32447,y:33228,varname:node_3255,prsc:2|A-3713-OUT,B-3392-OUT;n:type:ShaderForge.SFN_Get,id:5512,x:31858,y:34142,varname:node_5512,prsc:2|IN-6591-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5655,x:32005,y:33894,ptovrint:False,ptlb:yOffset,ptin:_yOffset,varname:node_5655,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.05;n:type:ShaderForge.SFN_Multiply,id:714,x:31925,y:34203,varname:node_714,prsc:2|A-2239-OUT,B-9084-OUT;n:type:ShaderForge.SFN_Tau,id:9084,x:31771,y:34270,varname:node_9084,prsc:2;n:type:ShaderForge.SFN_Sin,id:5879,x:32055,y:34174,varname:node_5879,prsc:2|IN-714-OUT;n:type:ShaderForge.SFN_Add,id:272,x:30310,y:33998,varname:node_272,prsc:2|A-6045-OUT,B-8782-OUT;n:type:ShaderForge.SFN_Vector1,id:8782,x:30108,y:34060,varname:node_8782,prsc:2,v1:0.245;n:type:ShaderForge.SFN_Multiply,id:4204,x:30645,y:33985,varname:node_4204,prsc:2|A-272-OUT,B-4579-OUT,C-4270-OUT;n:type:ShaderForge.SFN_Floor,id:2416,x:30837,y:33987,varname:node_2416,prsc:2|IN-4204-OUT;n:type:ShaderForge.SFN_Divide,id:3434,x:31022,y:33976,varname:node_3434,prsc:2|A-2416-OUT,B-4579-OUT;n:type:ShaderForge.SFN_Frac,id:2239,x:31551,y:34222,varname:node_2239,prsc:2|IN-3434-OUT;proporder:116-9793-2791-9254-4579-4095-7695-4270-2794-9616-5655;pass:END;sub:END;*/

Shader "Shader Forge/S_PalmTree" {
    Properties {
        _BC ("BC", 2D) = "white" {}
        _met ("met", Range(0, 1)) = 0
        _MainTex ("ARMC", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _FPS ("FPS", Float ) = 6
        _xOffset ("xOffset", Float ) = 0.05
        _zOffset ("zOffset", Float ) = 0.05
        _Speed ("Speed", Float ) = 1
        _xScale ("xScale", Float ) = 1
        _zScale ("zScale", Float ) = 1
        _yOffset ("yOffset", Float ) = 0.05
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _FPS;
            uniform float _xOffset;
            uniform float _zOffset;
            uniform float _Speed;
            uniform float _xScale;
            uniform float _zScale;
            uniform sampler2D _BC; uniform float4 _BC_ST;
            uniform float _met;
            uniform float _yOffset;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float Horizontal = o.vertexColor.r;
                float4 node_5675 = _Time;
                float3 node_928 = objPos.rgb.rgb;
                float node_6045 = (node_5675.g+(node_928.r*_xScale)+(node_928.b*_zScale));
                float motion = frac((floor((node_6045*_FPS*_Speed))/_FPS));
                float Vertical = o.vertexColor.b;
                v.vertex.xyz += ((Horizontal*((sin((motion*6.28318530718))*mul( unity_ObjectToWorld, float4(float3(1,0,0),0) ).xyz.rgb*_xOffset)+(sin((motion*6.28318530718))*mul( unity_ObjectToWorld, float4(float3(0,0,1),0) ).xyz.rgb*_zOffset)))+(mul( unity_ObjectToWorld, float4(float3(0,1,0),0) ).xyz.rgb*Vertical*_yOffset*sin((frac((floor(((node_6045+0.245)*_FPS*_Speed))/_FPS))*6.28318530718))));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float gloss = (1.0 - _MainTex_var.g);
                float perceptualRoughness = 1.0 - (1.0 - _MainTex_var.g);
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _met;
                float specularMonochrome;
                float4 _BC_var = tex2D(_BC,TRANSFORM_TEX(i.uv0, _BC));
                float3 diffuseColor = _BC_var.rgb; // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _FPS;
            uniform float _xOffset;
            uniform float _zOffset;
            uniform float _Speed;
            uniform float _xScale;
            uniform float _zScale;
            uniform sampler2D _BC; uniform float4 _BC_ST;
            uniform float _met;
            uniform float _yOffset;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float Horizontal = o.vertexColor.r;
                float4 node_5675 = _Time;
                float3 node_928 = objPos.rgb.rgb;
                float node_6045 = (node_5675.g+(node_928.r*_xScale)+(node_928.b*_zScale));
                float motion = frac((floor((node_6045*_FPS*_Speed))/_FPS));
                float Vertical = o.vertexColor.b;
                v.vertex.xyz += ((Horizontal*((sin((motion*6.28318530718))*mul( unity_ObjectToWorld, float4(float3(1,0,0),0) ).xyz.rgb*_xOffset)+(sin((motion*6.28318530718))*mul( unity_ObjectToWorld, float4(float3(0,0,1),0) ).xyz.rgb*_zOffset)))+(mul( unity_ObjectToWorld, float4(float3(0,1,0),0) ).xyz.rgb*Vertical*_yOffset*sin((frac((floor(((node_6045+0.245)*_FPS*_Speed))/_FPS))*6.28318530718))));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float gloss = (1.0 - _MainTex_var.g);
                float perceptualRoughness = 1.0 - (1.0 - _MainTex_var.g);
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _met;
                float specularMonochrome;
                float4 _BC_var = tex2D(_BC,TRANSFORM_TEX(i.uv0, _BC));
                float3 diffuseColor = _BC_var.rgb; // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _FPS;
            uniform float _xOffset;
            uniform float _zOffset;
            uniform float _Speed;
            uniform float _xScale;
            uniform float _zScale;
            uniform float _yOffset;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float Horizontal = o.vertexColor.r;
                float4 node_5675 = _Time;
                float3 node_928 = objPos.rgb.rgb;
                float node_6045 = (node_5675.g+(node_928.r*_xScale)+(node_928.b*_zScale));
                float motion = frac((floor((node_6045*_FPS*_Speed))/_FPS));
                float Vertical = o.vertexColor.b;
                v.vertex.xyz += ((Horizontal*((sin((motion*6.28318530718))*mul( unity_ObjectToWorld, float4(float3(1,0,0),0) ).xyz.rgb*_xOffset)+(sin((motion*6.28318530718))*mul( unity_ObjectToWorld, float4(float3(0,0,1),0) ).xyz.rgb*_zOffset)))+(mul( unity_ObjectToWorld, float4(float3(0,1,0),0) ).xyz.rgb*Vertical*_yOffset*sin((frac((floor(((node_6045+0.245)*_FPS*_Speed))/_FPS))*6.28318530718))));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _FPS;
            uniform float _xOffset;
            uniform float _zOffset;
            uniform float _Speed;
            uniform float _xScale;
            uniform float _zScale;
            uniform sampler2D _BC; uniform float4 _BC_ST;
            uniform float _met;
            uniform float _yOffset;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float Horizontal = o.vertexColor.r;
                float4 node_5675 = _Time;
                float3 node_928 = objPos.rgb.rgb;
                float node_6045 = (node_5675.g+(node_928.r*_xScale)+(node_928.b*_zScale));
                float motion = frac((floor((node_6045*_FPS*_Speed))/_FPS));
                float Vertical = o.vertexColor.b;
                v.vertex.xyz += ((Horizontal*((sin((motion*6.28318530718))*mul( unity_ObjectToWorld, float4(float3(1,0,0),0) ).xyz.rgb*_xOffset)+(sin((motion*6.28318530718))*mul( unity_ObjectToWorld, float4(float3(0,0,1),0) ).xyz.rgb*_zOffset)))+(mul( unity_ObjectToWorld, float4(float3(0,1,0),0) ).xyz.rgb*Vertical*_yOffset*sin((frac((floor(((node_6045+0.245)*_FPS*_Speed))/_FPS))*6.28318530718))));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float4 _BC_var = tex2D(_BC,TRANSFORM_TEX(i.uv0, _BC));
                float3 diffColor = _BC_var.rgb;
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, _met, specColor, specularMonochrome );
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float roughness = 1.0 - (1.0 - _MainTex_var.g);
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
