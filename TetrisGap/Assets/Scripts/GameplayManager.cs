using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private byte xSize;

    [SerializeField]
    private byte ySize;

    private bool[,] playerInputToggles;
    private bool[,] targetToggles;
    private byte numUnmatched;

    [SerializeField]
    private float timePerPuzzle = 20.0f;

    [SerializeField]
    private PlayAreaScript playerBoard;

    [SerializeField]
    private TargetAreaScript targetBoard;


    [SerializeField]
    private GameObject topLeftRail;
    [SerializeField]
    private GameObject topRightRail;
    [SerializeField]
    private GameObject bottomLeftRail;
    [SerializeField]
    private GameObject bottomRightRail;

    private float timer;

    private void Awake()
    {
        timer = timePerPuzzle;

        playerInputToggles = new bool[xSize, ySize];
        targetToggles = new bool[xSize, ySize];

        float xRailOffset = (xSize + 0.1f) / 2.0f;
        float yRailOffset = (ySize + 0.1f) / 2.0f;

        topLeftRail.transform.position = new Vector3(-xRailOffset, yRailOffset, 0);
        topRightRail.transform.position = new Vector3(xRailOffset, yRailOffset, 0);
        bottomLeftRail.transform.position = new Vector3(-xRailOffset, -yRailOffset, 0);
        bottomRightRail.transform.position = new Vector3(xRailOffset, -yRailOffset, 0);


        float larger = Mathf.Max(xSize, ySize);
        if (larger > 6)
        {
            Vector3 camPos = Camera.main.transform.position;
            Vector3 camDirect = Camera.main.transform.forward;

            larger = (larger - 6) * 4.5f;
            Vector3 movement = new Vector3(-1.0f * (camDirect.x + larger), camDirect.y, camDirect.z + larger);
            Debug.Log(movement);

            Camera.main.transform.position = camPos - movement;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        targetBoard.SetupBoard(xSize, ySize, this);
        playerBoard.SetupBoard(xSize, ySize, this);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        targetBoard.transform.parent.position = new Vector3(0, 0, 50.0f * (timer / timePerPuzzle));

        if(timer < 0)
        {
            timer = timePerPuzzle;
            // New puzzle / lose life
        }
    }

    public void UpdatePlayerToggle(byte xPos, byte yPos, bool toggleState)
    {
        playerInputToggles[xPos, yPos] = toggleState;

        if(playerInputToggles[xPos, yPos] == targetToggles[xPos, yPos])
        {
            numUnmatched--;
        }
        else
        {
            numUnmatched++;
        }

        if(numUnmatched <= 0)
        {
            targetBoard.GenerateNewBoard();
            timer = timePerPuzzle;
        }

    }

    public void SetTargetToggle(byte xPos, byte yPos, bool targetState)
    {
        targetToggles[xPos, yPos] = targetState;
    }

    public void CalculateUnmatched()
    {
        numUnmatched = 0;
        for(byte x = 0; x < xSize; x++)
        {
            for(byte y = 0; y < ySize; y++)
            {
                if(playerInputToggles[x,y] != targetToggles[x,y])
                {
                    numUnmatched++;
                }
            }
        }
    }
}
