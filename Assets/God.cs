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

    private bool _turnDone = false;
    private int _turnDelay = 100;
    private int _count = 0;

    public God()
    {
        //blob
    }

    // Start is called before the first frame update
    void Start()
    {
        StartBattle();
        _turnDone = true;
    }

    private void CreateTeams()
    {
        TeamOneBlobs = new GameObject[3];
        TeamTwoBlobs = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            var blobT1 = Instantiate(blobPrefab, new Vector3(transform.position.x + (i - 1) * 4, transform.position.y + 15, transform.position.z), Quaternion.identity);
            blobT1.GetComponent<SpriteRenderer>().color = Color.blue;
            blobT1.GetComponent<BlobScript>().SetClass(new WarriorClass(), new TestBrain());
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
        if (_count < 100)
        {
            _count++;
            return;
        }
        else
        {
            _count = 0;
        }
        if (_turnDone)
        {
            NextTurn();
        }
    }

    private void NextTurn()
    {
        _turnDone = false;
        var up = _turnOrder.Dequeue();

        if (TeamOneBlobs.Contains(up))
        {
            var temp = TeamOneBlobs.ToList();
            temp.Remove(up);

            up.GetComponent<BlobScript>().TakeTurn(up, temp.ToArray(), TeamTwoBlobs); //TODO fix
        }
        else if (TeamTwoBlobs.Contains(up))
        {
            var temp = TeamTwoBlobs.ToList();
            temp.Remove(up);

            up.GetComponent<BlobScript>().TakeTurn(up, temp.ToArray(), TeamOneBlobs); //TODO fix
        }



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
