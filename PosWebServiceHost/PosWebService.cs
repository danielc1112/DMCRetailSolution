using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace PosWebServiceHost
{
    public partial class PosWebWindowsService : ServiceBase
    {
        ServiceHost host;
        public PosWebWindowsService()
        {
            InitializeComponent();
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            host = new ServiceHost(typeof(PosWebService.PosWebService));
            host.Open();
        }

        protected override void OnStop()
        {
            if (host != null)
               host.Close();
        }
    }
}
