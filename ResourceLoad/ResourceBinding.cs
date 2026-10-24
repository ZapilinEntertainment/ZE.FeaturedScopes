using System;
using VContainer;

namespace ZE.FeaturedScopes
{
    public class ResourceBinding<TImplementation, TInterface> : IResourceBinder
    where TImplementation : class, TInterface
    {
        private readonly TImplementation _instance;

        public ResourceBinding(TImplementation instance)
        {
            _instance = instance ?? throw new ArgumentNullException(nameof(instance));
        }

        public void Register(IContainerBuilder builder)
        {
            builder.RegisterInstance(_instance)
                   .As<TInterface>()
                   .AsSelf();
        }
    }

    public class ResourceBinding<TImplementation> : IResourceBinder where TImplementation : class
    {
        private readonly TImplementation _instance;

        public ResourceBinding(TImplementation instance)
        {
            _instance = instance ?? throw new ArgumentNullException(nameof(instance));
        }

        public void Register(IContainerBuilder builder)
        {
            builder.RegisterInstance(_instance).AsSelf();
        }
    }
}
