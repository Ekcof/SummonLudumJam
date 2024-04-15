using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SO Installer", menuName = "Installers/SO Installer")]
public class SOInstaller : ScriptableObjectInstaller
{
    [SerializeField] private GridPrefabContainer _gridPrefabContainer;
    [SerializeField] private ResultHandler _resultHandler;
    [SerializeField] private SpriteHolder _heads;
    [SerializeField] private SpriteHolder _bodies;
    [SerializeField] private SoundHolder _soundHolder;

    public override void InstallBindings()
    {
        Bind(_gridPrefabContainer);
        Bind(_resultHandler);
        Container.BindInstance(_heads).WithId("heads");
        Container.BindInstance(_bodies).WithId("bodies");
        Bind(_soundHolder);
    }

    private void Bind<T>(T instance) where T : ScriptableObject
    {
        Container.BindInstance(instance);
    }
}
