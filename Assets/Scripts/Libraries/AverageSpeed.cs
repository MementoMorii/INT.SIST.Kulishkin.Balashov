using Assets.Scripts.Libraries;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AverageSpeed : MonoBehaviour
{
    public Text text;
    private List<Cell> cells; 
    // Start is called before the first frame update
    void Start()
    {
        cells = new List<Cell>();
        EventBus.OnCellBorn += OnCellBorn;
        EventBus.OnCellDie += OnCellDie;
    }

    private void OnCellDie(object sender, Cell e)
    {
        cells.Remove(e);
        CalculateAverage();
    }

    private void OnCellBorn(object sender, Cell e)
    {
        cells.Add(e);
        CalculateAverage();
    }

    private void CalculateAverage()
    {
        float average = 0f;
        foreach (var cell in cells)
        {
            average += cell.Speed;
        }
        average /= cells.Count;
        text.text = average.ToString();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
