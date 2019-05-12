using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleableScript : MonoBehaviour
{
    [SerializeField]
    private Color defaultColor;

    [SerializeField]
    private Color mouseOverDefaultColor;

    [SerializeField]
    private Color selectedColor;

    [SerializeField]
    private Color mouseOverSelectedColor;

    private MeshRenderer mRenderer;

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
        if (currentlySelected)
        {
            mRenderer.material.color = mouseOverSelectedColor;
        }
        else
        {
            mRenderer.material.color = mouseOverDefaultColor;
        }
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
            mRenderer.material.color = selectedColor;
        }
        else
        {
            mRenderer.material.color = defaultColor;
        }
    }
    public void SetupToggleable(byte x, byte y, PlayAreaScript _board)
    {
        xPosition = x;
        yPosition = y;

        mRenderer = GetComponent<MeshRenderer>();
        mRenderer.material.color = defaultColor;
        board = _board;
        currentlySelected = false;
    }
}
