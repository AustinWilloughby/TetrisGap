using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectScript : MonoBehaviour
{
    [SerializeField]
    private Color impassableColor;

    [SerializeField]
    private Color passableColor;

    private SpriteRenderer sRenderer;
    
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

        sRenderer = GetComponent<SpriteRenderer>();
        board = _board;

        CurrentState = startingState;

        if (CurrentState)
        {
            sRenderer.color = passableColor;
        }
        else
        {
            sRenderer.color = impassableColor;
        }
    }
}
