using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Reproduction :  MonoBehaviour
{
    private int _countChilds = 2;
    public void reproduct(GameObject gameObject)
    {
        for(int i = 0; i < _countChilds; i++)
        {
            Instantiate(gameObject);
        }      
    }
}

