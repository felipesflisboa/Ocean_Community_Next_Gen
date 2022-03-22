using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Provides a helper to dynamically load presets. Can load with a tween effect.
/// </summary>
public class OceanHelper {
#pragma warning disable 162
    Ocean ocean;
    OceanTweenRunner tweenRunner;
    OceanPresetData currentData;
    OceanTween lastTween;

    /// <summary>
    /// Class constructor
    /// </summary>
    /// <param name="pTweenRunner">Tween runner. When omitted, disable tweens.</param>
    public OceanHelper(OceanTweenRunner pTweenRunner=null) {
        tweenRunner = pTweenRunner;
        ocean = Ocean.Singleton;
    }

    public void LoadOcean(TextAsset file) {
        bool isPlaying = true;
#if UNITY_EDITOR
        isPlaying = UnityEditor.EditorApplication.isPlaying;
#endif
        using (Stream s = new MemoryStream(file.bytes)) {
            using (BinaryReader br = new BinaryReader(s)) {
                if (presetLoader.readPreset(ocean, br, isPlaying)) {
                    Ocean.Singleton._name = FormatFileName(file.name);
                    CheckOceanWidth();
                    // oldRenderRefraction = Ocean.Singleton.renderRefraction;
#if UNITY_EDITOR
                    UnityEditor.EditorUtility.SetDirty(ocean);
#endif
                }
            }
        }

        //TODO remove and use a reverse load
        using (Stream s = new MemoryStream(file.bytes))
            using (BinaryReader br = new BinaryReader(s))
                currentData = new OceanPresetData(FormatFileName(file.name), br);
    }

    public void LoadOceanWithTween(TextAsset file) {
        if (tweenRunner==null) {
            Debug.LogWarning("Tween disabled. Loading ocean normally");
            LoadOcean(file);
            return;
        }
        using (Stream s = new MemoryStream(file.bytes)) {
            using (BinaryReader br = new BinaryReader(s)) {
                OceanPresetData data = new OceanPresetData(FormatFileName(file.name), br);
                Assert.IsTrue(data.valid, string.Format("Problem loading preset {0} data!", file.name));
                StartTween(data);
                Ocean.Singleton._name = FormatFileName(file.name);
                CheckOceanWidth();
            }
        }
    }

    void StartTween(OceanPresetData data) {
        OceanPresetData lastData = currentData;
        currentData = data;
        if (lastTween != null)
            lastTween.Stop();
        lastTween = new OceanTween(ocean, tweenRunner, lastData, currentData);
    }

    public void Update() {
        if (lastTween != null)
            lastTween.Update();
    }

    void CheckOceanWidth() {
        if (new[] { 8, 16, 32, 64, 128 }.Contains(ocean.width))
            ocean.height = ocean.width;
    }

    static string FormatFileName(string filename) {
        return filename.Replace(".preset", "").Replace(".bytes", ""); ;
    }
#pragma warning restore 162
}