using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool IsGameContinuing { get; private set; }
    public bool IsGameStarted { get; private set; }
    [SerializeField] private Transform arena;
    [SerializeField] private GameObject stonePrefab;
    [SerializeField] private GameObject scissorsPrefab;
    [SerializeField] private GameObject paperPrefab;
    private List<Agent> _agents = new List<Agent>();

    private void Start()
    {
        Messenger<AgentType>.AddListener(GameEvents.CHECK_FOR_WINNER, CheckForWinner);
    }

    private void OnDestroy()
    {
        Messenger<AgentType>.RemoveListener(GameEvents.CHECK_FOR_WINNER, CheckForWinner);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsGameStarted)
        {
            if (IsGameContinuing)
            {
                Pause();
                Messenger.Broadcast(GameEvents.SHOW_MENU);
            }
            else
            {
                Messenger.Broadcast(GameEvents.HIDE_MENU);
                Play();
            }
        }
    }

    public void Play()
    {
        if (!IsGameStarted)
        {
            StartNew();
        }
        Messenger.Broadcast(GameEvents.HIDE_MENU);
        IsGameContinuing = true;
    }

    public void Restart()
    {
        if (_agents.Count > 0)
        {
            RemoveAgents();
        }
        StartNew();
        IsGameContinuing = true;
    }

    private void RemoveAgents()
    {
        foreach (Agent agent in _agents)
        {
            Destroy(agent.gameObject);
        }
        _agents.Clear();
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void StartNew()
    {
        SpawnAgents(stonePrefab, new Vector3(0, -3, 0), 10);
        SpawnAgents(scissorsPrefab, new Vector3(4, 3, 0), 10);
        SpawnAgents(paperPrefab, new Vector3(-4, 3, 0), 10);
        IsGameStarted = true;
    }

    private void Pause()
    {
        IsGameContinuing = false;
    }

    private void SpawnAgents(GameObject prefab, Vector3 position, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject instance = Instantiate(prefab, arena);
            instance.transform.position = position;
            Agent agent = instance.GetComponent<Agent>();
            _agents.Add(agent);
        }
    }

    private void CheckForWinner(AgentType type)
    {
        if (_agents.All(a => a.AgentType == type))
        {
            IsGameContinuing = false;
            Sprite winnerSprite = _agents.First().gameObject.GetComponent<SpriteRenderer>().sprite;
            Messenger<Sprite>.Broadcast(GameEvents.SHOW_WINNER, winnerSprite);
            RemoveAgents();
        }
    }
}
