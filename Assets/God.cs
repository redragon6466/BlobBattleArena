using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class God : MonoBehaviour
{
    [SerializeField]
    private List<BlobScript> TeamOneBlobs;
    [SerializeField]
    private List<BlobScript> TeamTwoBlobs;
    [SerializeField]
    GameObject blobPrefab;

    Queue<BlobScript> _turnOrder;

    private bool _turnDone = false;
    private bool _battleing = false;
    private int _turnDelay = 10;
    private int _count = 0;

    private IBrain[] _teamOneBrains = { new GuardianBrain(), new GuardianBrain(), new TestBrain(), };
    private Vector2[] _testOnePos = { new Vector2(5,15), new Vector2(0, 15), new Vector2(-5, 15), };
    private Vector2[] _testTwoPos = { new Vector2(-17, -5), new Vector2(-18, 4), new Vector2(-18, -10), };

    public God()
    {
        //blob
    }

    public void KillBlob(BlobScript blob)
    {
        TeamOneBlobs.Remove(blob);
        TeamTwoBlobs.Remove(blob);

        GameObject.Destroy(blob.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartBattle();
        _turnDone = true;
    }

    private void CreateTeams()
    {
        TeamOneBlobs = new List<BlobScript>();
        TeamTwoBlobs = new List<BlobScript>();
        for (int i = 0; i < 3; i++)
        {
            //var blobT1 = Instantiate(blobPrefab, new Vector3(transform.position.x + (i - 1) * 4, transform.position.y + 15, transform.position.z), Quaternion.identity);
            var blobT1 = Instantiate(blobPrefab, _testOnePos[i], Quaternion.identity);
            blobT1.GetComponent<SpriteRenderer>().color = Color.blue;
            blobT1.GetComponent<BlobScript>().SetClass(new WarriorClass(), _teamOneBrains[i], this);
            TeamOneBlobs.Add(blobT1.GetComponent<BlobScript>());

            var blobT2 = Instantiate(blobPrefab, _testTwoPos[i], Quaternion.identity);
            blobT2.GetComponent<SpriteRenderer>().color = Color.red;
            blobT2.GetComponent<BlobScript>().SetClass(new WarriorClass(), new WarriorBrain(), this);
            TeamTwoBlobs.Add(blobT2.GetComponent<BlobScript>());

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_battleing)
        {
            return;
        }
        if (_count < _turnDelay)
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

        if (TeamOneBlobs.Count == 0 || TeamTwoBlobs.Count == 0)
        {
            EndBattle();
        }

        _turnDone = false;
        var up = _turnOrder.Dequeue();

        if (TeamOneBlobs.Contains(up))
        {
            var temp = TeamOneBlobs.ToList();
            temp.Remove(up);

            up.TakeTurn(up, temp, TeamTwoBlobs); //TODO fix maybe?
        }
        else if (TeamTwoBlobs.Contains(up))
        {
            var temp = TeamTwoBlobs.ToList();
            temp.Remove(up);

            up.TakeTurn(up, temp, TeamOneBlobs); //TODO fix maybe?
        }



        _turnOrder.Enqueue(up);
        _turnDone = true;
    }

    public void StartBattle()
    {
        CreateTeams();
        BuildTurnOrder();
        _battleing = true;
    }

    public void EndBattle()
    {
        _battleing = false;
        Debug.Log("Someone Won!");
    }

    private void BuildTurnOrder()
    {
        var turnList = new List<BlobScript>();
        foreach (var obj in TeamOneBlobs)
        {
            turnList.Add(obj);
        }
        foreach (var obj in TeamTwoBlobs)
        {
            turnList.Add(obj);
        }
        List<BlobScript> sorted = turnList.OrderByDescending(x => x.GetInitiative()).ToList();

        _turnOrder = new Queue<BlobScript>(sorted);
    }


    
}
