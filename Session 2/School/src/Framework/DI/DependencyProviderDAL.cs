using Framework.DAL;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DI
{
    public class DependencyProviderDAL:DependencyResolver
    {
        internal override IUnityContainer GetContainer()
        {
            IUnityContainer unityContainerReal = new UnityContainer();
            //unityContainerReal.RegisterType(typeof(IRepository), typeof(GenericRepository), new InjectionMember[0]);
            return unityContainerReal;
        }
    }
}
