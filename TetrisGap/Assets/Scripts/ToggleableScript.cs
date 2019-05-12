using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleableScript : MonoBehaviour
{
    [SerializeField]
    private Color defaultColor;

    [SerializeField]
    private Color mouseOverColor;

    [SerializeField]
    private Color selectedColor;

    private SpriteRenderer sRenderer;

    private bool currentlySelected = false;
    private byte xPosition;
    private byte yPosition;

    private PlayAreaScript board;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        sRenderer.color = mouseOverColor;
    }

    private void OnMouseExit()
    {
        SetColorFromState();
    }

    private void OnMouseDown()
    {
        currentlySelected = !currentlySelected;
        SetColorFromState();

        board.NotifyManagerOfChange(xPosition, yPosition, currentlySelected);
    }

    void SetColorFromState()
    {
        if (currentlySelected)
        {
            sRenderer.color = selectedColor;
        }
        else
        {
            sRenderer.color = defaultColor;
        }
    }
    public void SetupToggleable(byte x, byte y, PlayAreaScript _board)
    {
        xPosition = x;
        yPosition = y;

        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.color = defaultColor;
        board = _board;
        currentlySelected = false;
    }
}
