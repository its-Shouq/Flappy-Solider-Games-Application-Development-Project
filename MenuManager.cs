using UnityEngine;
using UnityEngine.SceneManagement;

public static class Difficulty
{
    public static float moveSpeed = 5f;
    public static float spawnRate = 2f;
    public static float heightOffset = 10f;
}

public class MenuManager : MonoBehaviour
{
    public GameObject levelPanel;   // اسحبي لها LevelPanel من الهيراركي

    public void OpenLevelPanel()
    {
        if (levelPanel) levelPanel.SetActive(true);
    }

    public void CloseLevelPanel()
    {
        if (levelPanel) levelPanel.SetActive(false);
    }
    private string selectedDifficulty = "Easy"; // القيمة الافتراضية

    public void SetEasy()
    {
        selectedDifficulty = "Easy";
        Debug.Log("Selected Easy Mode");
    }

    public void SetHard()
    {
        selectedDifficulty = "Hard";
        Debug.Log("Selected Hard Mode");
    }

    public void Play()
    {
        if (selectedDifficulty == "Easy")
        {
            Difficulty.moveSpeed = 3.5f;
            Difficulty.spawnRate = 6.0f;
            Difficulty.heightOffset = 14f;
        }
        else if (selectedDifficulty == "Hard")
        {
            Difficulty.moveSpeed = 9.5f;
            Difficulty.spawnRate = 1.5f;
            Difficulty.heightOffset = 9f;
        }

        SceneManager.LoadScene("SampleScene");
    }
    public void PlayEasy()
    {
        Difficulty.moveSpeed = 3.0f;   // بطيء البايب
        Difficulty.spawnRate = 14.0f; // مسافة زمنية أكبر بين الأنابيب
        Difficulty.heightOffset = 14f; //فتحة اوسع بين الانابيت
        SceneManager.LoadScene("SampleScene");
    }

    public void PlayHard()
    {
        Difficulty.moveSpeed = 10f;   // سريع جدًا
        Difficulty.spawnRate = 0.8f; // أنابيب أكثر تقاربًا
        Difficulty.heightOffset = 9f;
        SceneManager.LoadScene("SampleScene");
    }
    public GameObject shopPanel; // مرجع للوحة المتجر
    public void OpenShop() { if (shopPanel) shopPanel.SetActive(true); }
    public void CloseShop() { if (shopPanel) shopPanel.SetActive(false); }
}
