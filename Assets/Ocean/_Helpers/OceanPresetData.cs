using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Ocean Preset Parameters, not used on real Ocean Preset. To be used on interpolation.
/// 
/// Skipped material, sun, random/fixed table, shader.
/// </summary>
public class OceanPresetData {
#pragma warning disable 414 // Since there some unused data here
    string name; // For debug purposes
    public bool valid;

    bool followMainCamera;
    public float ifoamStrength;
    public float farLodOffset;

    public float scale;
    public float choppy_scale;
    public float speed;
    public float wakeDistance;
    bool renderReflection;
    bool renderRefraction;
    int renderTexWidth;
    int renderTexHeight;
    public float m_ClipPlaneOffset;
    int renderLayers;
    public float specularity;
    bool mistEnabled;
    bool dynamicWaves;
    public float humidity;
    public float pWindx;
    public float pWindy;
    public Color waterColor;
    public Color surfaceColor;
    public float foamFactor;
    bool spreadAlongFrames;
    bool shaderLod;
    int everyXframe;
    bool useShaderLods;
    int numberLods;
    public Color fakeWaterColor;
    int defaultLOD;
    int reflrefrXframe;
    public float foamUV;
    Vector3 sunPos = new Vector3(1000, 0, 0);
    public float bumpUV;
    public float ifoamWidth;
    int lodSkipFrames;
    string[] materialNameArray = new string[3];

    public float shaderAlpha;
    public float reflectivity;
    public float translucency;
    public float shoreDistance;
    public float shoreStrength;
    public float specPower;
    string shaderName;
    bool[] hasShoreArray = new bool[2];
    bool[] hasFogArray = new bool[3];
    bool[] distCanArray = new bool[3];
    public float cancellationDistance;
    public float foamDuration;
    int sTilesLod;
    int discSize;
    int lowShaderLod;
    bool forceDepth;
    public float waveDistanceFactor = 1f;
    bool useFixedGaussianRandTable;
    
