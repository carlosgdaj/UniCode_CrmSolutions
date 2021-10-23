if (typeof (Unicode) == "undefined") { Unicode = {} }
if (typeof (Unicode.Product) == "undefined") { Unicode.Product = {} }

Unicode.Product = {
    LEG_tempodecurso: {
        DoisAnos: 100000000,
        QuatroAnos: 100000001,
    },
    LEG_tipodegraduacao: {
        Tecnologo: 100000000,
        Bacharel: 100000001,
    },
    Attributos: {
        leg_tempodecurso: "leg_tempodecurso",
        leg_tipodegraduacao: "leg_tipodegraduacao",
        productnumber: "productnumber"
    },
    TipoDeGraduacaoOnChange: function (context) {
        var formContext = context.getFormContext();
        var attributeName = Unicode.Product.Attributos.leg_tipodegraduacao;
        var valordoCampo = formContext.getAttribute(attributeName).getValue();

        if (valordoCampo == Unicode.Product.LEG_tipodegraduacao.Tecnologo)
            formContext.getAttribute(Unicode.Product.Attributos.leg_tempodecurso).setValue(Unicode.Product.LEG_tempodecurso.DoisAnos);
        else if (valordoCampo == Unicode.Product.LEG_tipodegraduacao.Bacharel)
            formContext.getAttribute(Unicode.Product.Attributos.leg_tempodecurso).setValue(Unicode.Product.LEG_tempodecurso.QuatroAnos);
    },
    OnChangeName: function (context) {

        var formContext = context.getFormContext();

        var attributeName = Unicode.Product.Attributos.productnumber;

        var id = Unicode.Product.geraCodigo();

        Xrm.WebApi.online.retrieveMultipleRecords("product", "?$select=productnumber&$filter=productnumber eq '" + id + "'").then(
            function success(results) {
                if (results.entities.length > 0) {
                    id = !formContext.getAttribute(attributeName).getValue() ? Unicode.Product.geraCodigo() : formContext.getAttribute(attributeName).getValue();
                    formContext.getAttribute(attributeName).setValue(id);
                }
                else {

                    id = !formContext.getAttribute(attributeName).getValue() ? id : formContext.getAttribute(attributeName).getValue();
                    formContext.getAttribute(attributeName).setValue(id);

                }
            },
            function (error) {
                Unicode.Product.DynamicsCustomAlert(error.message, "Erro com a Query de Contatos!");
            }
        );

    },
    rand: function (min, max) {
        return Math.floor(Math.random() * (max - min) + min);
    },
    geraMaiuscula: function () {
        return String.fromCharCode(Unicode.Product.rand(65, 91));
    },
    geraNumero: function () {
        return String.fromCharCode(Unicode.Product.rand(48, 58));
    },
    geraCodigo: function () {
        var id = [];

        id.push('C');
        id.push('-');
        for (let i = 0; i < 6; i++) {
            id.push(Unicode.Product.geraNumero());
        }
        id.push('-');
        for (let i = 0; i < 2; i++) {
            id.push(Unicode.Product.geraMaiuscula());
            id.push(Unicode.Product.geraNumero());
        }

        return id.join('');
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
}

