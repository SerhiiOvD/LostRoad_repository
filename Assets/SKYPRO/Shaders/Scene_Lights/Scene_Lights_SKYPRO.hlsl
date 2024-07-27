void GetLightingInformation_float(float3 WorldPos, out float3 Direction, out float3 Color, out float DistanceAttenuation, out float ShadowAttenuation)
{
    #ifdef SHADERGRAPH_PREVIEW
        Direction = float3(-0.5,0.5,-0.5);
        Color = float3(1,1,1);
        ShadowAttenuation = 0.4;
        DistanceAttenuation = 0.4;
    #else
    //    Light light = GetMainLight();
    //    Direction = light.direction;
    //    ShadowAttenuation = light.shadowAttenuation;
    //    DistanceAttenuation = light.distanceAttenuation;
    //    Color = light.color;
    //#endif
	#if SHADOWS_SCREEN
		half4 clipPos = TransformWorldToHClip(WorldPos);
		half4 shadowCoord = ComputeScreenPos(clipPos);
	#else
		half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
	#endif
		Light mainLight = GetMainLight(shadowCoord);
		Direction = mainLight.direction;
		Color = mainLight.color;
		DistanceAttenuation = mainLight.distanceAttenuation;
		ShadowAttenuation = mainLight.shadowAttenuation;
	#endif
}

void AdditionalLights_half(half3 WorldPosition, half3 WorldNormal, half3 WorldView, out half3 Colour)
{
	half3 diffuseColor = 0;

#ifndef SHADERGRAPH_PREVIEW
	WorldNormal = normalize(WorldNormal);
	WorldView = SafeNormalize(WorldView);
	int pixelLightCount = GetAdditionalLightsCount();
	for (int i = 0; i < pixelLightCount; ++i)
	{
		Light light = GetAdditionalLight(i, WorldPosition);
		half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
		diffuseColor += LightingLambert(attenuatedLightColor, light.direction, WorldNormal);
	}
#endif
	Colour = diffuseColor;
}