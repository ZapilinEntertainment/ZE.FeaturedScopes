using VContainer;

namespace ZE.FeaturedScopes
{
    public interface IFeatureInitializer { }

    public interface IAppFeatureInitializer : IFeatureInitializer
    {
        void OnAppContainerBuilt(IObjectResolver resolver);
    }

    public interface ISessionFeatureInitializer : IFeatureInitializer
    {
        void OnSessionContainerBuilt(IObjectResolver resolver);
    }

    public interface ISceneFeatureInitializer : IFeatureInitializer
    {
        void OnSceneContainerBuilt(IObjectResolver resolver);
    }
}

