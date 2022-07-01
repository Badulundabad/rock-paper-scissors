using UnityEngine;

public class Agent : MonoBehaviour
{
    private float _speed = 2f;
    private Vector3 _direction = Vector3.zero;
    [SerializeField] private AgentType agentType;
    [SerializeField] private LayerMask mask;

    public AgentType Type { get => agentType; }


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
            if (agent.Type != this.Type)
            {
                Fight(agent);
            }
        }
        else
        {
            _direction *= -1;
        }
    }

    private void Fight(Agent enemy)
    {
        bool lose = (this.Type == AgentType.Stone && enemy.Type == AgentType.Paper) ||
                    (this.Type == AgentType.Paper && enemy.Type == AgentType.Scissors) ||
                    (this.Type == AgentType.Scissors && enemy.Type == AgentType.Stone);
        if (lose)
        {
            this.agentType = enemy.Type;
            SpriteRenderer enemyRenderer = enemy.gameObject.GetComponent<SpriteRenderer>();
            gameObject.GetComponent<SpriteRenderer>().sprite = enemyRenderer.sprite;
        }
    }

    private void ChangeDirection()
    {
        _direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    }
}

public enum AgentType
{
    Stone,
    Scissors,
    Paper
}
