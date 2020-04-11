using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerControl : MonoBehaviour
{

    public SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        sr.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}
