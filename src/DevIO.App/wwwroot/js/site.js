// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function BuscaCep() {

    $(document).ready(function () {

        function limpa_fomulario() {
            $("#Endereco_Logradouro").val("");
            $("#Endereco_Bairro").val("");
            $("#Endereco_Cidade").val("");
            $("#Endereco_Estado").val("");

        }

        $("#Endereco_Cep").blur(function () {

            var cep = $(this).val().replace(/\D/g, '');

            if (cep != "") {

                var validacep = /^[0-9]{8}$/;

                //Valida formato do CEP
                if (validacep.test(cep)) {
                    //Preenche os campos com ... enquanto consulta webservice
                    $("#Endereco_Logradouro").val("...");
                    $("#Endereco_Bairro").val("...");
                    $("#Endereco_Cidade").val("...");
                    $("#Endereco_Estado").val("...");

                    //Consulta webservice 
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",

                        function (dados) {
                            //Atualiza os campos com valores da consulta
                            if (!("erro" in dados)) {
                                $("#Endereco_Logradouro").val(dados.logradouro);
                                $("#Endereco_Bairro").val(dados.bairro);
                                $("#Endereco_Cidade").val(dados.localidade);
                                $("#Endereco_Estado").val(dados.uf);
                            }

                            //Cep é não encontrado
                            else {
                                limpa_fomulario();
                                alert("CEP não encontrado.");
                            }

                        });
                } //end if

                else {
                    limpa_fomulario();
                    alert("CEP inválido");
                }

            } //end if
            else {
                limpa_fomulario();
            }

        });

    });
}