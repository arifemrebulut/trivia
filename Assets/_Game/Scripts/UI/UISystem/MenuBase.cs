using UnityEngine;
using Zenject;

namespace Trivia
{
    public abstract class MenuBase : MonoBehaviour
    {
        [Inject]
        protected UIManager uiManager;

        public virtual void Show() => gameObject.SetActive(true);

        public virtual void Hide() => gameObject.SetActive(false);
    }
}