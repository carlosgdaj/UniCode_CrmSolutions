using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginUniCode.Opportunity
{
    public class IntegrateOpportunity : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            IOrganizationService service = GetCrmSerivce();
            Entity opportunityLegado = GetContext(this.Context);
            Entity opportunityDestino = new Entity(opportunityLegado.LogicalName);

            if (Context.MessageName == "Create")
            {               
                Importacao(opportunityDestino, opportunityLegado);              
                service.Create(opportunityDestino);
            }
            else if (this.Context.MessageName == "Update")
            {
                Entity postOpportunity = this.Context.PostEntityImages["PostImage"];

                QueryExpression queryUpdate = new QueryExpression("opportunity");
                queryUpdate.ColumnSet.AddColumns("leg_oportunidadeid", "name", "leg_datadeinicio", "leg_unidade", "leg_statusdavacina", "leg_modalidade", "leg_tipodocurso", "leg_turno", "parentaccountid", "description");
                queryUpdate.Criteria.AddCondition("leg_oportunidadeid", ConditionOperator.Equal, postOpportunity["leg_oportunidadeid"]);
                EntityCollection productsForms = service.RetrieveMultiple(queryUpdate);

                foreach (Entity form in productsForms.Entities)
                {
                    Importacao(form, postOpportunity);
                    service.Update(form);
                }
            }
            else
            {
                if (Context.MessageName == "Delete")
                {
                    Entity preImageOpportunity = this.Context.PreEntityImages["PreImage"];

                    QueryExpression query = new QueryExpression(preImageOpportunity.LogicalName);
                    query.ColumnSet.AddColumn("leg_oportunidadeid");
                    query.Criteria.AddCondition("leg_oportunidadeid", ConditionOperator.Equal, preImageOpportunity["leg_oportunidadeid"]);
                    EntityCollection productsForms = service.RetrieveMultiple(query);

                    foreach (Entity form in productsForms.Entities)
                    {
                        service.Delete(form.LogicalName, form.Id);
                    }

                }
            }

        }

        private void Importacao(Entity opportunityDestino, Entity opportunityLegado)
        {
           
            opportunityDestino["leg_oportunidadeid"] = opportunityLegado["leg_oportunidadeid"];
            opportunityDestino["name"] = opportunityLegado["name"];
            opportunityDestino["leg_datadeinicio"] = opportunityLegado["leg_datadeinicio"];
            opportunityDestino["leg_unidade"] = opportunityLegado["leg_unidade"];            
            opportunityDestino["leg_statusdavacina"] = opportunityLegado["leg_statusdavacina"];           
            opportunityDestino["leg_modalidade"] = opportunityLegado["leg_modalidade"];            
            opportunityDestino["leg_tipodocurso"] = opportunityLegado["leg_tipodocurso"];            
            opportunityDestino["leg_turno"] = opportunityLegado["leg_turno"];                        
            opportunityDestino["description"] = opportunityLegado.Contains("description") ? opportunityLegado["description"] : String.Empty;            
            if (this.Context.MessageName == "Create")
            {
                opportunityDestino["leg_integracao"] = opportunityLegado["leg_integracao"];

            }
            
        }

        private Entity GetContext(IPluginExecutionContext context)
        {
            Entity opportunity = new Entity();
            if (context.MessageName == "Create" || context.MessageName == "Update")
            {

                opportunity = (Entity)context.InputParameters["Target"];
            }
            else
            {
                if (context.MessageName == "Delete")
                {
                    opportunity = (Entity)context.PreEntityImages["PreImage"];
                }
            }
            return opportunity;

        }

        private IOrganizationService GetCrmSerivce()
        {
            string connectionString =
             "AuthType=OAuth;" +
             "Username=tccambiente2@ambiente2.onmicrosoft.com;" +
             "Password=Senhagrupo2;" +
             "Url=https://orgb9bebf23.crm2.dynamics.com/;" +
             "AppId=b314a061-7cc9-4ee7-9621-ced0cfea6087;" +
             "RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            return crmServiceClient.OrganizationWebProxyClient;
        }
    }
}
