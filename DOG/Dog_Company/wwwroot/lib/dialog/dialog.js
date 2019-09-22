var dialog1 = {

    title:"",
    divContainer: null,

    init: function (title) {
        dialog1.title = title;
        dialog1.divContainer = document.getElementById("divMsg");
        if (dialog1.divContainer == null) {
            dialog1.divContainer = document.createElement("div");
            dialog1.divContainer.id = "divMsg";
            document.body.appendChild(dialog1.divContainer);
        }


    },

    createMsg: function (msg, type, functionYes) {

        var html = "<div class=\"modale opened\" >" +
                    "<div class=\"modal-dialog1\">" +
                    "    <div class=\"modal-header @type\">" +
                    "        @title" + 
                    "    </div>" +
                    "    <div class=\"modal-body\">" +
                    "      @msg " +
                    "    </div>" +
                    "    <div class=\"modal-footer\">" +
                    "       @btn @btn2" +
                    "   </div>" +
                    " </div>" +
                    " </div>";

        var htmlOK = html.replace("@title", dialog1.title).replace("@msg", msg).replace("@type", type);


        if (type == "alert" || type == "error") {
            var btn = "<input id=\"modale-ok\" type=\"button\" value=\"OK\" class=\"btn\" />"

            htmlOK = htmlOK.replace("@btn", btn);
        }
        if (type == "confirmacao") {
            var btn = "<input id=\"modale-cancel\" type=\"button\" value=\"cancel\" class=\"btn\" />"
            htmlOK = htmlOK.replace("@btn", btn);

            var btn2 = "<input id=\"modale-ok\" type=\"button\" value=\"OK\" class=\"btn\" />"
            htmlOK = htmlOK.replace("@btn2", btn2);

        }
        dialog1.divContainer.innerHTML = htmlOK;

        
        if (type == "confirmacao") {
            dialog1.divContainer.querySelector("#modale-ok").onclick = function () {
                cli.excluir();
                dialog1.divContainer.innerHTML = "";
            }
            

            dialog1.divContainer.querySelector("#modale-cancel").onclick = function () {
                dialog1.divContainer.innerHTML = "";
            }

        }

        if (type == "alert" || type == "error") {

            dialog1.divContainer.querySelector("#modale-ok").onclick = function () {
                dialog1.divContainer.innerHTML = "";
            }
        }


    },

    alert: function (msg, type) {

        dialog1.createMsg(msg, type, null);
    },
    
    alertSuccess: function (msg) {
        dialog1.createMsg(msg, "alert", null);
    },

    alertError: function (msg) {
        dialog1.createMsg(msg, "error", null);
    },

    confirm: function (msg) {

        dialog1.createMsg(msg, "confirmacao", null);
    }

    

}

document.addEventListener("DOMContentLoaded", function () {
    dialog1.init("Sistema X");
});