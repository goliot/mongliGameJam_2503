using UnityEngine;

public class BloodImageSelector : MonoBehaviour
{
    // Select Die Blood Image

    private SpriteRenderer _sr;
    public Sprite[] BloodSprites;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = BloodSprites[Random.Range(0, BloodSprites.Length)];
    }
}
