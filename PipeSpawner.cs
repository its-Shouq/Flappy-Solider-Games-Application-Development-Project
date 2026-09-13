using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 2;
    private float timer = 0;
    public float heightOffset = 10;
    private bool firstPipe = true;

    [Header("Coins")]
    public GameObject coinPrefab;   // نسحب Prefab الكوين من Unity
    [Range(0f, 1f)]
    public float coinChance = 0.4f; // احتمال ظهور كوين مع كل أنبوب (0.4 = 40%)
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnRate = Difficulty.spawnRate;
        heightOffset = Difficulty.heightOffset;
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else {
            spawnPipe();
            timer = 0;
        }
            
    }
    void spawnPipe() {
        float lowestPoint = transform.position.y - heightOffset;
        float heighestPoint = transform.position.y + heightOffset;
        Instantiate(pipe, new Vector3(transform.position.x,
                               Random.Range(lowestPoint, heighestPoint),
                               0),
             transform.rotation);

        // ما نطلع كوين مع أول أنبوب
        if (!firstPipe && coinPrefab != null && Random.value < coinChance)
        {
            float gapY = (lowestPoint + heighestPoint) / 2f;
            Vector3 coinPos = new Vector3(transform.position.x +12f, gapY, 0);
            Instantiate(coinPrefab, coinPos, Quaternion.identity);
        }

        firstPipe = false;
    }
}
