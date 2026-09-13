using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ApplySelectedSkin : MonoBehaviour
{
    public Sprite[] options; // نفس خيارات المتجر
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        int i = PlayerPrefs.GetInt("Skin", 0);
        if (options != null && options.Length > 0 && i >= 0 && i < options.Length)
            sr.sprite = options[i];
    }
}
