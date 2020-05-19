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

namespace Assets
{
    public class God : MonoBehaviour
    {
        const string BlobStatsFormat = "Blob {0}\nHp: {1}\nAttack: {2}\nDefense: {3}";

        [SerializeField]
        private List<BlobScript> TeamOneBlobs;
        [SerializeField]
        private List<BlobScript> TeamTwoBlobs;
        [SerializeField]
        GameObject blobPrefab;


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

        private IBrain[] _teamOneBrains = { new GuardianBrain(), new GuardianBrain(), new HealerBrain(), };
        private IClass[] _teamOneClasses = { new GuardianClass(), new GuardianClass(), new HealerClass(), };
        private IBrain[] _teamTwoBrains = { new WarriorBrain(), new WarriorBrain(), new WarriorBrain(), };
        private IClass[] _teamTwoClasses = { new WarriorClass(), new WarriorClass(), new WarriorClass(), };
        private Vector2[] _testOnePos = { new Vector2(5, 14), new Vector2(0, 14), new Vector2(-5, 14), };
        private Vector2[] _testTwoPos = { new Vector2(5, -14), new Vector2(0, -14), new Vector2(-5, -14), };

        private Vector2[] _blueStartPos = { new Vector2(-3.67f, 2.76f), new Vector2(-3.67f, -.1f), new Vector2(-3.67f, -3.5f), };
        private Vector2[] _redStartPos = { new Vector2(3.55f, 2.91f), new Vector2(3.55f, -.1f), new Vector2(3.55f, -3.5f), };

        private TwitchChatBot tcb;


        public God()
        {
            //blob
        }

        // Start is called before the first frame update
        void Start()
        {
            StartVsScreen();
            _turnDone = true;

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

        private void StartTwitchBot()
        {

            tcb = new TwitchChatBot(
            server: "irc.chat.twitch.tv",
            port: 6667,
            nick: "BlobArenaCoordinator",
            channel: "kalloc656"
            );

            //tcb.Start();

        }

        // Update is called once per frame
        void Update()
        {
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
            //tcb.OnEnd();
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

            _battleing = false;
        }

        public void StartBattle()
        {
            SceneManager.LoadScene("Arena");

            BuildTurnOrder();
            MoveTeams();
            _battleing = true;
        }

        public void EndBattle()
        {
            _battleing = false;

            _turnOrder.Clear();
            var temp = TeamOneBlobs.ToList();

            foreach (var blob in temp)
            {
                KillBlob(blob);
            }
            temp = TeamTwoBlobs.ToList();
            foreach (var blob in temp)
            {
                KillBlob(blob);
            }


            SceneManager.LoadScene("Lineup");

            StartCoroutine(DelayOneFrame());

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
                TeamOneBlobs[i].transform.position = _testOnePos[i];
                TeamOneBlobs[i].transform.localScale = new Vector2(_battleScale, _battleScale);
                TeamOneBlobs[i].GetComponentInChildren<Canvas>().enabled = true;
                TeamTwoBlobs[i].transform.position = _testTwoPos[i];
                TeamTwoBlobs[i].transform.localScale = new Vector2(_battleScale, _battleScale);
                TeamTwoBlobs[i].GetComponentInChildren<Canvas>().enabled = true;
            }
        }


        private void NextTurn()
        {

            if (TeamOneBlobs.Count == 0 || TeamTwoBlobs.Count == 0)
            {
                EndBattle();
            }

            if (_turnOrder.Count == 0)
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
                ((Text)stats.ElementAt(i)).text = string.Format(BlobStatsFormat, i + 1, blobT1Script.GetHealth(), blobT1Script.GetAttack(), blobT1Script.GetDefense());

                var blobT2 = Instantiate(blobPrefab, _redStartPos[i], Quaternion.identity);
                blobT2.GetComponent<SpriteRenderer>().color = Color.red;
                blobT2.GetComponent<BlobScript>().SetClass(_teamTwoClasses[i], _teamTwoBrains[i], this);
                blobT2.GetComponentInChildren<Canvas>().enabled = false;
                blobT2.transform.localScale = new Vector2(_vsScale, _vsScale);
                TeamTwoBlobs.Add(blobT2.GetComponent<BlobScript>());
                BlobScript blobT2Script = blobT2.GetComponent<BlobScript>();
                ((Text)stats.ElementAt(i + 3)).text = string.Format(BlobStatsFormat, i + 3 + 1, blobT2Script.GetHealth(), blobT2Script.GetAttack(), blobT2Script.GetDefense());

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

            _turnOrder = new Queue<BlobScript>(sorted);
        }


        private void StartCountdown()
        {
            _vsTimer += VsScreenTime;

            //Debug.Log("Start: "+_vsTimer);
        }
    }
}

