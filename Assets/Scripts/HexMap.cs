using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    public Dictionary<Vector3, GameObject> HexGrid = new Dictionary<Vector3, GameObject>();
       
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {                     
            HexGrid.Add(child.GetComponent<HexCell>().Cordinates, child.gameObject);
        }
    }

    // Update is called once per frame
    public void Update()
    {

    }
}
