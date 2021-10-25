if (typeof Unicode == 'undefined') {
    Unicode = {};
}
if (typeof Unicode.Opportunity == 'undefined') {
    Unicode.Opportunity = {};
}

Unicode.Opportunity = {
    statecode={
        Aberto: 0,
        Ganho: 1,
        Fechado: 2
    },
    Attributos: {
        leg_oportunidadeid: 'leg_oportunidadeid',
        statecode: "statecode",
        parentcontactid: "parentcontactid"
    },
    OnSaveStatusCode: function (context) {
        var formContext = context.getFormContext();
        var attributeName = Unicode.Opportunity.Attributos.statecode;

        var status = formContext.getAttribute(attributeName).getValue();

        if (status == Unicode.Opportunity.statecode.Ganho) {

            var contact = formContext.getAttribute(Unicode.Opportunity.Attributos.parentcontactid).getValue();
            CreateNotification(contact[0].id, contact[0].name);


        }
    },
    CreateNotification: function (contactId, contactName) {
        
        var entity = {}
        entity.["leg_nomedocliente@odata.bind"] = "/contacts(" + contactId + ")";
        entity.leg_datadanotificacao = new Date();
        entity.leg_mensagemdanotificacao = `Bem vindo a UniCode, ${contactName}!\nAgradecemos a sua escolha, sua matrícula foi concluída, agora é com você. Dê o seu melhor e se destaque!\nAtt. Equipe UniCode`;

        Xrm.WebApi.online.createRecord("leg_notificacaoaocliente", entity).then(
            function success(result) {
                var newEntityId = result.id;

                Treinamento.Account.DynamicsCustomAlert("Uma nova conta foi criada com o ID " + newEntityId, "ContaCriada");
            },
            function (error) {
                Treinamento.Account.DynamicsCustomAlert(error.message, "Error");
            }
        );
    },
    OnSave: function (context) {
        OnSaveStatusCode(context);
        var formContext = context.getFormContext();

        var attributeName = Unicode.Opportunity.Attributos.leg_oportunidadeid;

        var id = Unicode.Opportunity.geraCodigo();

        Xrm.WebApi.online.retrieveMultipleRecords("opportunity", "?$select=leg_oportunidadeid&$filter=leg_oportunidadeid eq '" + id + "'").then(
            function success(results) {
                if (results.entities.length > 0) {
                    id = !formContext.getAttribute(attributeName).getValue() ? Unicode.Opportunity.geraCodigo() : formContext.getAttribute(attributeName).getValue();                   
                    formContext.getAttribute(attributeName).setValue(id);
                }
                else {
                    
                    id = !formContext.getAttribute(attributeName).getValue() ? id : formContext.getAttribute(attributeName).getValue();
                    formContext.getAttribute(attributeName).setValue(id);

                }
            },
            function (error) {
                Unicode.Opportunity.DynamicsCustomAlert(error.message, "Erro com a Query de Contatos!");
            }
        );      

    },
    rand: function (min, max) {
        return Math.floor(Math.random() * (max - min) + min);
    },
    geraMaiuscula: function () {
        return String.fromCharCode(Unicode.Opportunity.rand(65, 91));
    },
    geraNumero: function () {
        return String.fromCharCode(Unicode.Opportunity.rand(48, 58));
    },
    geraCodigo: function () {
        var id = [];

        id.push('O');
        id.push('P');
        id.push('P');
        id.push('-');
        for (let i = 0; i < 6; i++) {
            id.push(Unicode.Opportunity.geraNumero());
        }
        id.push('-');
        for (let i = 0; i < 3; i++) {
            id.push(Unicode.Opportunity.geraMaiuscula());
            id.push(Unicode.Opportunity.geraNumero());
        }

        return id.join('').slice(0, 15);
    },
    DynamicsCustomAlert: function (alertText, alertTitle) {
        var alertStrings = {
            confirmButtonLabel: "OK",
            text: alertText,
            title: alertTitle
        };
        var alertOptions = {
            heigth: 120,
            width: 200
        };
        Xrm.Navigation.openAlertDialog(alertStrings, alertOptions);
    }
};
