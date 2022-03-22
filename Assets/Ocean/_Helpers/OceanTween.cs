using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic Tween helper. Override Start/End for specific tween logic.
/// </summary>
public class OceanTween {
    Ocean ocean;
    OceanTweenRunner tweenRunner;
    OceanPresetData lastData;
    OceanPresetData newData;
    internal bool finished;
    
    public OceanTween(Ocean pOcean, OceanTweenRunner pTweenRunner, OceanPresetData pLastData, OceanPresetData pNewData) {
        ocean = pOcean;
        tweenRunner = pTweenRunner;
        lastData = pLastData;
        newData = pNewData;
        Start();
    }

    public void Start() {
        newData.ToggleDirectValues();
        tweenRunner.Start(this);
    }

    /// <summary>
    /// Setter called on each frame.
    /// </summary>
    /// <param name="ratio">Tween ratio</param>
    public void SetterTweenData(float ratio) {
        if (ocean == null)
            return;
        ocean.waterColor = LerpOnMaterials(lastData.waterColor, newData.waterColor, ratio, "_WaterColor");
        ocean.surfaceColor = LerpOnMaterials(lastData.surfaceColor, newData.surfaceColor, ratio, "_SurfaceColor");
        ocean.fakeWaterColor = LerpOnMaterials(lastData.fakeWaterColor, newData.fakeWaterColor, ratio, "_FakeUnderwaterColor");

        ocean.foamFactor = LerpOnMaterials(lastData.foamFactor, newData.foamFactor, ratio, "_FoamFactor");
        ocean.specularity = LerpOnMaterials(lastData.specularity, newData.specularity, ratio, "_Specularity");
        ocean.specPower = LerpOnMaterials(lastData.specPower, newData.specPower, ratio, "_SpecPower");
        ocean.translucency = LerpOnMaterials(lastData.translucency, newData.translucency, ratio, "_Translucency");
        ocean.shoreDistance = LerpOnMaterials(lastData.shoreDistance, newData.shoreDistance, ratio, "_ShoreDistance");
        ocean.shoreStrength = LerpOnMaterials(lastData.shoreStrength, newData.shoreStrength, ratio, "_ShoreStrength");

        // ocean.foamUV = LerpOnMaterials(lastData.foamUV, newData.foamUV, ratio, "_FoamSize", 1f, 2); // Toggle
        // ocean.bumpUV = LerpOnMaterials(lastData.bumpUV, newData.bumpUV, ratio, "_Size", 0.015625f); // Toggle
        ocean.shaderAlpha = LerpOnMaterials(lastData.shaderAlpha, newData.shaderAlpha, ratio, "_WaterLod1Alpha");
        ocean.cancellationDistance = LerpOnMaterials(lastData.cancellationDistance, newData.cancellationDistance, ratio, "_DistanceCancellation");

        ocean.ifoamStrength = Lerp(lastData.ifoamStrength, newData.ifoamStrength, ratio);
        ocean.farLodOffset = Lerp(lastData.farLodOffset, newData.farLodOffset, ratio);
        ocean.scale = Lerp(lastData.scale, newData.scale, ratio);
        ocean.choppy_scale = Lerp(lastData.choppy_scale, newData.choppy_scale, ratio);
        // ocean.speed = Lerp(lastData.speed, newData.speed, ratio); // Toggle
        ocean.wakeDistance = Lerp(lastData.wakeDistance, newData.wakeDistance, ratio);
        ocean.m_ClipPlaneOffset = Lerp(lastData.m_ClipPlaneOffset, newData.m_ClipPlaneOffset, ratio);

        ocean.humidity = Lerp(lastData.humidity, newData.humidity, ratio);
        ocean.pWindx = Lerp(lastData.pWindx, newData.pWindx, ratio);
        ocean.pWindy = Lerp(lastData.pWindy, newData.pWindy, ratio);
        ocean.ifoamWidth = Lerp(lastData.ifoamWidth, newData.ifoamWidth, ratio);
        ocean.reflectivity = Lerp(lastData.reflectivity, newData.reflectivity, ratio);
        ocean.foamDuration = Lerp(lastData.foamDuration, newData.foamDuration, ratio);
        ocean.waveDistanceFactor = Lerp(lastData.waveDistanceFactor, newData.waveDistanceFactor, ratio);
    }

    Color LerpOnMaterials(Color oldValue, Color newValue, float ratio, string key) {
        if (oldValue == newValue)
            return newValue;
        Color ret = Color.Lerp(oldValue, newValue, ratio);
        for (int i = 0; i < 3; i++)
            if (ocean.mat[i] != null)
                ocean.mat[i].SetColor(key, ret);
        return ret;
    }

    public float LerpOnMaterials(float oldValue, float newValue, float ratio, string key) {
        if (oldValue == newValue)
            return newValue;
        float ret = Mathf.Lerp(oldValue, newValue, ratio);
        for (int i = 0; i < 3; i++)
            if (ocean.mat[i] != null)
                ocean.mat[i].SetFloat(key, ret);
        return ret;
    }

    float Lerp(float a, float b, float t) {
        if (a == b)
            return a;
        return Mathf.Lerp(a, b, t);
    }

    public void Update() {
        tweenRunner.Update();
    }

    public void Stop() {
        tweenRunner.Stop();
    }
}