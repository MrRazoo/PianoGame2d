using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{
    
    private Rigidbody2D rb;
    public float speed = 5f;
    private ObjectPool objectPool;
    private SoundManager soundManager;
    private GameManager gameManager;
    private Vector2 direction = Vector2.down; // its a downward direction -1 y-axis
    [SerializeField]
    private Object destroyRectRef;
    
    



    void Start()
    {
        soundManager = SoundManager.Instance;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {  
        gameManager = GameManager.Instance;
        objectPool = ObjectPool.Instance;
        
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        rb.position = new Vector2(gameManager.laneX[Random.Range(0, 4)], 5.972f);
        // rb.velocity = Vector2.zero;
        ScaleToLaneWidth();
    }

    void Update()
    {
        HandleTouch();
    }
    
    void FixedUpdate()
    {
        if(rb != null)
        {
            rb.velocity = direction * speed;
        } 

        if(rb.transform.position.y <= -6f)
        {
            objectPool.ReturnObjectToPool(gameObject);
        }
        
    }


    void HandleTouch()
    {
        
        if(Input.touchCount > 0)
        {
            for(int i = 0 ; i < Input.touchCount ; i++)
            {
                Touch touch = Input.GetTouch(i);
                if(touch.phase == TouchPhase.Began)
                {
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                    if(hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        ExplosionHappens();
                        objectPool.ReturnObjectToPool(gameObject);
                        soundManager.PlaySFX(soundManager.clicked);
                    }
                }
            }
        }
    }


    void ScaleToLaneWidth()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Get the sprite's original width (at scale 1)
            float originalWidth = spriteRenderer.sprite.bounds.size.x;
            // Calculate the scale needed to match laneWidth
            float scaleX = gameManager.laneWidth / originalWidth;
            // Apply the scale (keep Y scale unchanged)
            transform.localScale = new Vector3(scaleX, transform.localScale.y, 1f);
        }
    }

    private void ExplosionHappens()
    {
        GameObject NowDestructable = (GameObject)Instantiate(destroyRectRef);
        NowDestructable.transform.position = transform.position;
        Destroy(NowDestructable, 2f);
    }

}
