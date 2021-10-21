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
    //OnLoad: function (context) {
    //  var formContext = context.getFormContext();

    //  var attributeName = Unicode.Opportunity.Attributos.leg_oportunidadeid;

    //  var id = Unicode.Opportunity.geraCodigo();

    //  Xrm.WebApi.online
    //    .retrieveMultipleRecords(
    //      'opportunity',
    //      "?$select=leg_oportunidadeid&$filter=_leg_oportunidadeid_value eq '" +
    //        id +
    //        "'"
    //    )
    //    .then(
    //      function success(results) {
    //        if (results.entities.length > 0) {
    //          DynamicsCustomAlert(
    //            'Oportunidade ID duplicado',
    //            'Não foi possível atribuir um ID a oportunidade'
    //          );
    //        } else {
    //          formContext.getAttribute(attributeName).setValue(id);
    //        }
    //      },
    //      function (error) {
    //        DynamicsCustomAlert(error.message, 'Erro com a Query de Contatos!');
    //      }
    //    );
    //  },
    OppIdOnChange: function (context) {
        var formContext = context.getFormContext();
        var attributeName = Unicode.Opportunity.Attributos.leg_oportunidadeid;

        var id = formContext.getAttribute(attributeName).getValue();

        Xrm.WebApi.online.retrieveMultipleRecords('opportunity', "?$select=leg_oportunidadeid&$filter=_leg_oportunidadeid_value eq '" + id + "'").then(
            function success(results) {
                if (results.entities.length > 0) {
                    formContext.getAttribute(attributeName).setValue("dghdfghfd");
                } else {
                    formContext.getAttribute(attributeName).setValue(id);
                }
            },
            function (error) {
                DynamicsCustomAlert(error.message, 'Erro com a Query de Contatos!');
            }
        );
    }
};
