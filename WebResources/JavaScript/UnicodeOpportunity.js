if (typeof (Unicode) == "undefined") { Unicode = {} }
if (typeof (Unicode.Opportunity) == "undefined") { Unicode.Account = {} }

Unicode.Opportunity = {
    Attributos: {
        leg_oportunidadeid: "leg_oportunidadeid"
    },
    IdOnChange = function(context) {
        var formContext = context.getFormContext();

        var attributeName = Unicode.Account.Attributos.leg_oportunidadeid;

       formContext.getAttribute(attributeName).setValue("laranja");
    },

}