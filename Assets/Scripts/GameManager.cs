using System;
using System.Collections.Generic; 
using System.Linq; 
using UniRx; 
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public enum GameState
    {
        Game,
        Lose,
        Win
    }

    [System.Serializable]
    public class Panel
    {
        [HideInInspector]
        public string name;
        public GameState state;
        public GameObject[] gameObjects;
    }

    public GameState state;
    public Panel[] panels;

    private void OnValidate()
    {
        if (panels != null)
        {
            foreach (var panel in panels)
            {
                panel.name = panel.state.ToString();
            }
        }
    }

    public void SetGameState(GameState _state)
    {
        state = _state;

        foreach (var panel in panels)
        {
            if (panel.state != state)
            {
                foreach (var go in panel.gameObjects)
                {
                    if (go != null)
                    {
                        go.SetActive(false);
                    }
                }
            }
        }
        foreach (var panel in panels)
        {
            if (panel.state == state)
            {
                foreach (var go in panel.gameObjects)
                {
                    if (go != null)
                    {
                        go.SetActive(true);
                    }
                }
            }
        }
    }

    public void LoseGame()
    {
        SetGameState(GameState.Lose);
    }

    public void WinGame()
    {
        SetGameState(GameState.Win);
    }

    public void StartGame()
    {
        SetGameState(GameState.Game);
    }
}