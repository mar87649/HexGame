using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ObjectProps : MonoBehaviour
{
    [SerializeField]private string type;
    public string Type{
        get{ return type;}
        set{ type = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
