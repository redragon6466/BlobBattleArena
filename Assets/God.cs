using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class God : MonoBehaviour
{
    [SerializeField]
    private GameObject[] TeamOneBlobs;
    [SerializeField]
    private GameObject[] TeamTwoBlobs;
    [SerializeField]
    GameObject blobPrefab;

    [SerializeField]
    List<GameObject> queueToList;

    Queue<GameObject> _turnOrder;

    private bool _turnDone = true;

    public God()
    {
        //blob
    }

    // Start is called before the first frame update
    void Start()
    {
        StartBattle();
    }

    private void CreateTeams()
    {
        TeamOneBlobs = new GameObject[3];
        TeamTwoBlobs = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            var blobT1 = Instantiate(blobPrefab, new Vector3(transform.position.x + (i - 1) * 4, transform.position.y + 15, transform.position.z), Quaternion.identity);
            blobT1.GetComponent<SpriteRenderer>().color = Color.blue;
            blobT1.GetComponent<BlobScript>().SetClass(new WarriorClass(), new WarriorBrain());
            TeamOneBlobs[i] = blobT1;

            var blobT2 = Instantiate(blobPrefab, new Vector3(transform.position.x + (i - 1) * 4, transform.position.y - 15, transform.position.z), Quaternion.identity);
            blobT2.GetComponent<SpriteRenderer>().color = Color.red;
            blobT2.GetComponent<BlobScript>().SetClass(new WarriorClass(), new WarriorBrain());
            TeamTwoBlobs[i] = blobT2;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_turnDone)
        {
            NextTurn();
        }
    }

    private void NextTurn()
    {
        _turnDone = false;
        var up = _turnOrder.Dequeue();

        up.GetComponent<BlobScript>().TakeTurn()

        _turnOrder.Enqueue(up);
        _turnDone = true;
    }

    public void StartBattle()
    {
        CreateTeams();
        BuildTurnOrder();
    }

    public void EndBattle()
    {

    }

    private void BuildTurnOrder()
    {
        System.Console.WriteLine("Build Turn Order");
        var turnList = new List<GameObject>();
        foreach (var obj in TeamOneBlobs)
        {
            turnList.Add(obj);
        }
        foreach (var obj in TeamTwoBlobs)
        {
            turnList.Add(obj);
        }
        List<GameObject> sorted = turnList.OrderByDescending(x => x.GetComponent<BlobScript>().GetInitiative()).ToList();

        _turnOrder = new Queue<GameObject>(sorted);
        queueToList = _turnOrder.ToList();
    }


    
}
