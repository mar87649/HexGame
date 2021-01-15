using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class UnitProperties : MonoBehaviour
{
    public HexTools HexTools;   
    [SerializeField]
    private int health;
    public int Health{ get; set;}
    [SerializeField]
    private int moveRange;
    public int MoveRange    {
        get
        {
            //Some other code
            return moveRange;
        }
        set
        {
            //Some other code
            moveRange = value;
        }
    }
    [SerializeField]
    private int attack;
    public int Attack{ get; set;}
    public Vector3 CordPosition;


    // Start is called before the first frame update
    void Start()
    {
        CordPosition = HexTools.rect2Cube(gameObject.transform.position);
    }

    // Update is called once per frame
    void Update() 
    {
        CordPosition = HexTools.rect2Cube(gameObject.transform.position);
    }
}
