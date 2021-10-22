using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
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
                this.TracingService.Trace("Entrou do If Update");
                QueryExpression query = new QueryExpression(productLegado.LogicalName);
                query.ColumnSet.AddColumn("productnumber");
                query.Criteria.AddCondition("productnumber", ConditionOperator.Equal, productLegado["productnumber"]);
                EntityCollection productsForms = service.RetrieveMultiple(query);
                this.TracingService.Trace("Passou da query");
                foreach (Entity form in productsForms.Entities)
                {

                    this.TracingService.Trace("ID DO FORM" + form.Id);
                    //productDestino.Id = new Guid(form.Id.ToString());
                    //productDestino["name"] = productLegado["name"];
                    ////Importação(productLegado, productDestino);
                    //service.Update(productDestino);
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

                    foreach(Entity form in productsForms.Entities)
                    {
                        service.Delete(form.LogicalName, form.Id);
                    }
                    
                }
                
            }



        }

        private static void Importação(Entity productLegado, Entity productDestino)
        {
            productDestino["productnumber"] = productLegado["productnumber"];
            productDestino["name"] = productLegado["name"];
          //productDestino["parentproductid"] = productLegado["parentproductid"];
            productDestino["leg_tipodegraduacao"] = productLegado["leg_tipodegraduacao"];
            //productDestino["description"] = productLegado["description"];
           // productDestino["leg_tempodecurso"] = productLegado["leg_tempodecurso"];
            //productDestino["currentcost"] = productLegado["currentcost"];
            productDestino["defaultuomscheduleid"] = productLegado["defaultuomscheduleid"];
            productDestino["defaultuomid"] = productLegado["defaultuomid"];
           // productDestino["pricelevelid"] = productLegado["pricelevelid"];
            productDestino["quantitydecimal"] = productLegado["quantitydecimal"];
        }

        private Entity GetContext(IPluginExecutionContext context)
        {
            Entity product = new Entity();
            if(context.MessageName == "Create" || context.MessageName == "Update")
            {
                this.TracingService.Trace("Entrou do If Update");

                product = (Entity)context.InputParameters["Target"];
            }
            else
            {
                if(context.MessageName == "Delete")
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