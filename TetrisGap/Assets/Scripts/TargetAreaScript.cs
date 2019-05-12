using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAreaScript : MonoBehaviour
{
    private byte xSize = 6;
    private byte ySize = 9;

    [SerializeField]
    private GameObject targetObjectPrefab;
    [SerializeField]
    private GameObject topBorder;
    [SerializeField]
    private GameObject rightBorder;
    [SerializeField]
    private GameObject leftBorder;
    [SerializeField]
    private GameObject bottomBorder;

    private GameObject anchorObject;
    private TargetObjectScript[,] targetObjects;
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

        topBorder.transform.localPosition = new Vector3(0, (ySize + 0.5f) / 2.0f, 0);
        rightBorder.transform.localPosition = new Vector3((xSize + 0.5f) / 2.0f, 0, 0);
        leftBorder.transform.localPosition = new Vector3(-(xSize + 0.5f) / 2.0f, 0, 0);
        bottomBorder.transform.localPosition = new Vector3(0, -(ySize + 0.5f) / 2.0f, 0);

        topBorder.transform.localScale = new Vector3(xSize + 1, 0.5f, 1);
        rightBorder.transform.localScale = new Vector3(0.5f, ySize, 1);
        leftBorder.transform.localScale = new Vector3(0.5f, ySize, 1);
        bottomBorder.transform.localScale = new Vector3(xSize + 1, 0.5f, 1);


        gameManager = _gameManager;

        anchorObject = gameObject.transform.parent.gameObject;
        targetObjects = new TargetObjectScript[xSize, ySize];
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
                targetObjects[x, y] = Instantiate(targetObjectPrefab).GetComponent<TargetObjectScript>();

                targetObjects[x, y].transform.parent = anchorObject.transform;
                targetObjects[x, y].transform.localPosition = new Vector3(xOffset + x, yOffset + y);

                targetObjects[x, y].SetupTarget(x, y, this, Random.Range(0, 2) == 1);

                gameManager.SetTargetToggle(x, y, targetObjects[x, y].CurrentState);
            }
        }

        gameManager.CalculateUnmatched();
    }

    public void GenerateNewBoard()
    {
        for (byte x = 0; x < xSize; x++)
        {
            for (byte y = 0; y < ySize; y++)
            {
                targetObjects[x, y].SetupTarget(x, y, this, Random.Range(0, 2) == 1);
                gameManager.SetTargetToggle(x, y, targetObjects[x, y].CurrentState);
            }
        }
        gameManager.CalculateUnmatched();
    }
}
