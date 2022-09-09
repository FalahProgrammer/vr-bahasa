using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Repository Skybox", menuName = "Repository/Repository Skybox")]
public class RepositorySkybox : ScriptableObject
{
    [FormerlySerializedAs("skyboxInside")] public Material defaultSkybox;
}
