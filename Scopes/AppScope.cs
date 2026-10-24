using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace ZE.FeaturedScopes
{
    // app scope ->
    // AppAsyncEntryPoint (loading resouces for session scope) ->
    // session scope ->
    // SessionAsyncEntryPoint(loading resources for scene scope) ->
    // scene scope

    public class AppScope : FeaturedScopeBase<IAppFeatureScopeInstaller, IAppFeatureInitializer, IAppFeaturePostInitializer>
    {
        [SerializeField] protected FeaturesModulesList _modulesList;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterInstance<FeaturesModulesList>(_modulesList);
            builder.Register<SessionAsyncEntryPoint>(Lifetime.Transient);

#if UNITY_EDITOR && SCOPES_LOG
            UnityEngine.Debug.Log("app scope configured");
#endif
        }

        protected override void FeatureInitialize(IAppFeatureInitializer initializer, IObjectResolver resolver) =>
            initializer.OnAppContainerBuilt(resolver);

        protected override void FeaturePostInitialize(IAppFeaturePostInitializer postInitializer, IObjectResolver resolver) =>
            postInitializer.OnAppContainerPostBuilt(resolver);

        protected override void Install(IAppFeatureScopeInstaller installer, IContainerBuilder containerBuilder) =>
            installer.AppScopeInstall(containerBuilder);

        protected override IReadOnlyList<IFeatureModule> ResolveModulesList() => _modulesList.Modules;
    }
}
