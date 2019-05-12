using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectScript : MonoBehaviour
{
    [SerializeField]
    private Color impassableColor;

    [SerializeField]
    private Color passableColor;

    private MeshRenderer mRenderer;
    
    private TargetAreaScript board;

    private byte xPosition;
    private byte yPosition;

    public bool CurrentState { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetupTarget(byte x, byte y, TargetAreaScript _board, bool startingState)
    {
        xPosition = x;
        yPosition = y;

        mRenderer = GetComponent<MeshRenderer>();
        board = _board;

        CurrentState = startingState;

        if (CurrentState)
        {
            mRenderer.material.color = passableColor;
        }
        else
        {
            mRenderer.material.color = impassableColor;
        }
    }
}
