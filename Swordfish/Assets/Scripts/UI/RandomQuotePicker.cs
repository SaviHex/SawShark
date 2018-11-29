using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomQuotePicker : MonoBehaviour
{
    public TextAsset dialogSource;
    public TextMeshProUGUI textMesh;
    private RandomQuotes rq;
    
    void Start()
    {
        //textMesh = GetComponent<TextMeshProUGUI>();
        rq = new RandomQuotes(dialogSource);
        textMesh.text = rq.PickRandomQuote();
    }
}
