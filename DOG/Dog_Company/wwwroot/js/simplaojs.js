var simplaojs = {

    byId: function (id) {

        return document.getElementById(id);
    },

    bySelector: function (selector) {

        return document.querySelector(selector);
    },

    bySelectorAll: function (selector) {

        return document.querySelectorAll(selector);
    },

    onclick: function (id, f) {
        simplaojs.byId(id).onclick = f;
    },

    onchange: function (id, f) {
        simplaojs.byId(id).onchange = f;
    },

    renderTemplate(stringTemplate, obj){
        for (var p in obj) {
            stringTemplate = stringTemplate.replace(new RegExp('{' + p + '}', 'g'), obj[p]);
        }
         return stringTemplate;
    },
    esconder: function () {
        document.querySelector("#exampleModal").classList.remove("show");
        document.querySelector("#exampleModal").style.display = "none";
    },
    pegar: function (vl) {
        var retorno = document.getElementById(vl).value;
        return retorno;
    },

    ajax: function (obj, action, method, funcaoSucesso, funcaoErro) {

        var req = new XMLHttpRequest();
        req.open(method, action, true);
        req.setRequestHeader('Content-Type',
            'application/json; charset=utf-8');

        req.onreadystatechange = function () {

            if (req.readyState == 4 &&
                req.status == 200) {
                //finalizada

                var lit = JSON.parse(req.responseText);

                if (funcaoSucesso != null) 
                    funcaoSucesso(lit);

            }
            else if (req.readyState == 4 &&
                req.status != 200) {
                //deu pau....
                if (funcaoErro != null)
                    funcaoErro();
              
            }
        }

        if (obj != null)
            req.send(JSON.stringify(obj));
        else req.send();

    }
}

var y = simplaojs;

