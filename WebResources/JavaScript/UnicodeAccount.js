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
        leg_porte: "leg_porte",
        name: "name"
    },
    PorteOnChange: function (context) {
        var formContext = context.getFormContext();//formulario conta
        var attributeName = Unicode.Account.Attributos.leg_porte;//campo porte

        var valordoCampo = formContext.getAttribute(attributeName).getValue();//valor do campo porte

        if (valordoCampo == Unicode.Account.LEG_porte.Pequeno)
            formContext.getAttribute(Unicode.Account.Attributos.leg_niveldocliente).setValue(Unicode.Account.LEG_niveldocliente.Silver);//se valor do campo porte for pequeno, set value silver no nivel do cliente

        else if (valordoCampo == Unicode.Account.LEG_porte.Medio)
            formContext.getAttribute(Unicode.Account.Attributos.leg_niveldocliente).setValue(Unicode.Account.LEG_niveldocliente.Gold);//se valor do campo porte for pequeno, set value gold no nivel do cliente

        else if (valordoCampo == Unicode.Account.LEG_porte.Grande)
            formContext.getAttribute(Unicode.Account.Attributos.leg_niveldocliente).setValue(Unicode.Account.LEG_niveldocliente.Platinum);//se valor do campo porte for pequeno, set value platinum no nivel do cliente
    },
    CNJPOnchange: function (context) {
        var formContext = context.getFormContext();
        var cnpjField = "leg_cnpj";
        var cnpj = formContext.getAttribute(cnpjField).getValue();
        if (cnpj == null || cnpj == "") {
            return;
        } else {
            cnpj = cnpj.replace(".", "").replace(".", "").replace("/", "").replace("-", "");
            var verify = /[^0-9]/g;
            cnpj = cnpj.replace(verify, "");
            if (cnpj.length != 14) {
                this.DynamicsCustomAlert("Por favor digite 14 dígitos no campo CNPJ", "Erro de Validação de CNPJ");
                formContext.getAttribute(cnpjField).setValue("");
            }

            else {
                cnpj = cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
                formContext.getAttribute(cnpjField).setValue(cnpj);
            }
            if (cnpj == null || cnpj == "") {
                return;
            }
        }
        
    },
    CEPOnchange: function (context) {
        var formContext = context.getFormContext();
        var cepField = "address1_postalcode";
        var cep = formContext.getAttribute(cepField).getValue();
        cep = cep.replace(".", "").replace(".", "").replace("/", "").replace("-", "");
        var verif = /[^0-9]/g;
        cep = cep.replace(verif, "");
        if (cep.length != 8) {
            this.DynamicsCustomAlert("Por favor digite 8 dígitos no campo de CEP", "Erro de Validação de CEP");
            formContext.getAttribute(cepField).setValue("");
        }
        else {
            cep = cep.replace(/^([\d]{5})([\d]{3})|^[\d]{5}-[\d]{3}/, "$1-$2");
            formContext.getAttribute(cepField).setValue(cep);
        }
        if (cep == null) {
            return;
        }
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
    },
    NAMEOnchange: function (context) {
        var formContext = context.getFormContext();

        var atributoNome = Unicode.Account.Attributos.name;
        var nome = formContext.getAttribute(atributoNome).getValue();

        if (nome == null || nome == " ") {
            this.DynamicsCustomAlert("Digite um nome válido", "NOME INVÀLIDO");
        }
        else {
            palavra = nome.split(" ");
            function nameFormat(frase) {
                frase = frase.toLowerCase();
                return frase[0].toUpperCase() + frase.slice(1);
            }
            function captal(frase) {
                fraseModify = []
                for (let i = 0; i < palavra.length; i++) {
                    fraseModify[i] = nameFormat(palavra[i]);
                }
                for (let i = 0; i < palavra.length; i++) {

                    if (fraseModify[i].length <= 3 || fraseModify[i].getValue == "Dos") {
                        fraseModify[i] = palavra[i].toLowerCase();
                    }
                    else {
                        continue
                    }
                }
                return fraseModify.join(" ")
            }
            fraseModified = captal(palavra);
            formContext.getAttribute(Unicode.Account.Attributos.name).setValue(fraseModified);
        }
    },
    
    


}





