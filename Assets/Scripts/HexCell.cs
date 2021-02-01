using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class HexCell : MonoBehaviour 
{
    public HexTools HexTools;  

    [SerializeField]private Vector3 cordinates;
    public Vector3 Cordinates{
        get
        { 
            return cordinates;
        }
        set
        { 
            cordinates = value;
        }
    }
    [SerializeField]private bool   passable;
    public bool    Passable{
        get{ return passable;}
        set{ passable = value;}
    }
    [SerializeField]private int cost;
    public int  Cost{
        get{ return cost;}
        set{ cost = value;}
    }
    [SerializeField]private string terrain;
    public string  Terrain{
        get{ return terrain;}
        set{ terrain = value;}
    }
    [SerializeField]private bool blocked;
    public bool  Blocked{
        get{ return blocked;}
        set{ blocked = value;}
    }
    [SerializeField]private GameObject unit;
    public GameObject Unit{
        get{ return unit;}
        set{ unit = value;}
    }
    [SerializeField]private GameObject obj;
    public GameObject Obj{
        get{ return obj;}
        set{ obj = value;}
    }
    void Start() 
    {
       cordinates = HexTools.rect2Cube(gameObject.transform.position);
    }

    public void Update() 
    {
        
    }

}

