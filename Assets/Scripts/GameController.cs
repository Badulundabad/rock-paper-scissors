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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsGameStarted)
        {
            if (IsGameContinuing)
            {
                Pause();
                SendMessage("ShowMenu");
            }
            else
            {
                SendMessage("HideMenu");
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
        SendMessage("HideMenu");
        IsGameContinuing = true;
    }

    public void Restart()
    {
        if (_agents.Count > 0)
        {
            foreach (Agent agent in _agents)
            {
                Destroy(agent.gameObject);
            }
            _agents.Clear();
        }
        StartNew();
        IsGameContinuing = true;
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

    private void CheckForVictory()
    {
        if (_agents.All(a => a.Type == AgentType.Paper || a.Type == AgentType.Stone || a.Type == AgentType.Scissors))
        {
            SendMessage("Victory", _agents.First().Type);
        }
    }
}
