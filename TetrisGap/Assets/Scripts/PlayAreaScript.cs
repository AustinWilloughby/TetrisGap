using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaScript : MonoBehaviour
{
    [SerializeField]
    private byte xSize = 6;

    [SerializeField]
    private byte ySize = 9;

    [SerializeField]
    private GameObject toggleableObjectPrefab;
    

    private GameObject anchorObject;
    private GameObject[,] toggleableObjects;


    // Start is called before the first frame update
    void Start()
    {
        anchorObject = gameObject.transform.parent.gameObject;
        toggleableObjects = new GameObject[xSize, ySize];
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
                toggleableObjects[x, y] = Instantiate(toggleableObjectPrefab);
                toggleableObjects[x, y].transform.parent = anchorObject.transform;
                toggleableObjects[x, y].transform.position = new Vector3(xOffset + x, yOffset + y);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
