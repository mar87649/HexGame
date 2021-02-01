using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class UnitProps : MonoBehaviour
{
    public HexTools HexTools;   
    [SerializeField]private int health;
    public int Health{ get; set;}
    [SerializeField]private int moveRange;
    public int MoveRange{
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

    [SerializeField]private int attackRange;
    public int AttackRange{
        get
        {
            //Some other code
            return attackRange;
        }
        set
        {
            //Some other code
            attackRange = value;
        }
    }
    [SerializeField]private int attack;
    public int Attack{ get; set;}
    [SerializeField]private float  orientation;
    public float  Orientation    {
        get
        {
            return orientation;
        }
        set
        {
            orientation = value;
            transform.localRotation = Quaternion.Euler(0f, value, 0f);          
        }
    }
    [SerializeField]private List<string> bypassList;
    public List<string> BypassList{get; set;}
    [SerializeField] private GameObject hexCell;
    public GameObject HexCell{
        get{ return hexCell;}
        set{ hexCell = value;}
    }
    [SerializeField]private Vector3 cordPosition;
    public Vector3 CordPosition    {
        get
        {
            return cordPosition;
        }
        private set
        {            
            
        }
    }
    [SerializeField]private string type;
    public string Type{        
        get{ return type;}
        set{ type = value;}
    }
    [SerializeField]private string faction;
    public string Faction{        
        get{ return faction;}
        set{ faction = value;}
    }
        void Start()
    {
    }

    // Update is called once per frame
    void Update() 
    {
        cordPosition = HexTools.rect2Cube(transform.position);

        
    }
}
