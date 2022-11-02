using UnityEngine;

public class PlayerAudioSources : MonoBehaviour
{
    [SerializeField] private AudioSource _drop;
    [SerializeField] private AudioSource _coin;
    [SerializeField] private AudioSource _pickup;
    [SerializeField] private AudioSource _death;
    [SerializeField] private AudioSource _victory;

    public AudioSource Drop => _drop;
    public AudioSource Coin => _coin;
    public AudioSource Pickup => _pickup;
    public AudioSource Death => _death;
    public AudioSource Victory => _victory;
}
