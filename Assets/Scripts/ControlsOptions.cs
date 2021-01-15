using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ControlsOptions : MonoBehaviour
{
    [SerializeField]
    private KeyCode _moveCamUp;
     public KeyCode MoveCamUp{get;set;}

    [SerializeField]
    private KeyCode _moveCamDown;
    public KeyCode MoveCamDown{get;set;}

    [SerializeField]
    private KeyCode _moveCamLeft;
    public KeyCode MoveCamLeft{get;set;}

    [SerializeField]
    private KeyCode _moveCamRight;
    public KeyCode MoveCamRight{get;set;}
    [SerializeField] 
    private KeyCode _rotateClockWise;
    public KeyCode RotateClockWise{get;set;}
    [SerializeField] 
    private KeyCode _rotateCounterClockWise;
    public KeyCode RotateCounterClockWise{get;set;}

    [SerializeField] 
    private float _zoomCamIn;
    public float ZoomCamIn{get;set;}

    [SerializeField]
    private float _zoomCamOut;
    public float ZoomCamOut{get;set;}
    // Start is called before the first frame update
    void Start()
    {
        MoveCamUp = KeyCode.W;
        MoveCamDown = KeyCode.S;
        MoveCamLeft = KeyCode.A;
        MoveCamRight = KeyCode.D;
        RotateClockWise = KeyCode.E;
        RotateCounterClockWise = KeyCode.Q;
        ZoomCamIn = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamOut = Input.GetAxis("Mouse ScrollWheel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}