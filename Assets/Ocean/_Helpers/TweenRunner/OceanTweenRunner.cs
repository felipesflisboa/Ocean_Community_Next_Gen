using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Executes tween. Override with specific tween engine.
/// </summary>
public abstract class OceanTweenRunner {
    public float duration = 1;
    protected OceanTween currentTween;
    protected float startTime;

    public bool Running {
        get {
            return currentTween != null && !currentTween.finished;
        }
    }

    /// <summary>
    /// When true, do update calculation.
    /// </summary>
    protected abstract bool UseUpdateCall {
        get;
    }

    public void Start(OceanTween tween) {
        currentTween = tween;
        startTime = Time.timeSinceLevelLoad;
        StartTween();
    }

    protected abstract void StartTween();

    public void Update() {
        if (!UseUpdateCall || !Running)
            return;
        float ratio = Mathf.Min(Time.timeSinceLevelLoad / (Time.timeSinceLevelLoad + duration), 1f);
        UpdateTween(ratio);
        if (ratio == 1f) {
            currentTween.finished = true;
            Stop();
        }
    }

    protected abstract void UpdateTween(float ratio);

    public void Stop() {
        StopTween();
        currentTween = null;
    }

    protected abstract void StopTween();
}