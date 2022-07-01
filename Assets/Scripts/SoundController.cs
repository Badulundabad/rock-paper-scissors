using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip rock;
    [SerializeField] private AudioClip scissors;
    [SerializeField] private AudioClip paper;
    private AudioSource _source;

    private void Start()
    {
        _source = Camera.main.gameObject.GetComponent<AudioSource>();
        Messenger<AgentType>.AddListener(GameEvents.PLAY_SOUND, PlaySound);
    }

    private void OnDestroy()
    {
        Messenger<AgentType>.RemoveListener(GameEvents.PLAY_SOUND, PlaySound);
    }

    private void PlaySound(AgentType type)
    {
        if (type == AgentType.Rock)
            _source.PlayOneShot(rock);
        else if (type == AgentType.Scissors)
            _source.PlayOneShot(scissors);
        else
            _source.PlayOneShot(paper);
    }
}