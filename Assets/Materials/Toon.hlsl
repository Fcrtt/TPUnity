void ToonShading_float(in float3 Normal,in float3 WorldPos, in float4 ToonRampTinting, out float3 ToonRampOutput)
{
	// set the shader graph node previews
#ifdef SHADERGRAPH_PREVIEW
	ToonRampOutput = float3(0.5, 0.5, 0);
#else
	// grab the shadow coordinates
	half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);

	// grab the main light
#if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS
	Light light = GetMainLight(shadowCoord);
#else
	Light light = GetMainLight();
#endif

	// dot product for toonramp
	half d = dot(Normal, light.direction) * 0.5 + 0.5;

	// toonramp in a smoothstep
	half toonRamp = smoothstep(0.47, 0.53, d);
	// multiply with shadows;
	toonRamp *= light.shadowAttenuation;
	// add in lights and extra tinting
	ToonRampOutput = light.color * (toonRamp + ToonRampTinting);
#endif
}