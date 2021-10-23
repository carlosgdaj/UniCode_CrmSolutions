if (typeof Unicode == 'undefined') {
    Unicode = {};
}
if (typeof Unicode.Opportunity == 'undefined') {
    Unicode.Opportunity = {};
}

Unicode.Opportunity = {
    Attributos: {
        leg_oportunidadeid: 'leg_oportunidadeid'
    },
    OnSave: function (context) {

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
