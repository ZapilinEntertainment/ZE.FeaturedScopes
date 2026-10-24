using System;
using VContainer;

namespace ZE.FeaturedScopes
{
    public class KeyedResourceBinding<TImplementation, TKey> : IResourceBinder where TImplementation : class
    {
        private readonly TImplementation _instance;
        private readonly TKey _key;

        public KeyedResourceBinding(TImplementation instance, TKey key)
        {
            _instance = instance ?? throw new ArgumentNullException(nameof(instance));
            _key = key ?? throw new ArgumentNullException(nameof(_key));
        }

        public void Register(IContainerBuilder builder)
        {
            builder.RegisterInstance(_instance).Keyed(_key).AsSelf();
        }
    }
}
