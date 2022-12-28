using UnityEngine;
using Zenject;

namespace Trivia
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private UIManager.Settings uiManagerSettings;
        [SerializeField] private QuestionManager.Settings quesitonManagerSettings;

        public override void InstallBindings()
        {
            Container.Bind<UIManager>().AsSingle().WithArguments(uiManagerSettings);
            Container.Bind<QuestionManager>().AsSingle().WithArguments(quesitonManagerSettings).NonLazy();
        }
    }
}