using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Generator : MonoBehaviour
{
    // здесь мы указываем объекты, для которых предусмотрена сериализация
    [SerializeField] private GeneratorComponent[] item;
    [SerializeField] private GeneratorComponent player;

    private SceneState state;
    private string dataPath;
    private static Generator _inst;

    void Awake()
    {
        _inst = this;

        dataPath = Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + ".dat";

        if (File.Exists(dataPath))
        {
            state = Serializator.LoadBinary(dataPath);
            Generate();
        }
        else SetDefault();
    }

    public static void InstantiateItem(string prefabName, Vector3 position, Quaternion rotation)
    {
        _inst.InstantiateItem_inst(prefabName, position, rotation);
    }

    void InstantiateItem_inst(string prefabName, Vector3 position, Quaternion rotation)
    {
        GameObject obj = Resources.Load<GameObject>(prefabName);

        if (obj != null)
        {
            Instantiate(obj, position, rotation);

            state.AddItem(new BaseData(obj, prefabName, position));
        }
    }

    void Clear()
    {
        for (int i = 0; i < item.Length; i++)
        {
            item[i].gameObject.SetActive(false);
            Destroy(item[i].gameObject);
        }

        player.gameObject.SetActive(false);
        Destroy(player.gameObject);
    }

    void SetDefault()
    {
        state = new SceneState();

        for (int i = 0; i < item.Length; i++)
        {
            if (!string.IsNullOrEmpty(item[i].prefabName))
            {
                state.AddItem(new BaseData(item[i].gameObject, item[i].prefabName, item[i].transform.position));
            }
        }

        if (player != null && !string.IsNullOrEmpty(player.prefabName))
        {
            state.AddItem(new ShipData(player.gameObject, player.prefabName, player.transform.position));
        }
    }

    void Generate()
    {
        Clear();

        foreach (BaseData t in state.itemList)
        {
            if (!string.IsNullOrEmpty(t.Name))
            {
                t.Inst = Instantiate(Resources.Load<GameObject>(t.Name), t.model.position, transform.rotation) as GameObject;
            }
        }
    }

    public void SaveScene()
    {
        state.Update();
        Serializator.SaveBinary(state, dataPath);
    }
}