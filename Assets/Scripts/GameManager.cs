using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    static private GameManager _instance;
    static public GameManager instance { get { return _instance; } }

    public UnityEvent OnMove;
    public bool isInMove;

    private void Awake()
    {
        _instance = this;

        if(OnMove == null) OnMove = new UnityEvent();
    }

    public void StartMove()
    {
        isInMove = true;
    }

    public void EndMove()
    {
        isInMove = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
