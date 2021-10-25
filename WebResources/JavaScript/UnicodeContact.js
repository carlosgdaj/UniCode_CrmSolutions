if (typeof (Unicode) == "undefined") { Unicode = {} }
if (typeof (Unicode.Contact) == "undefined") { Unicode.Contact = {} }

Unicode.Contact = {
    OnLoad: function (context) {
        this.CPFOnChange(context);

    },
    CPFOnChange: function (context) {
        var formContext = context.getFormContext();
        var cpfField = "leg_cpf";

        var cpf = formContext.getAttribute(cpfField).getValue();

        if (cpf == "" || cpf == null)
            return;

        cpf = cpf.replace(".", "").replace(".", "").replace("-", "");

        if (cpf.length != 11) {
            formContext.getAttribute(cpfField).setValue("");
            this.DynamicsCustomAlert("Por favor, digite 11 caracteres no CPF", "Erro de Validação de CPF");

        }
        else {
            cpf = cpf.replace(/^(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
            formContext.getAttribute(cpfField).setValue(cpf);
        }
    },
    DynamicsCustomAlert: function (alertText, alertTitle) {
        var alertStrings = {
            confirmButtonLabel: "OK",
            text: alertText,
            title: alertTitle
        };

        var alertOptions = {
            heigtn: 120,
            width: 200
        };
        Xrm.Navigation.openAlertDialog(alertStrings, alertOptions);
    }
}