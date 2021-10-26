if (typeof (Unicode) == "undefined") { Unicode = {} }
if (typeof (Unicode.Account) == "undefined") { Unicode.Account = {} }

Unicode.LOpportunity = {

    VALOROnchange: function (context) {
        var formContext = context.getFormContext();
        var productid = formContext.getAttribute("productid").getValue();
        var newid = productid[0].id
        newid = newid.replace("{", "").replace("}", "");
        Xrm.WebApi.online.retrieveMultipleRecords("product", "?$select=currentcost&$filter=productid eq " + newid ).then(
            function sucess(results) {
                var newvalor = results.entities[0].currentcost;
                formContext.getAttribute("amount").setValue(newvalor);
            },
            function (error) {
                alert(error.message, "Erro com a mensalidade");
            }

        );


    }


}
