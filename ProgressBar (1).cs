/*
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CGP
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image ProgressImage;
        [SerializeField] private float defaultSpeed = 1f;
        [SerializeField] private UnityEvent<float> OnProgress;
        [SerializeField] private UnityEvent OnCompleted;

        private Coroutine AnimationCoroutine;

        public void SetProgress(float progress)
        {
            SetProgress(progress, defaultSpeed);
        }

        public void SetProgress(float progress, float speed)
        {
            if (progress < 0 || progress > 1)
            {
                progress = Mathf.Clamp01(progress);
            }
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(AnimateProgress(progress, speed));
        }

        private IEnumerator AnimateProgress(float progress, float speed)
        {
            float time = 0;
            float initialProgress = ProgressImage.fillAmount;

            while (time < 1)
            {
                ProgressImage.fillAmount = Mathf.Lerp(initialProgress, progress, time);
                time += Time.deltaTime * speed;

                OnProgress?.Invoke(ProgressImage.fillAmount);
                
                yield return null;
            }

            ProgressImage.fillAmount = progress;
            OnProgress?.Invoke(progress);
            OnCompleted?.Invoke();
        }
    }
}
*/