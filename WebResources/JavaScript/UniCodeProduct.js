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
            leg_tipodegraduacao: "leg_tipodegraduacao"
        },
    TipoDeGraduacaoOnChange: function (context) {
        var formContext = context.getFormContext();
        var attributeName = Unicode.Product.Attributos.leg_tipodegraduacao;
        var valordoCampo = formContext.getAttribute(attributeName).getValue();

        if (valordoCampo == Unicode.Product.LEG_tipodegraduacao.Tecnologo)
            formContext.getAttribute(Unicode.Product.Attributos.leg_tempodecurso).setValue(Unicode.Product.LEG_tempodecurso.DoisAnos);
        else if (valordoCampo == Unicode.Product.LEG_tipodegraduacao.Bacharel)
            formContext.getAttribute(Unicode.Product.Attributos.leg_tempodecurso).setValue(Unicode.Product.LEG_tempodecurso.QuatroAnos);
    }
}

