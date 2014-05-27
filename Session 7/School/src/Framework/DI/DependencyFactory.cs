using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.DI
{
    public static class DependencyFactory
    {
        private static IUnityContainer _unity;

        public static void SetUnityContainerProviderFactory(IUnityContainer container)
        {
            if (DependencyFactory._unity == null)
            {
                DependencyFactory._unity = container;
            }
            else
            {
                // No se puede cambiar el container de dependencias una vez iniciado
                throw new Exception("DependencyFactory cannot be changed once intialized");
            }
        }

        public static T Resolve<T>()
        {
            return DependencyFactory._unity.Resolve<T>();
        }
    }
}
