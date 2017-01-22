using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class DynamicEffects : MonoBehaviour {

    public float MinBloom;
    public float MaxBloom;

    private BloomOptimized bloomScript;

    public float MinVignette;
    public float MaxVignette;

    private VignetteAndChromaticAberration vignetteScript;
    // Use this for initialization
    void Start () {
        bloomScript = GetComponent<BloomOptimized>();
        vignetteScript = GetComponent<VignetteAndChromaticAberration>();
    }

    // Update is called once per frame
    void Update() { 

        int joy = RoyalBehaviour.Instance.Joy;
        bloomScript.threshold = MinBloom + (MaxBloom - MinBloom)*(joy / 100.0f);

        int vignetteJoy = Mathf.Clamp(joy, 0, 20);
        vignetteScript.intensity = MaxVignette + (MinVignette - MaxVignette)*(1 - joy/20.0f);
	}
}
