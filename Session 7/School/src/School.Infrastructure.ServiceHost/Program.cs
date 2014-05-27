using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace School.Infrastructure.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
             Uri baseAddress = new Uri("http://localhost:6525/SchoolService");

            using (var host = new System.ServiceModel.ServiceHost(typeof(School.Infrastructure.ServiceSchool), baseAddress))
            {
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

                host.Description.Behaviors.Remove<ServiceDebugBehavior>();
                ServiceDebugBehavior sdb = new ServiceDebugBehavior();
                sdb.IncludeExceptionDetailInFaults = true;

                host.Description.Behaviors.Add(smb);
                host.Description.Behaviors.Add(sdb);
                
                
                host.Open();

                Console.WriteLine("The service is ready at: {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service");
                Console.ReadKey();
                host.Close();

            }
        }
    }
}
