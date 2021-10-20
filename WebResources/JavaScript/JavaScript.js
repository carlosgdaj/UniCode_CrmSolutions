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
    }
}