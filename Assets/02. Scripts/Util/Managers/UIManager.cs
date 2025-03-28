using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    //��� UI����
    [SerializeField]
    private Image ImgHealthBar;
    [SerializeField]
    private Image ImgSheildBar;
    [SerializeField]
    private TextMeshPro TfMonsterCount;

    //�ϴ� UI����
    [SerializeField]
    private TextMeshPro TfBulletCount;

    private void Awake()
    {
        base.Awake();
    }

    public void SetHealthBar(float health, int maxHealth)
    {
        ImgHealthBar.fillAmount = health / maxHealth;
    }

    public void SetSheildBar(float sheild, int maxSheild)
    {
        ImgSheildBar.fillAmount = sheild / maxSheild;
    }

    public void SetMonsterCount(int count)
    {
        TfMonsterCount.text = count.ToString();
    }

    public void SetBulletCount(int count)
    {
        TfBulletCount.text = count.ToString();
    }
}
