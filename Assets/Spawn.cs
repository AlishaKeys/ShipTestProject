using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject asteroidPrefab, bullet;
    public Transform parent;
    public ReactiveCollection <AsteroidPresenter> spawn;

    private void Awake()
    {
        spawn = new ReactiveCollection<AsteroidPresenter>();

        Observable.Timer(TimeSpan.FromSeconds(10))
            .RepeatUntilDestroy(this)
            .Subscribe(_ =>
            {
                var positionAsteroid = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
                var asteroid = Instantiate(asteroidPrefab, new Vector2(UnityEngine.Random.Range(-positionAsteroid.x, positionAsteroid.x), positionAsteroid.y), asteroidPrefab.transform.rotation, parent).GetComponent<AsteroidPresenter>();
                Generator.InstantiateItem(asteroid.gameObject.name, asteroid.transform.position, asteroid.transform.rotation);

                spawn.Add(asteroid);

                asteroid.OnDestroyAsObservable()
                .Subscribe(x =>
                {
                    spawn.Remove(asteroid);
                    asteroid.disposables.Dispose();
                });
            })
            .AddTo(this);

    }
}
