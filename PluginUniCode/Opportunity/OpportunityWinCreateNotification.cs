using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace PluginUniCode.Opportunity
{
    public class OpportunityWinCreateNotification : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {

            if (this.Context.MessageName == "Win")
            {
                Entity OpportunityClose = (Entity)this.Context.InputParameters["OpportunityClose"];

                Entity notification = new Entity("leg_notificacaoaocliente");

                EntityReference opportunityReference = (EntityReference)OpportunityClose["opportunityid"];

                Entity opportunity = this.Service.Retrieve("opportunity", opportunityReference.Id, new ColumnSet("parentcontactid"));

                notification["leg_nomedocliente"] = opportunity["parentcontactid"];
                notification["leg_mensagemdanotificacao"] = "Obrigado por se matricular na UniCode!\nSeja bem vindo!\nAtt. Equipe UniCode";
                DateTime date = DateTime.Now;
                notification["leg_datadanotificacao"] = date;

                this.Service.Create(notification);              

            }          

        }
     
    }
}
