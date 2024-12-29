﻿using CodeBase.Infrastructure.State;
using Zenject;

namespace CodeBase.Infrastructure.States.Factory
{
    public class StateFactory : IStateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container)
        {
            _container = container;
        }
        
        public T GetState<T>() where T: class, IExitableState =>
        _container.Resolve<T>();
    }
}