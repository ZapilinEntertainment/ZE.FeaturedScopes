using VContainer;

namespace ZE.FeaturedScopes
{
    public class SessionScope : FeaturedScopeBase<ISessionFeatureScopeInstaller, ISessionFeatureInitializer, ISessionFeaturePostInitializer>
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.Register<SceneAsyncEntryPoint>(Lifetime.Transient);

            gameObject.name = nameof(SessionScope);

#if UNITY_EDITOR && SCOPES_LOG
            UnityEngine.Debug.Log("session scope configured");
#endif
        }

        protected override void FeatureInitialize(ISessionFeatureInitializer initializer, IObjectResolver resolver) =>
            initializer.OnSessionContainerBuilt(resolver);

        protected override void FeaturePostInitialize(ISessionFeaturePostInitializer postInitializer, IObjectResolver resolver) =>
            postInitializer.OnSessionContainerPostBuilt(resolver);

        protected override void Install(ISessionFeatureScopeInstaller installer, IContainerBuilder containerBuilder) =>
            installer.SessionScopeInstall(containerBuilder);
    }
}
