using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateWavyLetters : MonoBehaviour
{
    [TextArea(1, 4)]
    public string message;

    public float frequency = 8.0f;
    public float magnitude = 1.5f;
    public float offset = 0.05f;

    public GameObject letterPrefab;

    private float width;
    private int letterCount = 0;    
    
    private void Start()
    {
        width = letterPrefab.GetComponent<RectTransform>().rect.width;

        foreach(char c in message)
        {
            CreateLetter(c + "");
        } 

        enabled = false;
    }
      
    public void CreateLetter(string letter)
    {
        GameObject l = Instantiate(letterPrefab, transform);
        l.name = letter;

        l.transform.position +=  new Vector3(width * letterCount, 0f); // Place it where the parent is + offset.       

        // Setup Text Component   
        Text text = l.GetComponent<Text>();
        text.text = letter;

        // Setup WavyEffect
        WavyEffect effect = l.GetComponent<WavyEffect>();
        effect.frequency = frequency;
        effect.magnitude = magnitude;
        effect.offset = offset * letterCount;

        letterCount += 1;
    }
}
