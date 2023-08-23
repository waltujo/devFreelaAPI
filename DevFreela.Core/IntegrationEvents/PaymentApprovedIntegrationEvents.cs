using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.IntegrationEvents
{
    public class PaymentApprovedIntegrationEvents
    {
        public PaymentApprovedIntegrationEvents(int idProject)
        {
            IdProject = idProject;
        }

        public int IdProject { get; set; }
    }
}
