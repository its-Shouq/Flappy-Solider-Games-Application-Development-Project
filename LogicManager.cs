using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogicManager : MonoBehaviour
{
   
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    [Header("Coins")]
    public int runCoins = 0;      // كوينز هذه المحاولة
    public int totalCoins = 0;    // الرصيد الكلي (للـ Shop)
    public Text coinText;
    public AudioClip coinClip;
    [Header("Rewards - Cup")]
    public Image cupImage;          // صورة الكأس في الـUI
    public Sprite silverCupSprite;  // صورة الكأس الفضية
    public Sprite goldCupSprite;    // صورة الكأس الذهبية

    public int silverCupPipes = 10; // عدد الأنابيب للكأس الفضية
    public int goldCupPipes = 20;   // عدد الأنابيب للكأس الذهبية

    [Header("Shield")]
    public bool shieldActive = false;        // هل الشيلد شغّال الآن؟
    public float shieldDuration = 5f;        // مدة الشيلد بالثواني
    public int shieldCoinsRequired = 10;     // عدد الكوينز المطلوبة لتفعيل الشيلد
    public Image shieldIcon;                 // أيقونة الشيلد في الـUI
    public AudioClip shieldClip;             // صوت تفعيل الشيلد (اختياري)

    [Header("Win Screen UI")]
    public Text winCoinsText;
    public Text winScoreText;

    void Start()
    {
        // نقرأ الرصيد الكلي المحفوظ (للـ Shop)
        totalCoins = PlayerPrefs.GetInt("coins", 0);

        // كوينز المحاولة الحالية تبدأ من الصفر
        runCoins = 0;

        UpdateCoinUI();
        UpdateCup();
        if (shieldIcon != null)
            shieldIcon.enabled = false;
    }

    [Header("Audio")]
    public AudioSource sfxSource;       // مصدر الأصوات القصيرة
    public AudioClip gameOverClip;
    public AudioClip winClip;

    private float gameTimer = 0f;       // عداد الوقت
    public float winTime = 40f;         // المدة المطلوبة للفوز (بالثواني)
    private bool hasWon = false;

    void Update()
    {
        // نوقف العدّ لو فاز أو خسر
        if (hasWon || gameOverScreen.activeSelf)
            return;

        gameTimer += Time.deltaTime;

        if (gameTimer >= winTime)
        {
            WinGame();
        }
    }

    [ContextMenu("Increase Score ")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();

        UpdateCup(); // <-- أضفنا هذا السطر
    }

    public void AddCoins(int amount)
    {
        runCoins += amount;
        if (runCoins < 0) runCoins = 0;
        //totalCoins += amount;
        //if (totalCoins < 0)  totalCoins = 0;   // احتياط

        // بس عشان نتأكد إنها تنادَى فعلاً
        Debug.Log("Run Coins now = " + runCoins);

        // حدّث واجهة المستخدم
        UpdateCoinUI();

       

        // شغل الشيلد لو وصلنا العدد المطلوب ولسا ما تفعّل
        if (!shieldActive && runCoins > 0 && runCoins % shieldCoinsRequired==0)
        {
            ActivateShield();
        }

        // صوت الكوين
        if (sfxSource != null && coinClip != null)
            sfxSource.PlayOneShot(coinClip);
    }

    void ActivateShield()
    {
        shieldActive = true;

        if (shieldIcon != null)
            shieldIcon.enabled = true;   // يظهر الأيقونة

        if (sfxSource != null && shieldClip != null)
            sfxSource.PlayOneShot(shieldClip);   // صوت تفعيل الشيلد

        StartCoroutine(ShieldTimer());
    }

    IEnumerator ShieldTimer()
    {
        // ينتظر مدة الشيلد
        yield return new WaitForSeconds(shieldDuration);

        shieldActive = false;

        if (shieldIcon != null)
            shieldIcon.enabled = false;  // يختفي الأيقونة
    }

    // دالة بسيطة عشان نسأل المنطق: الشيلد شغّال؟
    public bool IsShieldActive()
    {
        return shieldActive;
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = runCoins.ToString();
        }
    }

    void UpdateCup()
    {
        if (cupImage == null)
            return;

        // أقل من 10: ما فيه كأس
        if (playerScore < silverCupPipes)
        {
            cupImage.enabled = false; // نخفي الصورة
        }
        // بين 10 و أقل من 20: كأس فضي
        else if (playerScore < goldCupPipes)
        {
            cupImage.enabled = true;

            if (silverCupSprite != null)
                cupImage.sprite = silverCupSprite;
        }
        // 20 أو أكثر: كأس ذهبي
        else
        {
            cupImage.enabled = true;

            if (goldCupSprite != null)
                cupImage.sprite = goldCupSprite;
        }
    }

    void SaveCoins()
    {
        // نضيف كوينز هذه المحاولة إلى الرصيد الكلي
        totalCoins += runCoins;
        PlayerPrefs.SetInt("coins", totalCoins);
        PlayerPrefs.Save();
    }

    public void restartGame()
    {
        runCoins = 0;
        UpdateCoinUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }



    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void gameOver()
    {
        if (sfxSource != null && gameOverClip != null)
            sfxSource.PlayOneShot(gameOverClip);

        gameOverScreen.SetActive(true);
        SaveCoins();
    }

    void WinGame()
    {
        hasWon = true;

        if (sfxSource != null && winClip != null)
            sfxSource.PlayOneShot(winClip);

        if (winScreen != null)
            winScreen.SetActive(true);

        if (winCoinsText != null)
            winCoinsText.text = "Coins: " + runCoins;

        if (winScoreText != null)
            winScoreText.text = "Score: " + playerScore;

        Time.timeScale = 0f; // يوقف كل الحركة في اللعبة
        SaveCoins();
    }
}
