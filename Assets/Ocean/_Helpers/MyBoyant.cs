using UnityEngine;
using System.Collections;

/// <summary>
/// Simple non-physics based buoyancy. Just have object floating/touching the sea surface.
/// </summary>
public class MyBoyant : MonoBehaviour {
    [SerializeField, Tooltip("Use this to adjust the level the object floats, 0 is neutral")] float buoyancy;
    [SerializeField, Tooltip("When true, ocean 'pushs'.")] bool hasChoppy = false;

    protected virtual bool ShouldRefresh {
        get {
            return Ocean.Singleton.canCheckBuoyancyNow[0] == 1;
        }
    }

    protected virtual void Start() {
        hasChoppy &= Ocean.Singleton.choppy_scale > 0;
    }

    protected virtual void Update() {
        if (ShouldRefresh)
            Refresh();
    }

    protected virtual void Refresh() {
        float off = hasChoppy ? Ocean.Singleton.GetChoppyAtLocation2(transform.position.x, transform.position.z) : 0;
        float targetY = Ocean.Singleton.GetWaterHeightAtLocation2(transform.position.x - off, transform.position.z) + buoyancy;
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }
}