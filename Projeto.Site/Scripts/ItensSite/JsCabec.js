$(document).ready(function () {
    //Defino que o campo de acoes nao pode ser filtrado nem ordenado
    //redefino os padroes de linguagem para o português

    $('#Dados').dataTable({
        "columnDefs": [{
            "targets": [7],
            "orderable": false,
            "serchable":false
        }],
        "language":
        {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "_MENU_ resultados por página",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Próximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Último"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });

    //Script para capturar os dados e posta-los na tela de edicao
    //utilizo o event.relatedTarget do bootstrap
    $('#TelaEdicao').on('show.bs.modal', function (e) {

        var _button = $(e.relatedTarget);

        // console.log(_button, _button.parents("tr"));
        var _row = _button.parents("tr");
        var _tbId = _row.find(".tb-item-id").text();
        var _tbTitulo = _row.find(".tb-item-titulo").text();
        var _tbAutor = _row.find(".tb-item-autor").text();
        var _tbIsbn = _row.find(".tb-item-isbn").text();
        var _tbPreco = _row.find(".tb-item-preco").text();
        var _tbDtpublicacao = _row.find(".tb-item-dtpublicacao").text();

        //Tiro o formato da moeda e transformo para decimal
        //utilizando as bibliotecas accounting.js e moment.js
        var preco = accounting.unformat(_tbPreco);
        var dtPubli = moment(_tbDtpublicacao, "DD/MM/YYYY");

        //carrego as informacoes no modal
        $(this).find(".tb-item-id").val(_tbId);
        $(this).find(".tb-item-titulo").val(_tbTitulo);
        $(this).find(".tb-item-autor").val(_tbAutor);
        $(this).find(".tb-item-isbn").val(_tbIsbn);
        $(this).find(".tb-item-preco").val(preco);
        $(this).find(".tb-item-dtpublicacao").val(dtPubli.format("YYYY-MM-DD"));
    });

    // validacao do formulario de cadastro
    $('#FormCadastro').validate({
        rules: {
            Autor: {
                required: true,
                maxlength: 50
            },
            Isbn: {
                required: true,
                maxlength: 50,
                remote:"http://localhost:49857/Livro/valIsbn"
            },
            Titulo: {
                required: true,
                maxlength: 50
            },
            Preco: {
                required: true,
                number: true
            },
            DtPublicacao: {
                required: true,
                date: true
            }
        },
        messages: {
            Autor: {
                required: "O campo [Autor] é obrigatório.",
                maxlength: "O campo [Autor] deve conter no maximo 50 caracteres."
            },
            Isbn: {
                required: "O preenchimento do campo [Isbn] é obrigatório",
                maxlength: "O campo [Isbn] deve conter no maximo 50 caracteres.",
                remote :"Já existe um registro para o [Isbn] informado na base de dados."
            },
            Titulo: {
                required: "O preenchimento do campo [Titulo] é obrigatório",
                maxlength: "O campo [Titulo] deve conter no maximo 50 caracteres."
            },
            Preco: {
                required: "O preenchimento do campo [Preço] é obrigatório!",
                number: "O campo [Preço] é numérico!"
            },
            DtPublicacao: {
                required: "O preenchimento do campo [Data publicação] é obrigatório!",
                number: "O campo [Data publicação] é numérico!"
            }
        }
    });


    // validacao do formulario de Edicao 
    $('#FormEdicao').validate({
        rules: {
            Autor: {
                required: true,
                maxlength: 50
            },

            Isbn: {
                required: true,
                maxlength: 50,
                remote: {
                    url: "http://localhost:49857/Livro/valIsbn",
                    data: {
                        Id: function () { return $('#Id').val();}
                    }
                }
            },
            Titulo: {
                required: true,
                maxlength: 50
            },
            Preco: {
                required: true,
                number: true
            },
            DtPublicacao: {
                required: true,
                date: true
            }
        },
        messages: {
            Autor: {
                required: "O campo [Autor] é obrigatório.",
                maxlength: "O campo [Autor] deve conter no maximo 50 caracteres."
            },
            Isbn: {
                required: "O preenchimento do campo [Isbn] é obrigatório",
                maxlength: "O campo [Isbn] deve conter no maximo 50 caracteres.",
                remote: "Já existe um registro para o [Isbn] informado na base de dados."
            },
            Titulo: {
                required: "O preenchimento do campo [Titulo] é obrigatório",
                maxlength: "O campo [Titulo] deve conter no maximo 50 caracteres."
            },
            Preco: {
                required: "O preenchimento do campo [Preço] é obrigatório!",
                number: "O campo [Preço] é numérico!"
            },
            DtPublicacao: {
                required: "O preenchimento do campo [Data publicação] é obrigatório!",
                number: "O campo [Data publicação] é numérico!"
            }
        }
    });

})