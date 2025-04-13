using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public List<Cell> columns = new List<Cell>();
    public List<Image> buttons = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Image i in buttons)
        {
            i.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateBoard()
    {
        for (int i = 0; i < columns.Count; i++)
        {
            for (int j = 0; j< columns[i].Count; i++)
            {

            }
        }
    }
}

[System.Serializable]
public class Cell
{
    public List<GameObject> cells = new List<GameObject>();
}