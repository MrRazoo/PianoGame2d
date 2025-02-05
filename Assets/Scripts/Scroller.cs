using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    private RawImage rawImage;
    public float moveSpeed = 0.1f;
    private Vector2 uvOffset = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        uvOffset.x += moveSpeed * Time.deltaTime;
        rawImage.uvRect = new Rect(uvOffset.x, 0f, rawImage.uvRect.width, rawImage.uvRect.height);
    }
}
