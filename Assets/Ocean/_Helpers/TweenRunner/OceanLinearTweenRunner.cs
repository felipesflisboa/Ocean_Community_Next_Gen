using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Executes tween using linear progression.
/// </summary>
public class OceanLinearTweenRunner : OceanTweenRunner {
    protected override void StartTween() {}

    protected override void UpdateTween(float ratio) {
        currentTween.SetterTweenData(ratio);
    }

    protected override void StopTween() {}
}