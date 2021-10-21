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
  OnLoad: function (context) {
    var formContext = context.getFormContext();

    var attributeName = Unicode.Opportunity.Attributos.leg_oportunidadeid;

    var id = Unicode.Opportunity.geraCodigo();

    Xrm.WebApi.online
      .retrieveMultipleRecords(
        'opportunity',
        "?$select=leg_oportunidadeid&$filter=_leg_oportunidadeid_value eq '" +
          id +
          "'"
      )
      .then(
        function success(results) {
          if (results.entities.length > 0) {
            DynamicsCustomAlert(
              'Oportunidade ID duplicado',
              'Não foi possível atribuir um ID a oportunidade'
            );
          } else {
            formContext.getAttribute(attributeName).setValue(id);
          }
        },
        function (error) {
          DynamicsCustomAlert(error.message, 'Erro com a Query de Contatos!');
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

    return id.join('').slice(0, 14);
  }
};
