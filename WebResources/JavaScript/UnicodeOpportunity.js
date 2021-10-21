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


        var id = !formContext.getAttribute(attributeName).getValue() ? Unicode.Opportunity.geraCodigo() : formContext.getAttribute(attributeName).getValue();
        formContext.getAttribute(attributeName).setValue(id);
       
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
    }
};
