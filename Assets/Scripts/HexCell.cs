using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class HexCell : MonoBehaviour 
{
    public HexTools HexTools;    
    private Vector3 _cordinates;
    public Vector3 Cordinates;
    public bool    Passable = true;
    public int     Cost = 1;
    public string  Terrain = "land";



    
    void Start() 
    {
       Cordinates = HexTools.rect2Cube(gameObject.transform.position);
    }

    public void Update() 
    {

    }

}

