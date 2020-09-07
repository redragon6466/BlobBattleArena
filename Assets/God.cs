using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Data;
using Assets.Services;

namespace Assets
{
    public class God : MonoBehaviour
    {
        const string BlobStatsFormat = "Blob {0}\nClass: {1}\nHp: {2}\nAttack: {3}\nDefense: {4}\nIniative: {5}(+{6})";

        [SerializeField]
        private List<BlobScript> TeamOneBlobs;
        [SerializeField]
        private List<BlobScript> TeamTwoBlobs;
        [SerializeField]
        GameObject blobPrefab;
        [SerializeField]
        GameObject kappa;


        private Text countDown;
        private float _vsTimer = 0.0f;
        private const int VsScreenTime = 11; //Add an extra 1 as the timer imediately starts at 10.9 which rounds down to 10, instead of 9.9 => 9

        Queue<BlobScript> _turnOrder;

        private bool _turnDone = false;
        private bool _battleing = false;
        private int _turnDelay = 5;
        private int _count = 0;
        private const float _vsScale = .25f;
        private const float _battleScale = .38f;

        //private IBrain[] _teamOneBrains = { new GuardianBrain(), new GuardianBrain(), new HealerBrain(), };
        //private IClass[] _teamOneClasses = { new GuardianClass(), new GuardianClass(), new HealerClass(), };
        private IBrain[] _teamOneBrains = { new WarriorBrain() , new WarriorBrain(), new WarriorBrain(), };
        private IClass[] _teamOneClasses = { new WarriorClass() , new WarriorClass(), new WarriorClass(), };
        private IBrain[] _teamTwoBrains = { new WarriorBrain() , new WarriorBrain(), new WarriorBrain(), };
        private IClass[] _teamTwoClasses = { new WarriorClass() , new WarriorClass(), new WarriorClass(15), };

        //ARENA POSITIONS
        private Vector2[] _testOnePos = { new Vector2(9, 0) , new Vector2(10, 0), new Vector2(11, 0), };
        private Vector2[] _testTwoPos = { new Vector2(3, 10), new Vector2(10, 10), new Vector2(17, 10), };

        //LINEUP POSITIONS
        private Vector2[] _blueStartPos = { new Vector2(-3.67f, 2.76f), new Vector2(-3.67f, -.1f), new Vector2(-3.67f, -3.5f), };
        private Vector2[] _redStartPos = { new Vector2(3.55f, 2.91f) , new Vector2(3.55f, -.1f), new Vector2(3.55f, -3.5f), };

        private TwitchChatBot tcb;


        public God()
        {
            //instance = this;
        }

        private static God instance = null;
        private static readonly object padlock = new object();


        public static God Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new God();
                    }
                    return instance;
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            instance = this;
            StartVsScreen();
            _turnDone = true;

