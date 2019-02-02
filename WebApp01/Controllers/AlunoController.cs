using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp01.Models;

namespace WebApp01.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Aluno")]
    public class AlunoController : ApiController
    {
        [HttpGet]
        [Route("Recuperar")]
        public IHttpActionResult Recuperar()
        {
            try
            {
                Aluno aluno = new Aluno();
                return Ok(aluno.ListarTodosAlunos());
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro {ex.Message}");
            }
        }

        [HttpGet]
        [Route("Recuperar/{id}/{nome?}/{sobrenome?}")]
        public List<AlunoDTO> Get(int id,string nome = null,string sobrenome = null)
        {

            try
            {
                Aluno aluno = new Aluno();
                return aluno.ListarAlunosIdNomeSobreNome(id, nome, sobrenome);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro  {ex.Message}");
            }
        }

        [HttpPost]
        public List<AlunoDTO> Post(AlunoDTO aluno)
        {

            try
            {
                Aluno _aluno = new Aluno();
                _aluno.Inserir(aluno);
                return _aluno.ListarTodosAlunos();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro  {ex.Message}");
            }
        }

        [HttpPut]
        public AlunoDTO Put(int id, [FromBody]AlunoDTO aluno)
        {
            try
            {
                Aluno _aluno = new Aluno();
                return _aluno.Atualizar(id, aluno);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro  {ex.Message}");
            }
        }

        [HttpDelete]
        public string Delete(int id)
        {
            try
            {
                Aluno _aluno = new Aluno();
                var deletado = _aluno.Deletar(id);
                return $"Total Exluido: {deletado}";
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao excluir aluno {ex.Message}");
            }
        }
    }
}
