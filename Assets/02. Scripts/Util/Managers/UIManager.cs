using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Internal.Commands;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
    //��� UI����
    [SerializeField]
    private GameObject playerMaskObj;
    [SerializeField]
    private Image ImgHealthBar;
    [SerializeField]
    private Image ImgSheildBar;
    [SerializeField]
    private TextMeshProUGUI TfMonsterCount;

    //�ϴ� UI����
    [SerializeField]
    private TextMeshProUGUI TfBulletCount;
    [SerializeField]
    private TextMeshProUGUI TfMaxBulletCount;

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
        playerMaskObj.SetActive(true);  // ����ũ Ȱ��ȭ
        playerMaskObj.GetComponent<Image>().DOFade(0, duration)  // ����ũ�� ���� ���� 0���� �ٲ㼭 ������ ������� ��
            .OnKill(() => playerMaskObj.SetActive(false));  // �ִϸ��̼� ���� �� ����ũ ��Ȱ��ȭ
    }
}
