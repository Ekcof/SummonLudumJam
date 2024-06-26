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
    [SerializeField] private FXManager _fXManager;
    [SerializeField] private UIPanel _uipanel;

    [SerializeField] private Bar _bar;


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
        Bind(_fXManager);
        Bind(_uipanel);
    }

    private protected void Bind<T>(T instance)
    {
        Container.BindInterfacesAndSelfTo<T>().FromInstance(instance).AsSingle();
    }
}
