function refresh() {
    window.location.reload();
}
function Limpar() {
    $('#inputNome').val('');
    $('#inputSobrenome').val('');
    $('#inputTelefone').val('');
    $('#inputRA').val('');
    $('#titulo').text('Cadastrar Aluno');
    $('#btncad').textContent = 'Cadastrar';
    $('#btnlimpa').textContent = 'Limpar';
}
function verificarVazio() {
    var nome = $('#inputNome').val();
    var sobrenome = $('#inputSobrenome').val();
    var telefone = $('#inputTelefone').val();
    var ra = $('#inputRA').val();

    if (nome == '' && sobrenome == '' && telefone == '' && ra == '') {
        swal("Nenhum informação encontrada", "", "error");
        setTimeout("refresh()", 3000);
        return false;
    } else {
        return true;
    }
}
$('#btnlimpa').click(function () {
    Limpar();
    aluno = {};

    CarregaEstudantes();
})
var tbody = document.querySelector('table tbody');
var aluno = {};

function Cadastrar() {
    var verificar = verificarVazio();

    if (verificar == true) {
        aluno.nome = $('#inputNome').val();
        aluno.sobrenome = $('#inputSobrenome').val();
        aluno.telefone = $('#inputTelefone').val();
        aluno.ra = $('#inputRA').val();

        if (aluno.id === undefined || aluno.id === 0) {
            SalvarEstudantes('POST', 0, aluno);
            swal("Aluno Cadastrado com Sucesso!!", "", "success");
        } else {
            SalvarEstudantes('PUT', aluno.id, aluno);
            swal("Aluno Alterado com Sucesso!!", "", "success");
        }
        CarregaEstudantes();
        Limpar();
        
    }
}

function CarregaEstudantes() {
    tbody.innerHTML = '';
    var xhr = new XMLHttpRequest();

    xhr.open(`GET`, `http://localhost:61018/api/aluno/Recuperar`, true);
    xhr.onload = function () {
        var estudantes = JSON.parse(this.responseText);
        for (var indice in estudantes) {
            AdicionaLinha(estudantes[indice]);
        }
    }
    xhr.send();
}

function SalvarEstudantes(metodo, id, corpo) {
    var xhr = new XMLHttpRequest();

    if (id === undefined || id === 0) {
        id = '';
    }
    xhr.open(metodo, `http://localhost:61018/api/aluno/${id}`, false);

    xhr.setRequestHeader('content-type', 'application/json');
    xhr.send(JSON.stringify(corpo));
}

CarregaEstudantes();


function AdicionaLinha(estudante) {

    var trow = `<tr>
                    <td>${estudante.nome}</td>
                    <td>${estudante.sobrenome}</td>
                    <td>${estudante.telefone}</td>
                    <td>${estudante.ra}</td>
                    <td><button data-toggle="modal" data-target="#exampleModal" onclick='editarEstudante(${JSON.stringify(estudante)})'class="btn btn-warning">Editar</button>
                    <button onclick='deletarEstudante(${JSON.stringify(estudante)})'class="btn btn-danger">Deletar</button></td>

</tr>
                `

    tbody.innerHTML += trow;

}

function editarEstudante(estudante) {
    var btnSalvar = document.querySelector('#btncad');
    var btnCancelar = document.querySelector('#btnlimpa');
    var titulo = document.querySelector('#titulo');

    $('#inputNome').val(estudante.nome);
    $('#inputSobrenome').val(estudante.sobrenome);
    $('#inputTelefone').val(estudante.telefone);
    $('#inputRA').val(estudante.ra);


    titulo.textContent = `Ediar Aluno ${estudante.nome}`
    btnSalvar.textContent = 'Salvar'
    btnCancelar.textContent = 'Cancelar'
    aluno = estudante;
}

function Excluir(id) {
    var xhr = new XMLHttpRequest();

    if (id === undefined || id === 0) {
        swal("Aluno Não Encontrado!!", "", "error");
        return false;
    }
    xhr.open(`DELETE`, `http://localhost:61018/api/aluno/${id}`, false);
    xhr.send();
}
function deletarEstudante(estudante) {

    bootbox.confirm({
        message: `Tem certeza que deseja excluir o aluno ${estudante.nome}`,
        buttons: {
            confirm: {
                label: 'Sim',
                className: 'btn-success'
            },
            cancel: {
                label: 'Não',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                Excluir(estudante.id);
                CarregaEstudantes();
                swal("Aluno excluido!!", "", "success");
            }
        }
    });

}