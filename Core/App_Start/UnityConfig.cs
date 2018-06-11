using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.WebApi;

namespace Core
{
    public static class UnityConfig
    {
        public static void RegisterComponets()
        {
            UsingUnityContainer.Init();

            DependencyRegisterType.Container_Sys(ref UsingUnityContainer._container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(UsingUnityContainer._container));
        }

        public static void RegisterComponentsByWebApi()
        {
            UsingUnityContainer.Init();

            DependencyRegisterType.Container_Sys(ref UsingUnityContainer._container);
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(UsingUnityContainer._container);
        }
    }

    public static class UsingUnityContainer
    {
        public static void Init()
        {
            if (null == _container)
                _container = new UnityContainer();

        }

        public static UnityContainer _container;
    }

}