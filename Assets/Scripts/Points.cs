using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
   public float points;
   public TextMeshProUGUI textMesh;


    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        points += Time.deltaTime;
        textMesh.text = points.ToString("0");

    }

    public void AddPoints (float entryPoints)
    {
        points += entryPoints;

    }

}
//testing