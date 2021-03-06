﻿const uri = "v1/api/tarefa";
let todos = null;

function getCount(data) {
    const el = $("#counter");
    let nome = "Tarefas (to-do)";
    if (data) {
        if (data > 1) {
            nome = "to-dos";
        }
        el.text(data + " " + nome);
    } else {
        el.text("No " + nome);
    }
}

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#todos");

            $(tBody).empty();

            getCount(data.length);

            $.each(data, function (key, tarefa) {
                const tr = $("<tr></tr>")
                    .append(
                        $("<td></td>").append(
                            $("<input/>", {
                                type: "checkbox",
                            disabled: true,
                            checked: tarefa.concluido
                            })
                        )
                    )
                    .append($("<td></td>").text(tarefa.nome))
                    .append(
                        $("<td></td>").append(
                            $("<button>Edit</button>").on("click", function () {
                                editItem(tarefa.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button>Delete</button>").on("click", function () {
                            deleteItem(tarefa.id);
                            })
                        )
                    );

                tr.appendTo(tBody);
            });

            todos = data;
        }
    });
}

function addItem() {
    const tarefa = {
        nome: $("#add-name").val(),
        concluido: false
    };

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri,
        contentType: "application/json",
        data: JSON.stringify(tarefa),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Algo deu errado!");
        },
        success: function (result) {
            getData();
            $("#add-name").val("");
        }
    });
}

function deleteItem(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
        }
    });
}

function editItem(id) {
    $.each(todos, function (key, tarefa) {
        if (tarefa.id === id) {
            $("#edit-name").val(tarefa.nome);
            $("#edit-id").val(tarefa.id);
            $("#edit-isComplete")[0].checked = tarefa.concluido;
        }
    });
    $("#spoiler").css({ display: "block" });
}

$(".my-form").on("submit", function () {
    const tarefa = {
        nome: $("#edit-name").val(),
        concluido: $("#edit-isComplete").is(":checked"),
        id: $("#edit-id").val()
    };

    $.ajax({
        url: uri + "/" + $("#edit-id").val(),
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(tarefa),
        success: function (result) {
            getData();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $("#spoiler").css({ display: "none" });
}