    public OceanPresetData(string pName, BinaryReader br) {
        name = pName;

        if (evalStream(br)) followMainCamera = br.ReadBoolean();
        if (evalStream(br)) ifoamStrength = br.ReadSingle();
        if (evalStream(br)) farLodOffset = br.ReadSingle();

        //these values cannot be updated at runtime!
        if (evalStream(br)) br.ReadInt32();
        if (evalStream(br)) br.ReadSingle();
        if (evalStream(br)) br.ReadSingle();
        if (evalStream(br)) br.ReadSingle();
        if (evalStream(br)) br.ReadInt32();
        if (evalStream(br)) br.ReadInt32();
        if (evalStream(br)) br.ReadBoolean();
        if (evalStream(br)) br.ReadInt32();///
        if (evalStream(br)) br.ReadInt32();///

        if (evalStream(br)) scale = br.ReadSingle();
        if (evalStream(br)) choppy_scale = br.ReadSingle();
        if (evalStream(br)) speed = br.ReadSingle();
        if (evalStream(br)) br.ReadSingle();//waveSpeed : removed
        if (evalStream(br)) wakeDistance = br.ReadSingle();
        if (evalStream(br)) renderReflection = br.ReadBoolean();
        if (evalStream(br)) renderRefraction = br.ReadBoolean();
        if (evalStream(br)) renderTexWidth = br.ReadInt32();
        if (evalStream(br)) renderTexHeight = br.ReadInt32();
        if (evalStream(br)) m_ClipPlaneOffset = br.ReadSingle();
        if (evalStream(br)) renderLayers = br.ReadInt32();
        if (evalStream(br)) specularity = br.ReadSingle();
        if (evalStream(br)) mistEnabled = br.ReadBoolean();
        if (evalStream(br)) dynamicWaves = br.ReadBoolean();
        if (evalStream(br)) humidity = br.ReadSingle();
        if (evalStream(br)) pWindx = br.ReadSingle();
        if (evalStream(br)) pWindy = br.ReadSingle();
        waterColor = LoadColor(waterColor, br);
        surfaceColor = LoadColor(surfaceColor, br);
        if (evalStream(br)) foamFactor = br.ReadSingle();
        if (evalStream(br)) spreadAlongFrames = br.ReadBoolean();
        if (evalStream(br)) shaderLod = br.ReadBoolean();
        if (evalStream(br)) everyXframe = br.ReadInt32();
        if (evalStream(br)) useShaderLods = br.ReadBoolean();
        if (evalStream(br)) numberLods = br.ReadInt32();
        fakeWaterColor = LoadColor(fakeWaterColor, br);
        if (evalStream(br)) defaultLOD = br.ReadInt32();

        if (evalStream(br)) { reflrefrXframe = br.ReadInt32(); if (reflrefrXframe == 0) reflrefrXframe = 1; }

        if (evalStream(br)) foamUV = br.ReadSingle();

        sunPos = LoadVector3(sunPos, br);

        if (evalStream(br)) bumpUV = br.ReadSingle();
        if (evalStream(br)) ifoamWidth = br.ReadSingle();
        if (evalStream(br)) lodSkipFrames = br.ReadInt32();

        for (int i = 0; i < 3; i++) if (evalStream(br)) materialNameArray[i] = br.ReadString();

        if (evalStream(br)) shaderAlpha = br.ReadSingle();
        if (evalStream(br)) reflectivity = br.ReadSingle();
        if (evalStream(br)) translucency = br.ReadSingle();
        if (evalStream(br)) shoreDistance = br.ReadSingle();
        if (evalStream(br)) shoreStrength = br.ReadSingle();
        if (evalStream(br)) specPower = br.ReadSingle();

        if (evalStream(br)) {
            shaderName = br.ReadString();
            // if (nm != null) { Shader shd = Shader.Find(nm); if (shd && o.material) { o.material.shader = shd; o.oceanShader = shd; } }
        }

        for (int i = 0; i < 2; i++) if (evalStream(br)) hasShoreArray[i] = br.ReadBoolean();
        for (int i = 0; i < 3; i++) if (evalStream(br)) hasFogArray[i] = br.ReadBoolean();
        for (int i = 1; i < 3; i++) if (evalStream(br)) distCanArray[i] = br.ReadBoolean();
        if (evalStream(br)) cancellationDistance = br.ReadSingle();
        if (evalStream(br)) foamDuration = br.ReadSingle();
        if (evalStream(br)) sTilesLod = br.ReadInt32();
        if (evalStream(br)) discSize = br.ReadInt32();
        if (evalStream(br)) lowShaderLod = br.ReadInt32();
        if (evalStream(br)) forceDepth = br.ReadBoolean();
        if (evalStream(br)) waveDistanceFactor = br.ReadSingle();

        if (evalStream(br)) useFixedGaussianRandTable = br.ReadBoolean();

        if (evalStream(br)) br.ReadBoolean();//reserved
        if (evalStream(br)) br.ReadBoolean();//reserved
        if (evalStream(br)) br.ReadBoolean();//reserved

        if (evalStream(br)) br.ReadInt32();//reserved
        if (evalStream(br)) br.ReadInt32();//reserved

        if (evalStream(br)) br.ReadSingle();//reserved
        if (evalStream(br)) br.ReadSingle();//reserved
        
        if (!evalStream(br)) useFixedGaussianRandTable = false;

        valid = true;
    }
    
