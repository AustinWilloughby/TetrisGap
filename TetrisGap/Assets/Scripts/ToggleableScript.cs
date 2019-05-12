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

    // Start is called before the first frame update
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.color = defaultColor;
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
        SetColorFromState();
        currentlySelected = !currentlySelected;
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

}
