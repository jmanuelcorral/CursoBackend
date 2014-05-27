﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Core.DAL;
using Microsoft.Practices.Unity;

namespace School.Core.DI
{
    public class DependencyProviderFake: DependencyResolver
    {
        internal override IUnityContainer GetContainer()
        {
            IUnityContainer unityContainerReal = new UnityContainer();
            unityContainerReal.RegisterType(typeof(IRepository), typeof(FakeRepository), new InjectionMember[0]);
            return unityContainerReal;
        }
    }
}
