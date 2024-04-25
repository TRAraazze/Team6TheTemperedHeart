using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class LoadingProgress : MonoBehaviour
{
    public Slider loadingSlider;
    public float loadingDuration = 10f;

    private void Start()
    {
        // Start the loading coroutine
        StartCoroutine(StartLoading());
    }

    private IEnumerator StartLoading()
    {
        float timer = 0f;
        float startValue = loadingSlider.minValue;
        float endValue = loadingSlider.maxValue;

        while (timer < loadingDuration)
        {
            // Calculate progress based on time elapsed
            float progress = Mathf.Lerp(startValue, endValue, timer / loadingDuration);

            // Update the slider value
            loadingSlider.value = progress;

            // Increment the timer
            timer += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the slider value reaches its maximum
        loadingSlider.value = endValue;
    }
}
