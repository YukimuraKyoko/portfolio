Shader "Kyoko/AnimationSphere"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)
        //_Xvalue("Xvalue", Range(-1,1)) = 1.0
        _Xstep("Xstep", Range(-0.01,1)) = 1.0
        //_Yvalue("Yvalue", Range(-1,1)) = 1.0
        _Ystep("Ystep", Range(0,1)) = 1.0
        _Xfrac("Xfrac", Range(1,10)) = 10.
        _Yfrac("Yfrac", Range(1,10)) = 10.
        _Xcollapse("Xcollapse", Range(0,1)) = 0
        _Ycollapse("Ycollapse", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Front
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _Color2;
            //float _Xvalue;
            float _Xstep;
            //float _Yvalue;
            float _Ystep;
            int _Xfrac;
            int _Yfrac;
            float _Xcollapse;
            float _Ycollapse;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 col = tex2D(_MainTex, uv);
                float3 XcollapseUse;
                float3 YcollapseUse;
                
                if(_Xcollapse == 1){
                    XcollapseUse = frac(-uv.x * _Xfrac) < _Xstep ? _Color2 : _Color;
				}
                else{
                    XcollapseUse = 0;
				}
                if(_Ycollapse == 1){
                    YcollapseUse = frac(-uv.y * _Yfrac) < _Ystep ? _Color2 : _Color;
				}
                else{
                    YcollapseUse = 0;
				}
                
                //float mix = frac(uv.x* _Xfrac);
                //col.rgb = lerp(_Color, _Color2, mix);
                //col.rgb = frac(uv.x * _Xfrac) < _Xstep ? _Color2 : _Color;
                col.rgb = (frac(uv.x * _Xfrac) < _Xstep ? _Color2 : _Color) +
                XcollapseUse + 
                YcollapseUse +
                (frac(uv.y * _Yfrac) < _Ystep ? _Color2 : _Color);

                return col;
            }
            ENDCG
        }
    }
}
