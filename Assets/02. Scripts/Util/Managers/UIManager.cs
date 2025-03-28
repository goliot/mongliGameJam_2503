using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Internal.Commands;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
    //화면 마스킹
    [SerializeField]
    private GameObject playerMaskObj;

    //상단 UI정보
    [SerializeField]
    private Image ImgHealthBar;
    [SerializeField]
    private Image ImgSheildBar;
    [SerializeField]
    private TextMeshProUGUI TfMonsterCount;
    [SerializeField]
    private TextMeshProUGUI TfEndTime;

    //하단 UI정보
    [SerializeField]
    private TextMeshProUGUI TfBulletCount;
    [SerializeField]
    private TextMeshProUGUI TfMaxBulletCount;


    private float lastTimeChecked = 0f;  
    private bool isTweening = false;

    private void Awake()
    {
        base.Awake();
        playerMaskObj.SetActive(false);
    }

    public void SetHealthBar(float health, float maxHealth)
    {
        float targetFillAmount = health / maxHealth;
        ImgHealthBar.DOFillAmount(targetFillAmount, 0.5f).SetEase(Ease.OutCubic);
    }

    public void SetSheildBar(float sheild, float maxSheild)
    {
        float targetFillAmount = sheild / maxSheild;
        ImgSheildBar.DOFillAmount(targetFillAmount, 0.5f).SetEase(Ease.OutCubic);
    }

    public void SetMonsterCount(int count)
    {
        TfMonsterCount.text = count.ToString();
        TfMonsterCount.transform.DOScale(Vector3.one * 1.2f, 0.2f)
            .OnKill(() => TfMonsterCount.transform.DOScale(Vector3.one, 0.2f));
    }

    public void ReduceMonsterCount()
    {
        TfMonsterCount.text = (int.Parse(TfMonsterCount.text) - 1).ToString();
        TfMonsterCount.transform.DOScale(Vector3.one * 1.2f, 0.2f)
            .OnKill(() => TfMonsterCount.transform.DOScale(Vector3.one, 0.2f));
    }

    public void SetBulletCount(int count)
    {
        TfBulletCount.text = count.ToString();
        TfBulletCount.DOFade(0, 0.2f)
            .OnKill(() => TfBulletCount.DOFade(1, 0.2f));
    }

    public void SetMaxBulletCount(int count)
    {
        TfMaxBulletCount.text = count.ToString();
        TfMaxBulletCount.DOFade(0, 0.2f)
            .OnKill(() => TfMaxBulletCount.DOFade(1, 0.2f));
    }

    public void ShowPlayerMask(float duration = 0.5f)
    {
        Image maskImage = playerMaskObj.GetComponent<Image>();
        maskImage.color = new Color(maskImage.color.r, maskImage.color.g, maskImage.color.b, 0.8f);
        playerMaskObj.SetActive(true);  // 마스크 활성화
        playerMaskObj.GetComponent<Image>().DOFade(0, duration)  // 마스크의 알파 값을 0으로 바꿔서 서서히 사라지게 함
            .OnKill(() => playerMaskObj.SetActive(false));  // 애니메이션 끝난 후 마스크 비활성화
    }
    public void SetEndTime(float time = 0f)
    {
        if (time <= 30f)
        {
            TfEndTime.color = Color.red;
        }
        else
        {
            TfEndTime.color = Color.white;
        }

        if (time <= 30f && !isTweening)
        {
            TfEndTime.color = Color.red;
            if (Time.time - lastTimeChecked >= 1f)
            {
                TfEndTime.transform.DOScale(Vector3.one * 1.5f, 0.1f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutBack);
                lastTimeChecked = Time.time;
                isTweening = true;
                DOVirtual.DelayedCall(0.2f, () => isTweening = false);
            }
        }
        TfEndTime.text = string.Format("{0:N3}", time);  
    }

}

