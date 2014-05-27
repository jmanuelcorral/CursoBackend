using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using School.Infrastructure;

namespace School.InfrastructureTest
{
    [TestClass]
    public class ServiceTest
    {

        private Process myProcess;

        [TestInitialize]
        public void Init()
        {
            myProcess = new Process();
            myProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory +
                                           "\\..\\..\\..\\School.Infrastructure.ServiceHost\\bin\\Debug\\School.Infrastructure.ServiceHost.exe";
            //Cargo el Host para que el test se pueda ejecutar.
            myProcess.Start();
        }

        [TestCleanup]
        public void End()
        {
            myProcess.Kill();
        }

        [TestMethod]
        public void GetStudentTest()
        {
            //Implementamos ChannelFactory
            //EndPoint en: http://localhost:6525/SchoolService
            BasicHttpBinding myBinding = new BasicHttpBinding();
            EndpointAddress myEndpoint = new EndpointAddress("http://localhost:6525/SchoolService");
            ChannelFactory<IServiceSchool> myChannelFactory = new ChannelFactory<IServiceSchool>(myBinding, myEndpoint);

            // Create a channel.
            IServiceSchool wcfClient = myChannelFactory.CreateChannel();
            var myStudent = wcfClient.GetStudent(1);
            ((IClientChannel)wcfClient).Close();
            myChannelFactory.Close();
            Assert.IsNotNull(myStudent);
        }
    }
}
