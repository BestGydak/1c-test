using UnityEngine;

public class DebugLol : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private void Start()
    {
        _gameManager.Won.AddListener(() => { Debug.Log("WON"); });
        _gameManager.Lost.AddListener(() => { Debug.Log("LOST"); });
        _gameManager.Started.AddListener(() => { Debug.Log("STARTED"); });
    }
}
