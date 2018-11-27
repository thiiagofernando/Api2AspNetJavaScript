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
        public AlunoDAO()
        {
            string strConecao = ConfigurationManager.ConnectionStrings["ConexaoBD"].ConnectionString;
            IDbConnection conexao;
            conexao = new SqlConnection(strConecao);
            conexao.Open();
        }
    }
}