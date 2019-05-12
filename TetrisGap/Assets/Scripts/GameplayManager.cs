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
    
    private TextMeshPro timerText;

    [SerializeField]
    private float timePerPuzzle = 20.0f;

    [SerializeField]
    private PlayAreaScript playerBoard;

    [SerializeField]
    private TargetAreaScript targetBoard;

    private float timer;

    private void Awake()
    {
        timerText = transform.Find("TimerText").GetComponent<TextMeshPro>();
        timer = timePerPuzzle;

        playerInputToggles = new bool[xSize, ySize];
        targetToggles = new bool[xSize, ySize];
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
        timerText.SetText(((int)timer).ToString());

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
