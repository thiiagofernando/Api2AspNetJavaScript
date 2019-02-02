using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Newtonsoft.Json;

namespace WebApp01.Models
{
    public class Aluno
    {


        public List<AlunoDTO> ListarTodosAlunos()
        {
            try
            {
                var alunobd = new AlunoDAO();
                return alunobd.ListarAlunosTodosDb();
            }
            catch (Exception ex)
            {
                throw new Exception($"erro {ex.Message}");
            }
        }
        public AlunoDTO ListarAlunosId(int id)
        {
            try
            {
                var alunobd = new AlunoDAO();
                return alunobd.ListarAlunosPorId(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"erro {ex.Message}");
            }
        }
        public List<AlunoDTO> ListarAlunosIdNomeSobreNome(int id, string nome,string sobrenome)
        {
            try
            {
                var alunobd = new AlunoDAO();
                return alunobd.BuscarAlunosPorIdNomeSobrenome(id, nome, sobrenome);
            }
            catch (Exception ex)
            {
                throw new Exception($"erro {ex.Message}");
            }
        }


        public AlunoDTO Atualizar(int id, AlunoDTO aluno)
        {
            try
            {
               var alunobd = new AlunoDAO();
               return alunobd.AtualizarAlunoDB(id,aluno);
            }
            catch (Exception ex)
            {
                throw new Exception($"erro inserir {ex.Message}");
            }
        }

        public int Deletar(int id)
        {
            var alunoDeletar = new AlunoDAO();
            try
            {
                var conta = alunoDeletar.DeletarAlunoDB(id);
                return conta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Deletar {ex.Message}");
            }
        }

        public void Inserir(AlunoDTO aluno)
        {
            try
            {
                var alunobd = new AlunoDAO();
                alunobd.InserirAlunoDB(aluno);
            }
            catch (Exception ex)
            {

                throw new Exception($"erro inserir {ex.Message}");
            }
        }

    }
}