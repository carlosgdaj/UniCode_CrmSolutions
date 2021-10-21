using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginUniCode.Opportunity
{
    public class AlphaNumerico : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity opportunity = (Entity)this.Context.InputParameters["Target"];

            
            opportunity["leg_oportunidadeid"] = generateCode();
            this.Service.Update(opportunity);
            //this.TracingService.Trace(generateCode());
            //string opportunityCode = opportunity.Contains("leg_oportunidadeid") ? opportunity["leg_oportunidadeid"].ToString() : generateCode();
            //opportunity["leg_oportunidadeid"] = generateCode();
            //this.Service.Update(opportunity);

            //if (cnpj != string.Empty)
            //{
            //    QueryExpression queryAccount = new QueryExpression(opportunity.LogicalName);
            //    queryAccount.ColumnSet.AddColumns("fyi_cnpj");
            //    queryAccount.Criteria.AddCondition("fyi_cnpj", ConditionOperator.Equal, cnpj);
            //    EntityCollection accounts = this.Service.RetrieveMultiple(queryAccount);

            //    if (accounts.Entities.Count() > 0)
            //        throw new InvalidPluginExecutionException("Já existe uma conta com este CNPJ no sistema");

            //}
        }
        public string generateCode()
        {
            this.TracingService.Trace("Entrou no generate");

            char[] arrayID = new char[14];
            this.TracingService.Trace("array de " + arrayID.Length + "Posições");
            for (int i = 0; i < arrayID.Length; i++)
            {
                this.TracingService.Trace("Editando posição: " + i);
                if (i == 0)
                {
                    arrayID[i] = 'O';
                }
                else if (i == 1 || i == 2)
                {
                    arrayID[i] = 'P';
                }
                else if (i == 3 || i == 9)
                {
                    arrayID[i] = '-';
                }
                else if(i == 4 || i == 5 || i == 6 || i == 7 || i == 8 || i == 11 || i == 13)
                {
                    this.TracingService.Trace("ENTROU NO 4");

                    arrayID[i] = gerarNumero();
                    this.TracingService.Trace("GEROU DIGITO POSICAO 4");
                }
                
                else if (i == 10 || i == 12)
                {
                    arrayID[i] = gerarMaiscula();
                }
            }
            this.TracingService.Trace("Array feito");
            string id = String.Join("", arrayID);
            this.TracingService.Trace("ID é: " + id);
            return id;

        }
        public string rand(int min, int max)
        {
            Random rand = new Random();
            return (rand.Next(min, max)).ToString();
        }
        public char gerarMaiscula()
        {
            return Convert.ToChar(rand(65, 91));
        }
        public char gerarNumero()
        {
            this.TracingService.Trace("Entrou no GERARNUMERO ");

            char numeroGerado = Convert.ToChar(rand(48, 58));
            this.TracingService.Trace("Numero: "+ numeroGerado);
            return numeroGerado;
        }
    }
}
