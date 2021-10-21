using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace PluginUniCode.Account
{
    class CheckDuplicateCNPJ : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity account = (Entity)this.Context.InputParameters["Target"];

            string cnpj = account.Contains("leg_cnpj") ? account["leg_cnpj"].ToString() : string.Empty;

            if (cnpj != string.Empty)
            {
                QueryExpression queryAccount = new QueryExpression(account.LogicalName);
                queryAccount.ColumnSet.AddColumn("leg_cnpj");
                queryAccount.Criteria.AddCondition("leg_cnpj", ConditionOperator.Equal, cnpj);
                EntityCollection accounts = this.Service.RetrieveMultiple(queryAccount);

                if (accounts.Entities.Count() > 0)
                    throw new InvalidPluginExecutionException("Já existe uma conta com este CNPJ no sistema!");
            }
        }
    }
}
