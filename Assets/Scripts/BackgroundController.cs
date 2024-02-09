using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private RawImage bgImg;
    [SerializeField] private float x, y;

    private void Awake()
    {
        //enable bg 
        bgImg.enabled = true;
    }

    void Update()
    {
        bgImg.uvRect = new Rect(bgImg.uvRect.position + new Vector2(x, y) * Time.deltaTime, bgImg.uvRect.size);
    }
}
