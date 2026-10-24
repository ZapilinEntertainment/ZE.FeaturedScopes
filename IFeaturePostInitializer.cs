using VContainer;

namespace ZE.FeaturedScopes
{
    public interface IFeaturePostInitializer { }
    public interface IAppFeaturePostInitializer : IFeaturePostInitializer
    {
        void OnAppContainerPostBuilt(IObjectResolver resolver);
    }
    public interface ISessionFeaturePostInitializer : IFeaturePostInitializer
    {
        void OnSessionContainerPostBuilt(IObjectResolver resolver);
    }
    public interface ISceneFeaturePostInitializer : IFeaturePostInitializer
    {
        void OnSceneContainerPostBuilt(IObjectResolver resolver);
    }
}

