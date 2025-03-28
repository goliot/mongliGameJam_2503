using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class DelayHpBar : MonoBehaviour
{
    /*Slider slider;
    public Slider SubSlider;
    public Boss boss;

    public float subSliderDelay = 0.5f; // 서브 슬라이더 지연 시간
    public float subSliderSpeed = 2.0f; // 보간 속도

    private RectTransform rectTransform;
    private Vector2 originalPos;

    void Awake()
    {
        slider = GetComponent<Slider>();
        rectTransform = GetComponent<RectTransform>();
        originalPos = rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        slider.value = 1;
        SubSlider.value = 1;
    }

    void Update()
    {
        if (boss != null)
        {
            float targetValue = boss.Hp / boss.MaxHp;
            slider.value = targetValue;

            if (SubSlider.value > targetValue)
            {
                StartCoroutine(UpdateSubSlider(targetValue));
            }
        }
    }

    public void ShakeSlider()
    {
        rectTransform.DOShakeAnchorPos(0.1f, 10f, 10, 90, false, true)
           .OnComplete(() => rectTransform.DOAnchorPos(originalPos, 0.1f));
    }

    IEnumerator UpdateSubSlider(float targetValue)
    {
        yield return new WaitForSeconds(subSliderDelay);

        while (SubSlider.value > targetValue)
        {
            SubSlider.value = Mathf.Lerp(SubSlider.value, targetValue, Time.deltaTime * subSliderSpeed);
            yield return null;
        }

        SubSlider.value = targetValue;
    }*/
}
