using UnityEngine;
using Zenject;

namespace Trivia
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private UIManager.Settings uiManagerSettings;

        public override void InstallBindings()
        {
            Container.Bind<UIManager>().AsSingle().WithArguments(uiManagerSettings);
        }
    }
}