            //Task.Run(() => StartDatabaseManager());
            Task.Run(() => StartTwitchBot());
        }

        public void Awake()
        {
            int gameStatusCount = FindObjectsOfType<God>().Length;
            if (gameStatusCount > 1)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject); //when the scene changes don't destroy the game object that owns this
            }
        }

        private void StartDatabaseManager()
        {
           
            if (!DataService.Instance.CheckDatabase())
            {
                if (!DataService.Instance.CreateDatabase())
                {
                    Debug.Log("Failed to create database ");
                    return;
                }
            }

            //DataService.Instance.UpdateBalance("kalloc656", 500);
            Debug.Log(DataService.Instance.GetBalance("kalloc656"));

        }

        private void StartTwitchBot()
        {

            tcb = new TwitchChatBot(
            server: "irc.chat.twitch.tv",
            port: 6667,
            nick: "BlobArenaCoordinator",
            channel: "kalloc656"
            );

            tcb.Start();

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }

            if (!_battleing)
            {
                _vsTimer -= Time.deltaTime;
                if (countDown != null)
                {
                    countDown.text = Math.Floor(_vsTimer).ToString();
                    if (_vsTimer <= 0)
                    {
                        StartBattle();
                    }
                }

                return;
            }
            else
            {
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

                    //Debug.Log("next turn");
                    NextTurn();
                }
            }

        }

        public void OnDestroy()
        {
            if (tcb != null)
            {
                tcb.OnEnd();
            }
        }

        public void EndTurn()
        {
            //Debug.Log("end turn");
            _turnDone = true;
        }



        public void StartVsScreen()
        {
            //Debug.Log("start vs");

            var temp = GameObject.FindWithTag("CountDown");

            //Debug.Log(temp != null);
            if (temp != null)
            {
                //Debug.Log("found text");
                countDown = temp.GetComponent<Text>();
            }



            CreateLineup();
            StartCountdown();
            BettingService.Instance.StartNewRound();
            _battleing = false;
        }

        public void StartBattle()
        {
            SceneManager.LoadScene("Arena");

            Task task = new Task (() => UpdateBalancesOnRoundStart());
            task.Start();
            BuildTurnOrder();
            MoveTeams();
            _battleing = true;

            
        }

        public void EndBattle()
        {
            _battleing = false;

            

            _turnOrder.Clear();
            var temp = TeamOneBlobs.ToList();
            int team = 0;
            foreach (var blob in temp)
            {
                if (blob.GetHealth() > 0)
                {
                    team = 1;
                }
                KillBlob(blob);
            }
            temp = TeamTwoBlobs.ToList();
            foreach (var blob in temp)
            {
                if (blob.GetHealth() > 0)
                {
                    team = 2;
                }
                KillBlob(blob);
            }

            Task task = new Task(() => UpdateBalancesOnRoundEnd(team));
            task.Start();

            SceneManager.LoadScene("Lineup");

            StartCoroutine(DelayOneFrame());

        }

        /// <summary>
        /// Gets a list of all the blobs
        /// </summary>
        /// <returns>a list of all blobs</returns>
        public List<BlobScript> GetAllBlobs()
        {
            var blobs = new List<BlobScript>(TeamOneBlobs);
            blobs.AddRange(TeamTwoBlobs);
            return blobs;
        }

        public void KillBlob(BlobScript blob)
        {
            TeamOneBlobs.Remove(blob);
            TeamTwoBlobs.Remove(blob);

            var temp = _turnOrder.ToList();
            temp.Remove(blob);

            _turnOrder = new Queue<BlobScript>(temp);

            GameObject.Destroy(blob.gameObject);
        }
        IEnumerator DelayOneFrame()
        {

            //returning 0 will make it wait 1 frame
            yield return 0;

            //code goes here
            StartVsScreen();
        }



        private void MoveTeams()
        {
            for (int i = 0; i < 3; i++)
            {
                TeamOneBlobs[i].transform.position = GridService.Instance.ConvertToPoint((int)_testOnePos[i].x, (int)_testOnePos[i].y);
                TeamOneBlobs[i].transform.localScale = new Vector2(_battleScale, _battleScale);
                TeamOneBlobs[i].GetComponentInChildren<Canvas>().enabled = true;
                TeamOneBlobs[i].SetGridLocation((int)_testOnePos[i].x, (int)_testOnePos[i].y);
                TeamTwoBlobs[i].transform.position = GridService.Instance.ConvertToPoint((int)_testTwoPos[i].x, (int)_testTwoPos[i].y);
                TeamTwoBlobs[i].transform.localScale = new Vector2(_battleScale, _battleScale);
                TeamTwoBlobs[i].GetComponentInChildren<Canvas>().enabled = true;
                TeamTwoBlobs[i].SetGridLocation((int)_testTwoPos[i].x, (int)_testTwoPos[i].y);
            }
        }


        private void NextTurn()
        {

            if (TeamOneBlobs.Count == 0 || TeamTwoBlobs.Count == 0)
            {
                EndBattle();
            }

            if (_turnOrder != null && _turnOrder.Count == 0)
            {
                Debug.Log("empty turn order");
                _turnDone = true;
                return;
            }

            _turnDone = false;
            var up = _turnOrder.Dequeue();

            //Debug.Log("begin blob turn");
            if (TeamOneBlobs.Contains(up))
            {
                var temp = TeamOneBlobs.ToList();
                temp.Remove(up);

                //Debug.Log("take turn");
                up.TakeTurn(up, temp, TeamTwoBlobs); //TODO fix maybe?
            }
            else if (TeamTwoBlobs.Contains(up))
            {
                var temp = TeamTwoBlobs.ToList();
                temp.Remove(up);

                //Debug.Log("take turn");
                up.TakeTurn(up, temp, TeamOneBlobs); //TODO fix maybe?
            }



            //Debug.Log("return blob to queue");
            _turnOrder.Enqueue(up);
        }


        private void CreateLineup()
        {
            TeamOneBlobs = new List<BlobScript>();
            TeamTwoBlobs = new List<BlobScript>();

            var texts = FindObjectsOfType(typeof(Text)).ToList().OrderBy(x => ((Text)x).text);
            var stats = texts.ToList();
            stats.RemoveAt(0);

            for (int i = 0; i < 3; i++)
            {
                var blobT1 = Instantiate(blobPrefab, _blueStartPos[i], Quaternion.identity);
                blobT1.GetComponent<SpriteRenderer>().color = Color.blue;
                blobT1.GetComponent<BlobScript>().SetClass(_teamOneClasses[i], _teamOneBrains[i], this);
                blobT1.GetComponentInChildren<Canvas>().enabled = false;
                blobT1.transform.localScale = new Vector2(_vsScale, _vsScale);
                TeamOneBlobs.Add(blobT1.GetComponent<BlobScript>());
                BlobScript blobT1Script = blobT1.GetComponent<BlobScript>();
                ((Text)stats.ElementAt(i)).text = string.Format(BlobStatsFormat, i + 1, blobT1Script.GetClass().ToString(), blobT1Script.GetHealth(), blobT1Script.GetAttack(), blobT1Script.GetDefense(), blobT1Script.GetInitiative(), blobT1Script.GetInitiativBonus());

                var blobT2 = Instantiate(kappa, _redStartPos[i], Quaternion.identity);
                blobT2.GetComponent<SpriteRenderer>().color = Color.red;
                blobT2.GetComponent<BlobScript>().SetClass(_teamTwoClasses[i], _teamTwoBrains[i], this);
                blobT2.GetComponentInChildren<Canvas>().enabled = false;
                blobT2.transform.localScale = new Vector2(_vsScale, _vsScale);
                TeamTwoBlobs.Add(blobT2.GetComponent<BlobScript>());
                BlobScript blobT2Script = blobT2.GetComponent<BlobScript>();
                ((Text)stats.ElementAt(i + 3)).text = string.Format(BlobStatsFormat, i + 3 + 1, blobT2Script.GetClass().ToString(), blobT2Script.GetHealth(), blobT2Script.GetAttack(), blobT2Script.GetDefense(), blobT2Script.GetInitiative(), blobT2Script.GetInitiativBonus());

            }
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
            Debug.Log("Create Turn Order");
            _turnOrder = new Queue<BlobScript>(sorted);
        }


        private void StartCountdown()
        {
            _vsTimer += VsScreenTime;

            //Debug.Log("Start: "+_vsTimer);
        }

        private void UpdateBalancesOnRoundStart()
        {
            var bets = BettingService.Instance.GetTeamOneBets();
            bets.AddRange(BettingService.Instance.GetTeamTwoBets());
            foreach (var item in bets)
            {
                Debug.Log(DataService.Instance.GetBalance(item.ViewerName));
                Debug.Log("-- RF TEMP -- Balance subtracted for bet: " + item.ViewerName + "," + item.Amount);
                DataService.Instance.UpdateBalance(item.ViewerName, item.Amount * -1);
                Debug.Log(DataService.Instance.GetBalance(item.ViewerName));
            }
        }

        private void UpdateBalancesOnRoundEnd(int team)
        {
            Debug.Log(team);

            var winners = BettingService.Instance.PayoutBets(team);
            
            foreach (var item in winners)
            {
                DataService.Instance.UpdateBalance(item.Item1, item.Item2);
            }
        }
    }
}

