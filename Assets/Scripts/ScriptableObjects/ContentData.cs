using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ContentData : ScriptableObject
{
    public string title;
    [TextArea(10, 100)] public string content;
    public Texture contentImage;
}
