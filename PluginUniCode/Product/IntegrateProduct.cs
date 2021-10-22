using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginUniCode.Product
{
    public class IntegrateProduct : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            IOrganizationService service = GetCrmService();
            Entity productLegado = GetContext(this.Context);
            Entity productDestino = new Entity(productLegado.LogicalName);
           
            if (this.Context.MessageName == "Create")
            {
                Importação(productLegado, productDestino);
                service.Create(productDestino);
            }
            else if (this.Context.MessageName == "Update")
            {
                Entity postProduct = this.Context.PostEntityImages["PostImageUpdate"];

                QueryExpression queryUpdate = new QueryExpression("product");                
                queryUpdate.ColumnSet.AddColumns("productnumber", "name", "leg_tipodegraduacao","description","leg_tempodecurso","currentcost","defaultuomscheduleid","defaultuomid","quantitydecimal");
                queryUpdate.Criteria.AddCondition("productnumber", ConditionOperator.Equal, postProduct["productnumber"]);
                EntityCollection productsForms = service.RetrieveMultiple(queryUpdate);
                this.TracingService.Trace("Depois da query");

                foreach (Entity form in productsForms.Entities)
                {
                    Importação(postProduct, form);                    
                    service.Update(form);
                }
            }
            else
            {
                if (this.Context.MessageName == "Delete")
                {
                    Entity preProduct = this.Context.PreEntityImages["PreImage"];

                    QueryExpression query = new QueryExpression(preProduct.LogicalName);
                    query.ColumnSet.AddColumn("productnumber");
                    query.Criteria.AddCondition("productnumber", ConditionOperator.Equal, preProduct["productnumber"]);
                    EntityCollection productsForms = service.RetrieveMultiple(query);

                    foreach (Entity form in productsForms.Entities)
                    {
                        service.Delete(form.LogicalName, form.Id);
                    }
                }
            }
        }
        private static void Importação(Entity productLegado, Entity productDestino)
        {
            productDestino["name"] = productLegado["name"];
            productDestino["productnumber"] = productLegado["productnumber"];
            productDestino["leg_tipodegraduacao"] = productLegado["leg_tipodegraduacao"];
            productDestino["description"] = productLegado.Contains("description") ? productLegado["description"] : String.Empty;
            productDestino["leg_tempodecurso"] = productLegado.Contains("leg_tempodecurso") ? productLegado["leg_tempodecurso"] : null;
            productDestino["currentcost"] = productLegado.Contains("currentcost") ? productLegado["currentcost"] : new Money() { Value = new decimal(0) };
            productDestino["defaultuomscheduleid"] = productLegado["defaultuomscheduleid"];
            productDestino["defaultuomid"] = productLegado["defaultuomid"];
            productDestino["quantitydecimal"] = productLegado["quantitydecimal"];
        }
        private Entity GetContext(IPluginExecutionContext context)
        {
            Entity product = new Entity();
            if (context.MessageName == "Create" || context.MessageName == "Update")
            {
                this.TracingService.Trace("Entrou do If Update");

                product = (Entity)context.InputParameters["Target"];
            }
            else
            {
                if (context.MessageName == "Delete")
                {
                    product = (Entity)context.PreEntityImages["PreImage"];
                }
            }
            return product;
        }
        public static IOrganizationService GetCrmService()
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