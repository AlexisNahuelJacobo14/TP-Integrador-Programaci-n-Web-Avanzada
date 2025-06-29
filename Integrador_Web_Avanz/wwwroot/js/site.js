// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener("DOMContentLoaded", function () {
	const tipoConsulta = document.getElementById("tipoConsulta");
const partnerContainer = document.getElementById("partnerContainer");

function mostrarOcultarPartner() {
		if (tipoConsulta.value && tipoConsulta.value != "Otros") {
	partnerContainer.style.display = "block";
		} else {
			partnerContainer.style.display = "none";
			partnerSelect.value = '';
		}
	}

tipoConsulta.addEventListener("change", mostrarOcultarPartner);
mostrarOcultarPartner(); // Ejecutar al cargar por si hay valor seleccionado
});
