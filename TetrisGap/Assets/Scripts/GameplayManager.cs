using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using DG.Tweening;

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
    private byte maxLives = 5;

    [SerializeField]
    private PlayAreaScript playerBoard;

    [SerializeField]
    private TargetAreaScript targetBoard;


    private TextMeshPro livesText;
    private TextMeshPro scoreText;
    
    private GameObject topLeftRail;
    private GameObject topRightRail;
    private GameObject bottomLeftRail;
    private GameObject bottomRightRail;

    private float timer;
    private bool runningTimer = true;
    private byte lives;
    private long score;

    private void Awake()
    {
        timer = timePerPuzzle;
        lives = maxLives;

        playerInputToggles = new bool[xSize, ySize];
        targetToggles = new bool[xSize, ySize];

        float xRailOffset = (xSize + 0.1f) / 2.0f;
        float yRailOffset = (ySize + 0.1f) / 2.0f;

        topLeftRail = transform.Find("TopLeftRail").gameObject;
        topRightRail = transform.Find("TopRightRail").gameObject;
        bottomLeftRail = transform.Find("BottomLeftRail").gameObject;
        bottomRightRail = transform.Find("BottomRightRail").gameObject;

        topLeftRail.transform.position = new Vector3(-xRailOffset, yRailOffset, 0);
        topRightRail.transform.position = new Vector3(xRailOffset, yRailOffset, 0);
        bottomLeftRail.transform.position = new Vector3(-xRailOffset, -yRailOffset, 0);
        bottomRightRail.transform.position = new Vector3(xRailOffset, -yRailOffset, 0);


        livesText = topLeftRail.transform.Find("LivesText").GetComponent<TextMeshPro>();
        scoreText = bottomLeftRail.transform.Find("ScoreText").GetComponent<TextMeshPro>();

        livesText.SetText("Lives: " + lives);
        scoreText.SetText("Score: " + score);
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
        if (runningTimer && lives > 0)
        {
            timer -= Time.deltaTime;
            targetBoard.transform.parent.position = new Vector3(0, 0, 50.0f * (timer / timePerPuzzle));

            if (timer < 0)
            {
                CheckSuccess();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                runningTimer = false;
                targetBoard.transform.parent.DOMoveZ(-20, 1.0f).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    CheckSuccess();
                    runningTimer = true;
                });
            }
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

    private void CheckSuccess()
    {
        if (numUnmatched > 0)
        {
            lives--;
            livesText.SetText("Lives: " + lives);
        }
        else
        {
            score++;
            scoreText.SetText("Score: " + score);
        }
        targetBoard.GenerateNewBoard();
        timer = timePerPuzzle;
    }
}
