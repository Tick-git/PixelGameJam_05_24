using UnityEngine;

public class OasisExtension : MonoBehaviour
{
    [SerializeField] int _extensionLayer = 0;

    public int ExtensionLayer { get => _extensionLayer; set => _extensionLayer = value; }
}