using UnityEngine;
using UnityEngine.UI;

public class CharacterShopUI : MonoBehaviour
{
    [Header("UI")]
    public Image preview;

    [Header("Options")]
    public Sprite[] options;   // اسحبي السبرايتات هنا بالترتيب

    private int index = 0;

    void OnEnable()
    {
        index = PlayerPrefs.GetInt("Skin", 0); // يرجع آخر اختيار
        UpdatePreview();
    }

    public void Next() { index = (index + 1) % options.Length; UpdatePreview(); }
    public void Prev() { index = (index - 1 + options.Length) % options.Length; UpdatePreview(); }

    public void Select()
    {
        PlayerPrefs.SetInt("Skin", index);
        PlayerPrefs.Save();
    }


    private void UpdatePreview()
    {
        if (options != null && options.Length > 0) preview.sprite = options[index];
    }
}
