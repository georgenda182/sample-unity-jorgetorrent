using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, object> _services;

        public ServiceLocator()
        {
            _services = new Dictionary<Type, object>();
        }

        public void RegisterService<T>(T service)
        {
            var type = typeof(T);
            Assert.IsFalse(_services.ContainsKey(type), 
                $"Service {type} already registered");
        
            _services.Add(type, service);
        }

        public T GetService<T>()
        {
            var type = typeof(T);
            if (!_services.TryGetValue(type, out var service))
            {
                throw new Exception($"Service {type} not found");
            }

            return (T) service;
        }
    }
}