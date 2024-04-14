using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private HexagonalMap _hexMap;
    [SerializeField] private GridObjectPool _gridObjectPool;
    [SerializeField] private MainGameManager _uiManager;

    [Header("Scriptable objects")]
    [SerializeField] private GridPrefabContainer _gridPrefabContainer;

    public override void InstallBindings()
    {
        Container.BindInstance(_mainCamera).WithId("mainCamera");
        Bind(_hexMap);
        Bind(_gridObjectPool);
        Bind(_gridPrefabContainer);
        Bind(_uiManager);
    }

    private protected void Bind<T>(T instance)
    {
        Container.BindInterfacesAndSelfTo<T>().FromInstance(instance).AsSingle();
    }
}
