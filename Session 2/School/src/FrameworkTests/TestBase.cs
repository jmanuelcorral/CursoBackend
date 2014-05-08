using AutoPoco;
using AutoPoco.Engine;
using Framework.DI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Microsoft.Practices.Unity;
namespace FrameworkTests
{
    [TestClass]
    public abstract class TestBase
    {
        private static bool _isDependencyFactoryInitialized = false;

 

        [TestInitialize()]
        public void TestUnityBaseInitialize()
        {
            if (!TestBase._isDependencyFactoryInitialized)
            {
                DependencyFactory.SetUnityContainerProviderFactory(Framework.DI.DependencyResolver.GetContainer(ContainerAvailable.Mocks));
                TestBase._isDependencyFactoryInitialized = true;
            }
        }

    }
}