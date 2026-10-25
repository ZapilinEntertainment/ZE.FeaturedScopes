using System.Threading;
using UnityEngine;
using VContainer.Unity;

namespace ZE.FeaturedScopes
{
    // load resources before SessionScope built
    public class SessionAsyncEntryPoint : AsyncScopeEntryPoint<ISessionAsyncResourceLoader>
    {
        private readonly LifetimeScope _currentScope;
        public SessionAsyncEntryPoint(FeaturesModulesList modulesList, LifetimeScope currentScope) : base(modulesList)
        {
            _currentScope = currentScope;
        }

        public override async Awaitable<LifetimeScope> StartAsync(CancellationToken cancellation)
        {
            var binder = await LoadResourcesAsync(cancellation);
            if (binder == null)
                return _currentScope.CreateChild<SessionScope>();
            else
                return _currentScope.CreateChild<SessionScope>(builder => binder.Register(builder));
        }

        protected override Awaitable<IResourceBinder> LoadResourcesAsync(ISessionAsyncResourceLoader resourceLoader) =>
            resourceLoader.LoadSessionResourcesAsync(_currentScope.Container);
    }

}

