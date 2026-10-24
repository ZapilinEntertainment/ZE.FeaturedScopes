using System.Threading;
using UnityEngine;
using VContainer.Unity;

namespace ZE.FeaturedScopes
{

    // load resources for child - SceneScope
    public class SceneAsyncEntryPoint : AsyncScopeEntryPoint<ISceneAsyncResourceLoader>
    {
        public LifetimeScope SceneScope { get; private set; }
        private readonly LifetimeScope _currentScope;
        private IResourceBinder _resourceBinder;
        public SceneAsyncEntryPoint(FeaturesModulesList modulesList, LifetimeScope currentScope) : base(modulesList)
        {
            _currentScope = currentScope;
        }

        public override async Awaitable<LifetimeScope> StartAsync(CancellationToken cancellation)
        {
            if (_resourceBinder == null)
                _resourceBinder = await LoadResourcesAsync(cancellation);

            SceneScope = GameObject.FindAnyObjectByType<SceneScope>(FindObjectsInactive.Exclude);  
            if (SceneScope == null)
            {
#if UNITY_EDITOR && SCOPES_LOG
                Debug.LogWarning("scene scope object not found, building anew...");
#endif
                SceneScope = _currentScope.CreateChild<SceneScope>(builder => _resourceBinder.Register(builder));
            }
            else
            {
                SceneScope.Build();
            }
            return SceneScope;
        }

        protected override Awaitable<IResourceBinder> LoadResourcesAsync(ISceneAsyncResourceLoader resourceLoader) =>
            resourceLoader.LoadSceneResourcesAsync(_currentScope.Container);
    }
}