    /// <summary>
    /// Instant trigger boolean, integers and some special values.
    /// 
    /// Doesn't apply on 100% of values
    /// </summary>
    public void ToggleDirectValues() {
        Ocean o = Ocean.Singleton;

        SetMaterialProperty(ref o.foamUV, foamUV, "_FoamSize", 1f, 2);
        SetMaterialProperty(ref o.bumpUV, bumpUV, "_Size", 0.015625f);
        o.speed = speed;

        o.followMainCamera = followMainCamera;
        o.renderTexWidth = renderTexWidth;
        o.renderTexHeight = renderTexHeight;
        o.renderLayers = renderLayers;
        o.mistEnabled = mistEnabled;
        o.dynamicWaves = dynamicWaves;
        o.spreadAlongFrames = spreadAlongFrames;
        o.everyXframe = everyXframe;
        o.useShaderLods = useShaderLods;
        o.numberLods = numberLods;
        o.reflrefrXframe = reflrefrXframe;
        o.lodSkipFrames = lodSkipFrames;

        // string[] materialNameArray = new string[3];
        // string shaderName;

        o.sTilesLod = sTilesLod;
        o.discSize = discSize;
        o.lowShaderLod = lowShaderLod;
        o.lodSkipFrames = lodSkipFrames;
        o.forceDepth = forceDepth;
        o.lodSkipFrames = lodSkipFrames;

        // bool useFixedGaussianRandTable;

        SetMaterialProperty(ref o.hasShore, hasShoreArray[0], o.mat[0], "SHORE_ON", "SHORE_OFF");
        SetMaterialProperty(ref o.hasShore1, hasShoreArray[1], o.mat[1], "SHORE_ON", "SHORE_OFF");

        SetMaterialProperty(ref o.hasFog, hasFogArray[0], o.mat[0], "FOGON", "FOGOFF");
        SetMaterialProperty(ref o.hasFog1, hasFogArray[1], o.mat[1], "FOGON", "FOGOFF");
        SetMaterialProperty(ref o.hasFog2, hasFogArray[2], o.mat[2], "FOGON", "FOGOFF");

        // There is no 0
        SetMaterialProperty(ref o.distCan1, distCanArray[1], o.mat[1], "DCON", "DCOFF");
        SetMaterialProperty(ref o.distCan2, distCanArray[2], o.mat[2], "DCON", "DCOFF");

        if (defaultLOD != o.defaultLOD) {
            //hardcoded for now. If the shader has alpha disable refraction since it is not needed (and not supported by the shader.)
            if (defaultLOD == 6 || defaultLOD == 7 || defaultLOD == 1) {
                o.renderRefraction = false;
                //Debug.Log("Shader cannot use Refraction");
            }
            o.defaultLOD = defaultLOD;
        }

        if (o.renderReflection != renderReflection) {
            o.EnableReflection(renderReflection);
            o.renderReflection = renderReflection;
        }

        if (o.renderRefraction != renderRefraction) {
            if (o.defaultLOD != 6 && o.defaultLOD != 7) {
                o.EnableRefraction(renderRefraction);
                o.renderRefraction = renderRefraction;
            } else {
                o.renderRefraction = false;
            }
        }

		if (o.shaderLod != shaderLod) {
            o.shaderLod = shaderLod;
            
            if (o.lowShaderLod>0)
                o.shader_LOD(!o.shaderLod, o.material, o.lowShaderLod);

			if (!o.shaderLod) {
				o.EnableReflection(o.renderReflection);
                o.EnableRefraction(o.renderRefraction);
			}
		}
    }

    static void SetMaterialProperty(ref bool originalValue, bool newValue, Material mat, string stringOn, string stringOff) {
        if (originalValue == newValue)
            return;
        if (mat)
            switchKeyword(mat, stringOn, stringOff, newValue);
        originalValue = newValue;
    }

    public void SetMaterialProperty(ref float originalValue, float newValue, string key, float multiplier = 1f, int length = 3) {
        if (originalValue == newValue)
            return;
        Ocean ocean = Ocean.Singleton;
        for (int i = 0; i < length; i++)
            if (ocean.mat[i] != null)
                ocean.mat[i].SetFloat(key, newValue * multiplier);
        originalValue = newValue;
    }

    static bool evalStream(BinaryReader br) {
        return br.BaseStream.Position != br.BaseStream.Length;
    }

    static Color LoadColor(Color oldColor, BinaryReader br) {
        if (evalStream(br)) oldColor.r = br.ReadSingle();
        if (evalStream(br)) oldColor.g = br.ReadSingle();
        if (evalStream(br)) oldColor.b = br.ReadSingle();
        if (evalStream(br)) oldColor.a = br.ReadSingle();
        return oldColor;
    }

    static Vector3 LoadVector3(Vector3 oldVector3, BinaryReader br) {
        if (evalStream(br)) oldVector3.x = br.ReadSingle();
        if (evalStream(br)) oldVector3.y = br.ReadSingle();
        if (evalStream(br)) oldVector3.z = br.ReadSingle();
        return oldVector3;
    }

    static void switchKeyword(Material mat, string keyword1, string keyword2, bool on) {
        if (on) {
            mat.EnableKeyword(keyword1);
            mat.DisableKeyword(keyword2);
        } else {
            mat.EnableKeyword(keyword2);
            mat.DisableKeyword(keyword1);
        }
    }
#pragma warning restore 414
}