using UnityEngine;

public class Agent : MonoBehaviour
{
    private float _speed = 2f;
    private Vector3 _direction = Vector3.zero;
    [SerializeField] private AgentType agentType;

    public AgentType AgentType { get => agentType; }


    private void Start()
    {
        ChangeDirection();
    }

    void Update()
    {
        if (Controllers.Game.IsGameContinuing)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Agent agent = collision.gameObject.GetComponent<Agent>();
        if (agent != null)
        {
            ChangeDirection();
            if (agent.AgentType != this.AgentType)
            {
                Fight(agent);
                Messenger<AgentType>.Broadcast(GameEvents.CHECK_FOR_WINNER, agent.AgentType);
            }
        }
        else
        {
            _direction *= -1;
        }
    }

    private void Fight(Agent enemy)
    {
        bool lose = (this.AgentType == AgentType.Rock && enemy.AgentType == AgentType.Paper) ||
                    (this.AgentType == AgentType.Paper && enemy.AgentType == AgentType.Scissors) ||
                    (this.AgentType == AgentType.Scissors && enemy.AgentType == AgentType.Rock);
        if (lose)
        {
            this.agentType = enemy.AgentType;
            SpriteRenderer enemyRenderer = enemy.gameObject.GetComponent<SpriteRenderer>();
            gameObject.GetComponent<SpriteRenderer>().sprite = enemyRenderer.sprite;
            Messenger<AgentType>.Broadcast(GameEvents.PLAY_SOUND, this.agentType);
        }
    }

    private void ChangeDirection()
    {
        _direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    }
}

public enum AgentType
{
    Rock,
    Scissors,
    Paper
}
