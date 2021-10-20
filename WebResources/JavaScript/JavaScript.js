if (typeof (Unicode) == "undefined") { Unicode = {} }
if (typeof (Unicode.Account) == "undefined") { Unicode.Account = {} }

Unicode.Account = {
    LEG_porte: {
        Pequeno: 100000000,
        Medio: 100000001,
        Grande: 100000002
    },
    LEG_niveldocliente: {
        Silver: 100000000,
        Gold: 100000001,
        Platinum: 100000002
    },
    Attributos: {
        leg_niveldocliente: "leg_niveldocliente",
        leg_porte: "leg_porte"
    },
    PorteOnChange: function (context) {
        var formContext = context.getFormContext();//formulario conta
        var attributeName = Unicode.Account.Attributos.leg_porte;//campo porte

        var valordoCampo = formContext.getAttribute(attributeName).getValue();//valor do campo porte

        if (valordoCampo == Unicode.Account.LEG_porte.Pequeno) {
            formContext.getAttribute(Unicode.Account.Attributos.leg_niveldocliente).setValue(Unicode.Account.LEG_niveldocliente.Silver);
        } else {
            if (valordoCampo == Unicode.Account.LEG_porte.Medio) {
                formContext.getAttribute(Unicode.Account.Attributos.leg_niveldocliente).setValue(Unicode.Account.LEG_niveldocliente.Gold);
            } else {
                if (valordoCampo == Unicode.Account.LEG_porte.Grande) {
                    formContext.getAttribute(Unicode.Account.Attributos.leg_niveldocliente).setValue(Unicode.Account.LEG_niveldocliente.Platinum);
                }
            }
        }
    },
    CNJPOnchange: function (context) {
        var formContext = context.getFormContext();
        var cnpjField = "leg_cnpj";

        var cnpj = formContext.getAttribute(cnjpField).getValue();
        cnpj = cnpj.replace(".", "").replace(".", "").replace("/", "").replace("-", "");
        cnpj = replace(/[\s.-]*/igm, '');
        if (cnpj.length != 14) {
            this.DynamicsCustomAlert("Por favor digite 14 dígitos no campo CNPJ", "Erro de Validação de CNPJ");
            formContext.getAttribute(cnpjField).setValue("");
        }
        else {

            cnpj = cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
            formContext.getAttribute(cnpjField).setValue(cnpj);

        }


    },
    CEPOnchange: function (context) {
        var formContext = context.getFormContext();
        var cepField = "address1_postalcode";

        var cep = formContext.getAttribute(cepField).getValue();
        cep = cep.replace(".", "").replace(".", "").replace("/", "").replace("-", "");
        cep = replace(/[\s.-]*/igm, '');
        if (cep.length != 8) {
            this.DynamicsCustomAlert("Por favor digite 8 dígitos no campo de CEP", "Erro de Validação de CEP");
            formContext.getAttribute(cepField).setValue("");
        }
        else {

            cep = cep.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
            formContext.getAttribute(cepField).setValue(cep);

        }


    },

}


