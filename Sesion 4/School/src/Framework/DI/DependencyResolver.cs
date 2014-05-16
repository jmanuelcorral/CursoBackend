using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DI
{

    public enum ContainerAvailable
    {
        Real,
        Mocks
    }

    public abstract class DependencyResolver
    {
            private static DependencyProviderDAL ContainerProvider = new DependencyProviderDAL();
            private static DependencyProviderFake ContainerProviderFake = new DependencyProviderFake();

            public static IUnityContainer GetContainer(ContainerAvailable containerSelected)
            {
                IUnityContainer result = null;

                switch (containerSelected)
                {
                    case ContainerAvailable.Real:
                        result = DependencyResolver.ContainerProvider.GetContainer();
                        break;
                    case ContainerAvailable.Mocks:
                        result = DependencyResolver.ContainerProviderFake.GetContainer();
                        break;
                    default:
                        throw new Exception("IUnityContainer does not exist in the list of available providers");
                }


                return result;
            }

            internal abstract IUnityContainer GetContainer();
    }
}
