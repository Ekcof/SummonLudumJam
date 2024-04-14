using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private HexagonalMap _hexMap;
    [SerializeField] private GridObjectPool _gridObjectPool;
    [SerializeField] private MainGameManager _mainGameManager;
    [SerializeField] private CombinationManager _combinationManager;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private AnimalView _animalView;

    [SerializeField] private Bar _bar;

    [Header("Scriptable objects")]
    [SerializeField] private GridPrefabContainer _gridPrefabContainer;
    [SerializeField] private ResultHandler _resultHandler;
    [SerializeField] private SpriteHolder _spriteHolder;

    public override void InstallBindings()
    {
        Container.BindInstance(_mainCamera).WithId("mainCamera");
        Bind(_hexMap);
        Bind(_gridObjectPool);
        Bind(_mainGameManager);
        Bind(_combinationManager);
        Bind(_soundManager);
        Bind(_animalView);

        Bind(_bar);

        Bind(_gridPrefabContainer);
        Bind(_resultHandler);
        Bind(_spriteHolder);
    }

    private protected void Bind<T>(T instance)
    {
        Container.BindInterfacesAndSelfTo<T>().FromInstance(instance).AsSingle();
    }
}
