using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    [SerializeField] float slowDownFactor = 0.05f;
    [SerializeField] float slowDownLength = 2f;

    void Update()
    {
        ResetTimeScale();
    }

    void ResetTimeScale() {
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void SlowDownTime() {
        // TODO slow down animation time
        // TODO slow audio / increase pitch
        // TODO disable jumping after teleporting?

        Time.timeScale = slowDownFactor; // Adjusts regulat timeScale

        // * 0.02 as we want fixed update around 50 times per second
        Time.fixedDeltaTime = Time.timeScale * .02f; // Adjusts physics timeScale
    }
}
