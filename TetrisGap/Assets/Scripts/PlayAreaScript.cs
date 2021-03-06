﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaScript : MonoBehaviour
{
    private byte xSize = 6;
    private byte ySize = 9;

    [SerializeField]
    private GameObject toggleableObjectPrefab;


    private GameObject anchorObject;
    private ToggleableScript[,] toggleableObjects;
    private GameplayManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupBoard(byte _xSize, byte _ySize, GameplayManager _gameManager)
    {
        xSize = _xSize;
        ySize = _ySize;
        gameManager = _gameManager;

        anchorObject = gameObject.transform.parent.gameObject;
        toggleableObjects = new ToggleableScript[xSize, ySize];
        transform.localScale = new Vector3(xSize * 100, ySize * 100, 1);

        //Calculate the offset of the bottom left corner from the center of the playfield
        //First calculate the halfwidth (and make it negative to go down and left)
        //Then use mod to determine if the number is odd or even.
        //If it is even, we need to add 0.5.
        float xOffset = -xSize / 2 + (((xSize + 1)) % 2 / 2.0f);
        float yOffset = -ySize / 2 + (((ySize + 1)) % 2 / 2.0f);

        for (byte x = 0; x < xSize; x++)
        {
            for (byte y = 0; y < ySize; y++)
            {
                toggleableObjects[x, y] = Instantiate(toggleableObjectPrefab).GetComponent<ToggleableScript>();
                toggleableObjects[x, y].SetupToggleable(x, y, this);

                toggleableObjects[x, y].transform.parent = anchorObject.transform;
                toggleableObjects[x, y].transform.localPosition = new Vector3(xOffset + x, yOffset + y);
            }
        }
    }

    public void NotifyManagerOfChange(byte x, byte y, bool newState)
    {
        gameManager.UpdatePlayerToggle(x, y, newState);
    }
}
