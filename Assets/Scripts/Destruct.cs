using UnityEngine;

public class Destruct : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float torque;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-30,10)*6f, Random.Range(-15,20)*6f), ForceMode2D.Impulse);
        rb.AddTorque(torque);
    }

}