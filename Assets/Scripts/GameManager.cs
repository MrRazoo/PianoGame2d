using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float[] laneX = new float[4]; // Now it works!
    public float laneWidth;

    private ObjectPool objectPool;
    private GameObject LastSpawnedObject;
    [SerializeField]
    private float spawnInterval = 1f;
    private float Timer = 0f;
    private int laneCount = 4;
    
    void Awake()
    {
        CalculateLanePosition();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    void Start()
    {

        objectPool = ObjectPool.Instance;
        LastSpawnedObject = objectPool.GetFromPool();
        
    }

    
    void FixedUpdate()
    {


        Timer += Time.deltaTime;

        if(LastSpawnedObject != null && Timer >= spawnInterval)
        {
            SpawnedNextObject();
            Timer = 0f;
        }
    
    }


    private void SpawnedNextObject()
    {
        GameObject newSpawned = objectPool.GetFromPool();
        if(newSpawned != null)
        {
            LastSpawnedObject = newSpawned;
        }
        
    }

    void CalculateLanePosition()
    {
        float ScreenWidthInWorld = Camera.main.orthographicSize * 2 * ((float)Screen.width/Screen.height); // find width of our devices
        laneWidth = ScreenWidthInWorld / laneCount; // here Ones width

        for(int i = 0 ; i < laneCount ; i++) //laneCount is 4
        {
            laneX[i] = (i - ((laneCount - 1) / 2f)) * laneWidth; // find mean point of each one 
            
        }
    }
}
