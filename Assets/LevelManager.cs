using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();
            }
            return instance;
        }
    }

    [SerializeField] GameObject prefabAsteroid;
    [SerializeField] Transform parent;
    public GameObject winPanel, losePanel, mapPanel;

    private void Start()
    {
        CreateAsteroid();
    }

    void CreateAsteroid()
    {
        Observable.Timer(TimeSpan.FromSeconds(10))
            .RepeatUntilDestroy(this)
            .Subscribe(_ =>
            {
                var positionAsteroid = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
                GameObject asteroid = Instantiate(prefabAsteroid, new Vector2(UnityEngine.Random.Range(-positionAsteroid.x, positionAsteroid.x), positionAsteroid.y), prefabAsteroid.transform.rotation, parent);
            })
            .AddTo(this);
    }

    public void Win()
    {
        winPanel.SetActive(true);
        Observable.Timer(TimeSpan.FromSeconds(2))
            .Subscribe(_ => mapPanel.SetActive(true));
    }

    public void Lose()
    {
        losePanel.SetActive(true);
    }
}
