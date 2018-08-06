using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private static Level instance;

    public static Level Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Level>();
            }
            return instance;
        }
    }

    public Text timerText;

    [System.Serializable]
    public class BaseLevel
    {
        public int timer;

        public ReactiveProperty<int> currentTimer;

        public Button levelBttn;

        public ReactiveCommand RestartTimer;

        public bool isLocked, isDone;

        public CompositeDisposable disposables;

    }

    public BaseLevel[] levels;

    private void Start()
    {
        SetTimer(0);
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].levelBttn.interactable = !levels[i].isLocked;
            int k = i;
            levels[i].levelBttn.onClick.AddListener(() => { SetTimer(k); GameManager.Instance.StartGame(); });
        }
    }

    public void SetTimer(int index)
    {

        levels[index].disposables = new CompositeDisposable();
        levels[index].currentTimer = new ReactiveProperty<int>(levels[index].timer);

        levels[index].RestartTimer = levels[index].currentTimer.Select(x => x <= 0).ToReactiveCommand();

        levels[index].RestartTimer.Subscribe(_ => { levels[index].currentTimer.Value = levels[index].timer; });

        levels[index].currentTimer
            .ObserveEveryValueChanged(x => x.Value)
            .Subscribe(xs =>
            {
                StartTimer(xs, index);

            });

        levels[index].RestartTimer.BindTo(levels[index].levelBttn);
    }

    public void StartTimer(int time, int index)
    {
        Observable.Timer(TimeSpan.FromSeconds(1))
            .RepeatUntilDisable(this)
            .Subscribe(_ =>
            {
                if (time >= 0)
                {
                    timerText.text = "00 : " + time.ToString("00");
                    time--;
                }
                else
                {
                    GameManager.Instance.WinGame();

                    if (index + 1 < levels.Length - 1)
                    {
                        levels[index + 1].isLocked = false;
                        levels[index + 1].levelBttn.interactable = true;
                    }

                    levels[index].isDone = true;
                    levels[index].disposables.Dispose();
                }
            })
            .AddTo(levels[index].disposables);
    }

}
