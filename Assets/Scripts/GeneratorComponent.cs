using UnityEngine;

public class GeneratorComponent: MonoBehaviour
{
    [SerializeField] private string _prefabName; // имя префаба, данного объекта в Resources
    public string prefabName { get { return _prefabName; } }
}