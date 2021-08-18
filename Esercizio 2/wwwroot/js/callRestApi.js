"use strict";

function AJAXSubmitFile(oFormElement, UserControl) {
	var oReq = new XMLHttpRequest();
	oReq.onload = function (e) {
		var result = oFormElement.elements.namedItem("result");
		result.value = 'Result Status: ' + this.status;
		if (this.status == 200) {
			var data = JSON.parse(this.responseText);
			result.value += ' -> File Elaborati : ' + data.count + " con un numero totale di parole : " + data.words + " (come parole intendo sequenze alfanumeriche di lettere maiuscole o minuscole con i numeri da 0 a 9 )";
		}
	};
	oReq.open(oFormElement.method, oFormElement.action);
	oReq.send(new FormData(oFormElement));
}
function AJAXSubmitText(oFormElement, UserControl) {
	var oReq = new XMLHttpRequest();
	oReq.onload = function (e) {
		var result = oFormElement.elements.namedItem("result");
		result.value = 'Result Status: ' + this.status;
		if (this.status == 200) {
			var data = JSON.parse(this.responseText);
			result.value += ' -> Testo lungo : ' + data.count + " con un numero totale di parole : " + data.words + " (come parole intendo sequenze alfanumeriche di lettere maiuscole o minuscole con i numeri da 0 a 9 )";
		}
		if (this.status == 401) {
			result.value += "CREDENZIALI ERRATE!";
			alert("CREDENZIALI ERRATE!");
		}
	};
	oReq.open(oFormElement.method, oFormElement.action);
	if (UserControl) {
		var user = UserControl.querySelector("#InputUsername");
		var pass = UserControl.querySelector("#InputPassword");
		if (user.value) {
			alert("inserisci l'username");
			return;
		}
		if (pass.value) {
			alert("inserisci la password");
			return;
		}
		oReq.setRequestHeader("Authorization", "Basic " + btoa(user.value + ":" + pass.value));
	}
	oReq.send(new FormData(oFormElement));
}