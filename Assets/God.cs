using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{
    [SerializeField]
    private GameObject[] TeamOneBlobs;
    [SerializeField]
    private GameObject[] TeamTwoBlobs;
    [SerializeField]
    GameObject blobPrefab;

    public God()
    {
        //blob
    }

    // Start is called before the first frame update
    void Start()
    {
        TeamOneBlobs = new GameObject[3];
        TeamTwoBlobs = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            var blobT1 = Instantiate(blobPrefab, new Vector3(transform.position.x + i -1, transform.position.y + 4, transform.position.z), Quaternion.identity);
            blobT1.GetComponent<SpriteRenderer>().color = Color.blue;
            TeamOneBlobs[i] = blobT1;

            var blobT2 = Instantiate(blobPrefab, new Vector3(transform.position.x + i - 1, transform.position.y - 4, transform.position.z), Quaternion.identity);
            blobT2.GetComponent<SpriteRenderer>().color = Color.red;
            TeamTwoBlobs[i] = blobT2;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle()
    {

    }

    public void EndBattle()
    {

    }


    
}
