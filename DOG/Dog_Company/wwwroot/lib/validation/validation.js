var validation = {

    inputsRequired : null,
    inputsCPF: null,

    init: function () {

        //recuperando os elementos marcados para validação
        validation.inputsRequired = document.querySelectorAll("[data-validation-required]");

        for (var i = 0; i < validation.inputsRequired.length; i++) {
            validation.inputsRequired[i].addEventListener("blur", function () {
                validation.requiredValidade(this);

            });

        }


        validation.inputsCPF = document.querySelectorAll("[data-validation-cpf]");

        for (var i = 0; i < validation.inputsCPF.length; i++) {
            validation.inputsCPF[i].addEventListener("blur", function () {
                validation.cpfValidade(this);
            });
        }

    },

    requiredValidade: function (input) {

        var valid = true;
        var spanMsgId = input.getAttribute("data-validation-summary");
        var spanMsg = document.getElementById(spanMsgId);
        
     
        if (input.value.trim() == "") {

            var msg = input.getAttribute("data-validation-required");
            if (msg == "") {
                msg = "Campo requerido.";
            }
            spanMsg.innerHTML = msg;
            input.classList.add("inputInvalid");
            valid = false;
        }
        else {
            spanMsg.innerHTML = "";
            input.classList.remove("inputInvalid");
        }

        return valid;
    },


    cpfValidade: function (input) {

        var valid = true;
        var spanMsgId = input.getAttribute("data-validation-summary");
        var spanMsg = document.getElementById(spanMsgId);

        if (input.className.indexOf("inputInvalid") > -1)
            return;

       

        if (input.value.trim() != "" &&
            input.value.trim().length < 11) {

            var msg = input.getAttribute("data-validation-cpf");
            if (msg == "") {
                msg = "CPF inválido.";
            }
            spanMsg.innerHTML = msg;
            input.classList.add("inputInvalid");
            valid = false;
        }
        else {
            spanMsg.innerHTML = "";
            input.classList.remove("inputInvalid");
        }

        return valid;
    },


    formIsValid: function () {

        var valid = true;
        
        for (var i = 0; i < validation.inputsRequired.length; i++) {

            var input = validation.inputsRequired[i];
            if (!validation.requiredValidade(input)) {

                if (valid)
                    input.focus();

                valid = false;
            }
        }


        for (var i = 0; i < validation.inputsCPF.length; i++) {

            var input = validation.inputsCPF[i];
            if (!validation.cpfValidade(input)) {

                if (valid)
                    input.focus();

                valid = false;
            }
        }


        return valid;
    }



}


document.addEventListener("DOMContentLoaded", function () {
    validation.init();
});
