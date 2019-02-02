using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApp01.Models
{
    public class AlunoDAO
    {
        public string strConecao = ConfigurationManager.ConnectionStrings["ConexaoBD"].ConnectionString;
        public IDbConnection conexao;
        public AlunoDAO()
        {
            conexao = new SqlConnection(strConecao);
            conexao.Open();
        }

        public List<AlunoDTO> ListarAlunosTodosDb()
        {
            if (conexao.State == ConnectionState.Closed)
                conexao.Open();

            var listaAlunos = new List<AlunoDTO>();
            IDbCommand selectCommand = conexao.CreateCommand();
            selectCommand.CommandText = "select * from Aluno";
            IDataReader resultado = selectCommand.ExecuteReader();

            while (resultado.Read())
            {
                var alu = new AlunoDTO();
                alu.id = Convert.ToInt32(resultado["Id"]);
                alu.nome = Convert.ToString(resultado["Nome"]);
                alu.sobrenome = Convert.ToString(resultado["SobreNome"]);
                alu.telefone = Convert.ToString(resultado["Telefone"]);
                alu.ra = Convert.ToInt32(resultado["RA"]);
                alu.data = Convert.ToString(resultado["Data"]);
                listaAlunos.Add(alu);
            }
            if (conexao.State == ConnectionState.Open)
                conexao.Close();
            return listaAlunos;

        }
        public void InserirAlunoDB(AlunoDTO aluno)
        {

            IDbCommand inserCommand = conexao.CreateCommand();
            inserCommand.CommandText = "insert into aluno (nome,sobrenome,telefone,data,ra) values(@nome,@sobrenome,@telefone,@data,@ra)";

            IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
            inserCommand.Parameters.Add(paramNome);

            IDbDataParameter paramsobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
            inserCommand.Parameters.Add(paramsobrenome);

            IDbDataParameter paramtelefone = new SqlParameter("telefone", aluno.telefone);
            inserCommand.Parameters.Add(paramtelefone);

            IDbDataParameter paramdata = new SqlParameter("data", DateTime.Now);
            inserCommand.Parameters.Add(paramdata);

            IDbDataParameter paramra = new SqlParameter("ra", aluno.ra);
            inserCommand.Parameters.Add(paramra);

            inserCommand.ExecuteNonQuery();

        }

        public AlunoDTO AtualizarAlunoDB(int id, AlunoDTO aluno)
        {

            IDbCommand UpdateCommand = conexao.CreateCommand();
            UpdateCommand.CommandText = "update Aluno set Nome=@nome,SobreNome=@sobrenome,Telefone=@telefone,RA=@ra,Data=@data where Id=@id";

            IDbDataParameter paramId = new SqlParameter("id", id);
            UpdateCommand.Parameters.Add(paramId);

            IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
            UpdateCommand.Parameters.Add(paramNome);

            IDbDataParameter paramsobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
            UpdateCommand.Parameters.Add(paramsobrenome);

            IDbDataParameter paramtelefone = new SqlParameter("telefone", aluno.telefone);
            UpdateCommand.Parameters.Add(paramtelefone);

            IDbDataParameter paramdata = new SqlParameter("data", aluno.data);
            UpdateCommand.Parameters.Add(paramdata);

            IDbDataParameter paramra = new SqlParameter("ra", aluno.ra);
            UpdateCommand.Parameters.Add(paramra);

            UpdateCommand.ExecuteNonQuery();

            var retorna = ListarAlunosPorId(id);

            return retorna;


        }

        public AlunoDTO ListarAlunosPorId(int id)
        {
            conexao = new SqlConnection(strConecao);
            if (conexao.State == ConnectionState.Closed)
                conexao.Open();
            AlunoDTO listaAlunos = new AlunoDTO();
            IDbCommand selectCommand = conexao.CreateCommand();
            selectCommand.CommandText = $"select * from Aluno where id={id}";
            IDataReader resultado = selectCommand.ExecuteReader();

            while (resultado.Read())
            {
                listaAlunos.id = Convert.ToInt32(resultado["Id"]);
                listaAlunos.nome = Convert.ToString(resultado["Nome"]);
                listaAlunos.sobrenome = Convert.ToString(resultado["SobreNome"]);
                listaAlunos.telefone = Convert.ToString(resultado["Telefone"]);
                listaAlunos.ra = Convert.ToInt32(resultado["RA"]);
                listaAlunos.data = Convert.ToString(resultado["Data"]);
            }
            if (conexao.State == ConnectionState.Open)
                conexao.Close();
            return listaAlunos;
        }

        public List<AlunoDTO> BuscarAlunosPorIdNomeSobrenome(int id, string nome, string sobrenome)
        {
            conexao = new SqlConnection(strConecao);
            if (conexao.State == ConnectionState.Closed)
                conexao.Open();
            List<AlunoDTO> listaAlunos = new List<AlunoDTO>();
            IDbCommand selectCommand = conexao.CreateCommand();
            selectCommand.CommandText = $"select * from Aluno where id={id} or nome like '%{nome}%' or sobrenome like '%{sobrenome}%' ";
            IDataReader resultado = selectCommand.ExecuteReader();

            while (resultado.Read())
            {
                var alu = new AlunoDTO();
                alu.id = Convert.ToInt32(resultado["Id"]);
                alu.nome = Convert.ToString(resultado["Nome"]);
                alu.sobrenome = Convert.ToString(resultado["SobreNome"]);
                alu.telefone = Convert.ToString(resultado["Telefone"]);
                alu.ra = Convert.ToInt32(resultado["RA"]);
                alu.data = Convert.ToString(resultado["Data"]);
                listaAlunos.Add(alu);
            }
            if (conexao.State == ConnectionState.Open)
                conexao.Close();
            return listaAlunos;
        }

        public int DeletarAlunoDB(int id)
        {
            if (conexao.State == ConnectionState.Closed)
                conexao.Open();
            var total = ListarAlunosDbDelete(id);
            IDbCommand DeleteCommand = conexao.CreateCommand();
            DeleteCommand.CommandText = "delete from Aluno where id=@id";

            IDbDataParameter paramId = new SqlParameter("id", id);
            DeleteCommand.Parameters.Add(paramId);

            if (conexao.State == ConnectionState.Closed)
                conexao.Open();
            DeleteCommand.ExecuteScalar();

            if (conexao.State == ConnectionState.Open)
                conexao.Close();

            return total;

        }

        public int ListarAlunosDbDelete(int id)
        {
            if (conexao.State == ConnectionState.Closed)
                conexao.Open();
            var listaAlunos = new List<AlunoDTO>();
            IDbCommand selectCommand = conexao.CreateCommand();
            selectCommand.CommandText = $"select * from Aluno where id={id}";
            IDataReader resultado = selectCommand.ExecuteReader();

            while (resultado.Read())
            {
                var alu = new AlunoDTO();
                alu.id = Convert.ToInt32(resultado["Id"]);
                listaAlunos.Add(alu);
            }

            if (conexao.State == ConnectionState.Open)
                conexao.Close();
            return listaAlunos.Count;

        }
    }
}