using VContainer;

namespace ZE.FeaturedScopes
{
    public class SceneScope : FeaturedScopeBase<ISceneFeatureScopeInstaller, ISceneFeatureInitializer, ISceneFeaturePostInitializer>
    {

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            gameObject.name = nameof(SceneScope);

#if UNITY_EDITOR && SCOPES_LOG
            UnityEngine.Debug.Log("scene scope configured");
#endif
        }

        protected override void FeatureInitialize(ISceneFeatureInitializer initializer, IObjectResolver resolver) =>
            initializer.OnSceneContainerBuilt(resolver);

        protected override void FeaturePostInitialize(ISceneFeaturePostInitializer postInitializer, IObjectResolver resolver) =>
            postInitializer.OnSceneContainerPostBuilt(resolver);

        protected override void Install(ISceneFeatureScopeInstaller installer, IContainerBuilder containerBuilder) =>
            installer.SceneScopeInstall(containerBuilder);
    }
}
