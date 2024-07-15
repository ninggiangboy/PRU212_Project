using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;
    private GamePlayScript _gamePlay;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _gamePlay = FindObjectOfType<GamePlayScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetBool("IsOpen", true);
            _gamePlay.WinGame();
        }
    }
}
