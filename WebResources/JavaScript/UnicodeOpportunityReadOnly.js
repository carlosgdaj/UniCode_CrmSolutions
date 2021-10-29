if (typeof (Unicode) == 'undefined') { Unicode = {} }
if (typeof (Unicode.OpportunityReadOnly) == 'undefined') { Unicode.OpportunityReadOnly = {} }

Unicode.OpportunityReadOnly = {
    Attributos: {
        leg_oportunidadeid: 'leg_oportunidadeid',
        leg_integracao: 'leg_integracao',
        name:"name",
        leg_datadeinicio:"leg_datadeinicio",
        leg_unidade:"leg_unidade",
        leg_statusdavacina:"leg_statusdavacina",
        leg_modalidade:"leg_modalidade",
        leg_tipodocurso:"leg_tipodocurso",
        leg_turno:"leg_turno",
        description:"description"

    },
    OnLoad: function (context) {
        debugger;
        var formContext = context.getFormContext();
        var integracao = Unicode.OpportunityReadOnly.Attributos.leg_integracao;

        var valorDaIntegracao = formContext.getAttribute(integracao).getValue();

        if (valorDaIntegracao == true) {
            formContext.getControl(Unicode.OpportunityReadOnly.Attributos.name).setDisabled(true);
            formContext.getControl(Unicode.OpportunityReadOnly.Attributos.leg_datadeinicio).setDisabled(true);
            formContext.getControl(Unicode.OpportunityReadOnly.Attributos.leg_unidade).setDisabled(true);
            formContext.getControl(Unicode.OpportunityReadOnly.Attributos.leg_statusdavacina).setDisabled(true);
            formContext.getControl(Unicode.OpportunityReadOnly.Attributos.leg_modalidade).setDisabled(true);
            formContext.getControl(Unicode.OpportunityReadOnly.Attributos.leg_tipodocurso).setDisabled(true);
            formContext.getControl(Unicode.OpportunityReadOnly.Attributos.leg_turno).setDisabled(true);
            formContext.getControl(Unicode.OpportunityReadOnly.Attributos.description).setDisabled(true);
        }
    }
};
