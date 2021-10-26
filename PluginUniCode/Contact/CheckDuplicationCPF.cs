using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;


namespace PluginUniCode.Contact
{
    public class CheckDuplicationCPF : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity account = (Entity)this.Context.InputParameters["Target"];

            string cpf = account.Contains("leg_cpf") ? account["leg_cpf"].ToString() : string.Empty;

            if (cpf != string.Empty)
            {
                QueryExpression queryAccount = new QueryExpression(account.LogicalName);
                queryAccount.ColumnSet.AddColumns("leg_cpf");
                queryAccount.Criteria.AddCondition("leg_cpf", ConditionOperator.Equal, cpf);
                EntityCollection accounts = this.Service.RetrieveMultiple(queryAccount);

                if (accounts.Entities.Count() > 0)
                    throw new InvalidPluginExecutionException("Já existe uma conta com este CPF no sistema!");
            }
        }
    }
}
