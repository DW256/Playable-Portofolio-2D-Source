using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentReader : MonoBehaviour
{
    public ContentData contentData;

    public TextMeshProUGUI title;
    public TextMeshProUGUI content;
    public RectTransform contentRectTransform;
    public RawImage contentImage;

    private void OnEnable()
    {
        //Pause
        Time.timeScale = 0f;

        //Baca value SO
        title.text = contentData.title;
        content.text = contentData.content;

        if (contentData.contentImage != null) //Ada Content Image Data
        {
            contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, 230);
            contentImage.gameObject.SetActive(true);
            contentImage.texture = (Texture)contentData.contentImage;
        }
        else
        {
            contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, 570);
            contentImage.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        //Unpause
        Time.timeScale = 1f;
    }

}
