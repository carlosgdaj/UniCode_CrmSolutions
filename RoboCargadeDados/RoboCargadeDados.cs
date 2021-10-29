﻿using System;
using static RoboCargadeDados.Conexao;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RoboCargadeDados
{
    class RoboCargadeDados
    {
        static private Timer timer = new Timer();

        static void Main(string[] args)
        {

            var serviceproxy = new Conexao().ConexaoRobo();
            var serviceproxyCliente = new ConexaoCliente().ConexaoDestino();

            ProcessorManager();
            EuRoboFinal(serviceproxy, serviceproxyCliente);
            Console.WriteLine("/////////////////////////////////Fim da Importação/////////////////////////////////");
            Console.WriteLine(DateTime.Now);
            Console.ReadKey();

        }
        private static void ProcessorManager()
        {
            AdjustTimer();
            timer.Start();
        }

        private static void OnTimeOut(object source, ElapsedEventArgs e)
        {

            try
            {
                var serviceproxy = new Conexao().ConexaoRobo();
                var serviceproxyCliente = new ConexaoCliente().ConexaoDestino();
                EuRoboFinal(serviceproxy, serviceproxyCliente);
                Console.WriteLine("/////////////////////////////////Fim da Importação/////////////////////////////////");
                Console.WriteLine(DateTime.Now);
                /*Aqui adicione o seu código para executar a procedure*/
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
        }
        private static void AdjustTimer()
        {
            timer.Interval = 30000;
            timer.AutoReset = true;

            timer.Elapsed += OnTimeOut;
        }
        private static void EuRoboFinal(CrmServiceClient serviceProxy, CrmServiceClient serviceProxyCliente)
        {
            RetornarMultiploClienteContaContato(serviceProxy, serviceProxyCliente);
            //RetornarMultiploClienteConcorrentes(serviceProxy, serviceProxyCliente);
            //RetornarMultiploClienteFaturaItens(serviceProxy, serviceProxyCliente);

        }

        static EntityCollection RetornarMultiploClienteContaContato(CrmServiceClient serviceProxy, CrmServiceClient serviceProxyCliente)
        {
            //Query Contact
            QueryExpression queryContato = new QueryExpression("contact");
            queryContato.Criteria.AddCondition("firstname", ConditionOperator.NotNull);
            queryContato.Criteria.AddCondition("lastname", ConditionOperator.NotNull);
            queryContato.Criteria.AddCondition("leg_cpf", ConditionOperator.NotNull);

            queryContato.ColumnSet = new ColumnSet("firstname", "lastname", "leg_cpf", "jobtitle", "parentcustomerid", "emailaddress1", "telephone1", "mobilephone", "fax");
            EntityCollection entityscollection = serviceProxy.RetrieveMultiple(queryContato);

            foreach (var entity in entityscollection.Entities)
            {
                try
                {
                    var contact = new Entity("contact");
                    Guid Registro = new Guid();

                    if (entity.Attributes.Contains("contactid"))
                        contact.Attributes.Add("contactid", entity.Id);

                    if (entity.Attributes.Contains("firstname"))
                        contact.Attributes.Add("firstname", entity.GetAttributeValue<string>("firstname"));


                    if (entity.Attributes.Contains("lastname"))
                        contact.Attributes.Add("lastname", entity.GetAttributeValue<string>("lastname"));

                    if (entity.Attributes.Contains("leg_cpf"))
                        contact.Attributes.Add("leg_cpf", entity.GetAttributeValue<string>("leg_cpf"));

                    if (entity.Attributes.Contains("jobtitle"))
                        contact.Attributes.Add("jobtitle", entity.GetAttributeValue<string>("jobtitle"));

                    if (entity.Attributes.Contains("parentcustomerid"))
                        contact.Attributes.Add("parentcustomerid", entity.GetAttributeValue<EntityReference>("parentcustomerid"));

                    if (entity.Attributes.Contains("emailaddress1"))
                        contact.Attributes.Add("emailaddress1", entity.GetAttributeValue<string>("emailaddress1"));

                    if (entity.Attributes.Contains("telephone1"))
                        contact.Attributes.Add("telephone1", entity.GetAttributeValue<string>("telephone1"));

                    if (entity.Attributes.Contains("mobilephone"))
                        contact.Attributes.Add("mobilephone", entity.GetAttributeValue<string>("mobilephone"));

                    if (entity.Attributes.Contains("fax"))
                        contact.Attributes.Add("fax", entity.GetAttributeValue<string>("fax"));



                    Registro = serviceProxyCliente.Create(contact);

                }
                catch (Exception e)
                {

                    Console.WriteLine("{0} Exception.", e);
                }
            }

            // Query account
            QueryExpression queryAccount = new QueryExpression("account");
            queryAccount.Criteria.AddCondition("name", ConditionOperator.NotNull);
            queryAccount.Criteria.AddCondition("leg_cnpj", ConditionOperator.NotNull);


            queryAccount.ColumnSet = new ColumnSet("name", "address1_postalcode", "leg_cnpj", "leg_niveldocliente", "leg_porte", "telephone1", "fax", "websiteurl", "parentaccountid", "tickersymbol", "customertypecode", "defaultpricelevelid");
            EntityCollection collectionEntity = serviceProxy.RetrieveMultiple(queryAccount);

            foreach (var entity in collectionEntity.Entities)
            {
                try
                {
                    var account = new Entity("account");
                    Guid Registro = new Guid();

                    if (entity.Attributes.Contains("accountid"))
                        account.Attributes.Add("accountid", entity.Id);

                    if (entity.Attributes.Contains("name"))
                        account.Attributes.Add("name", entity.GetAttributeValue<string>("name"));

                    if (entity.Attributes.Contains("address1_postalcode"))
                        account.Attributes.Add("address1_postalcode", entity.GetAttributeValue<string>("address1_postalcode"));

                    if (entity.Attributes.Contains("leg_cnpj"))
                        account.Attributes.Add("leg_cnpj", entity.GetAttributeValue<string>("leg_cnpj"));

                    if (entity.Attributes.Contains("leg_niveldocliente"))
                        account.Attributes.Add("leg_niveldocliente", entity.GetAttributeValue<OptionSetValue>("leg_niveldocliente"));

                    if (entity.Attributes.Contains("leg_porte"))
                        account.Attributes.Add("leg_porte", entity.GetAttributeValue<OptionSetValue>("leg_porte"));

                    if (entity.Attributes.Contains("telephone1"))
                        account.Attributes.Add("telephone1", entity.GetAttributeValue<string>("telephone1"));

                    if (entity.Attributes.Contains("fax"))
                        account.Attributes.Add("fax", entity.GetAttributeValue<string>("fax"));

                    if (entity.Attributes.Contains("websiteurl"))
                        account.Attributes.Add("websiteurl", entity.GetAttributeValue<string>("websiteurl"));

                    if (entity.Attributes.Contains("parentaccountid"))
                        account.Attributes.Add("parentaccountid", entity.GetAttributeValue<EntityReference>("parentaccountid"));

                    if (entity.Attributes.Contains("tickersymbol"))
                        account.Attributes.Add("tickersymbol", entity.GetAttributeValue<string>("tickersymbol"));

                    if (entity.Attributes.Contains("customertypecode"))
                        account.Attributes.Add("customertypecode", entity.GetAttributeValue<string>("customertypecode"));

                    if (entity.Attributes.Contains("defaultpricelevelid"))
                        account.Attributes.Add("defaultpricelevelid", entity.GetAttributeValue<string>("defaultpricelevelid"));

                    Registro = serviceProxyCliente.Create(account);

                }
                catch (Exception e)
                {

                    Console.WriteLine("{0} Exception.", e);
                }
            }

            //Update Account
            foreach (var entity in collectionEntity.Entities)
            {
                try
                {
                    var Registro = serviceProxy.Retrieve("account", entity.Id, new ColumnSet("parentaccountid"));
                    if (entity.Attributes.Contains("parentaccountid"))
                        Registro.Attributes.Add("parentaccountid", entity.GetAttributeValue<EntityReference>("parentaccountid"));
                    serviceProxyCliente.Update(Registro);

                }
                catch (Exception e)
                {

                    Console.WriteLine("{0} Exception ", e);
                }
            }

            //Update Contact
            foreach (var entity in entityscollection.Entities)
            {
                try
                {
                    var Registro = serviceProxy.Retrieve("contact", entity.Id, new ColumnSet("parentcustomerid"));
                    if (entity.Attributes.Contains("parentcustomerid"))
                        Registro.Attributes.Add("parentcustomerid", entity.GetAttributeValue<EntityReference>("parentcustomerid"));
                    serviceProxyCliente.Update(Registro);

                }
                catch (Exception e)
                {

                    Console.WriteLine("{0} Exception ", e);
                }
            }

            return collectionEntity;
        }


        static EntityCollection RetornarMultiploClienteConcorrentes(CrmServiceClient serviceProxy, CrmServiceClient serviceProxyCiente)
        {
            //Query Concorrentes
            QueryExpression queryConcorrentes = new QueryExpression("competitor");
            queryConcorrentes.Criteria.AddCondition("name", ConditionOperator.NotNull);
            queryConcorrentes.ColumnSet = new ColumnSet("name", "websiteurl", "transactioncurrencyid", "address1_line1", "address1_line2 ", "address1_line3", "address1_city", "address1_stateorprovince", "address1_postalcode", "address1_country");
            EntityCollection collectionEntities = serviceProxy.RetrieveMultiple(queryConcorrentes);

            foreach (var entities in collectionEntities.Entities)
            {
                try
                {
                    var concorrentes = new Entity("competitor");
                    Guid Registro = new Guid();

                    if (entities.Attributes.Contains("name"))
                        concorrentes.Attributes.Add("name", entities.GetAttributeValue<string>("name"));

                    if (entities.Attributes.Contains("websiteurl"))
                        concorrentes.Attributes.Add("websiteurl", entities.GetAttributeValue<string>("websiteurl"));

                    if (entities.Attributes.Contains("transactioncurrencyid"))
                        concorrentes.Attributes.Add("transactioncurrencyid", entities.GetAttributeValue<string>("transactioncurrencyid"));

                    if (entities.Attributes.Contains("address1_line1"))
                        concorrentes.Attributes.Add("address1_line1", entities.GetAttributeValue<string>("address1_line1"));

                    if (entities.Attributes.Contains("address1_line2"))
                        concorrentes.Attributes.Add("address1_line2", entities.GetAttributeValue<string>("address1_line2"));

                    if (entities.Attributes.Contains("address1_line3"))
                        concorrentes.Attributes.Add("address1_line3", entities.GetAttributeValue<string>("address1_line3"));

                    if (entities.Attributes.Contains("address1_city"))
                        concorrentes.Attributes.Add("address1_city", entities.GetAttributeValue<string>("address1_city"));

                    if (entities.Attributes.Contains("address1_stateorprovince"))
                        concorrentes.Attributes.Add("address1_stateorprovince", entities.GetAttributeValue<string>("address1_stateorprovince"));

                    if (entities.Attributes.Contains("address1_postalcode"))
                        concorrentes.Attributes.Add("address1_postalcode", entities.GetAttributeValue<string>("address1_postalcode"));

                    if (entities.Attributes.Contains("address1_country"))
                        concorrentes.Attributes.Add("address1_country", entities.GetAttributeValue<string>("address1_country"));

                    Registro = serviceProxy.Create(concorrentes);

                }
                catch (Exception e)
                {

                    Console.WriteLine("{0} Exception ", e);
                }
            }

            return collectionEntities;
        }


        private static EntityCollection RetornarMultiploClienteFaturaItens(CrmServiceClient serviceProxy, CrmServiceClient serviceProxyCiente)
        {
            //Query Fatura
            QueryExpression queryFatura = new QueryExpression("invoice");
            queryFatura.Criteria.AddCondition("invoicenumber", ConditionOperator.NotNull);
            queryFatura.Criteria.AddCondition("opportunityid", ConditionOperator.NotNull);
            queryFatura.Criteria.AddCondition("pricelevelid", ConditionOperator.NotNull);
            queryFatura.ColumnSet = new ColumnSet("invoicenumber", "name", "transactioncurrencyid", "pricelevelid", "ispricelocked", "opportunityid", "salesorderid", "customerid");
            EntityCollection collectionEntities = serviceProxy.RetrieveMultiple(queryFatura);

            foreach (var entities in collectionEntities.Entities)
            {
                try
                {
                    var fatura = new Entity("invoice");
                    Guid Registro = new Guid();

                    if (entities.Attributes.Contains("invoicenumber"))
                        fatura.Attributes.Add("invoicenumber", entities.GetAttributeValue<string>("invoicenumber"));

                    if (entities.Attributes.Contains("name"))
                        fatura.Attributes.Add("name", entities.GetAttributeValue<string>("name"));

                    if (entities.Attributes.Contains("transactioncurrencyid"))
                        fatura.Attributes.Add("transactioncurrencyid", entities.GetAttributeValue<string>("transactioncurrencyid"));

                    if (entities.Attributes.Contains("pricelevelid"))
                        fatura.Attributes.Add("pricelevelid", entities.GetAttributeValue<string>("pricelevelid"));

                    if (entities.Attributes.Contains("ispricelocked"))
                        fatura.Attributes.Add("ispricelocked", entities.GetAttributeValue<string>("ispricelocked"));

                    if (entities.Attributes.Contains("opportunityid"))
                        fatura.Attributes.Add("opportunityid", entities.GetAttributeValue<EntityReference>("opportunityid"));

                    if (entities.Attributes.Contains("salesorderid"))
                        fatura.Attributes.Add("salesorderid", entities.GetAttributeValue<EntityReference>("salesorderid"));

                    if (entities.Attributes.Contains("customerid"))
                        fatura.Attributes.Add("customerid", entities.GetAttributeValue<string>("customerid"));

                    Registro = serviceProxy.Create(fatura);
                }
                catch (Exception e)
                {

                    Console.WriteLine("{0} Exception ", e);
                }

            }
            return collectionEntities;
        }


    }
}

