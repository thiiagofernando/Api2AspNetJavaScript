﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Newtonsoft.Json;

namespace WebApp01.Models
{
    public class Aluno
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
        public string data { get; set; }
        public int ra { get; set; }

        public List<Aluno> ListarAlunos()
        {
            var caminho = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = File.ReadAllText(caminho);

            var listaAlunos = JsonConvert.DeserializeObject<List<Aluno>>(json);

            return listaAlunos;
        }

        public bool RescreverArquivo(List<Aluno> listaAlunos)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            File.WriteAllText(caminhoArquivo,json);
            return true;
        }

        public Aluno Salvar(Aluno aluno)
        {
            var listaAlunos = this.ListarAlunos();
            var maxId = listaAlunos.Max(p => p.id);
            listaAlunos.Add(aluno);
            RescreverArquivo(listaAlunos);
            return aluno;
        }

        public Aluno Atualizar(int id,Aluno Aluno)
        {
            var listaAlunos = this.ListarAlunos();
            var itemIdex = listaAlunos.FindIndex(p => p.id == id);
            if (itemIdex >=0)
            {
                Aluno.id = id;
                listaAlunos[itemIdex] = Aluno;
            }
            else
            {
                return null;
            }

            RescreverArquivo(listaAlunos);
            return Aluno;
        }

        public bool Deletar(int id)
        {
            var listaAluno = this.ListarAlunos();
            var itemIndex = listaAluno.FindIndex(p => p.id == id);
            if (itemIndex >=0)
            {
                listaAluno.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }

            RescreverArquivo(listaAluno);
            return true;
        }

        public Aluno Inserir(Aluno Aluno)
        {
            var listaAlunos = this.ListarAlunos();
            var maxId = listaAlunos.Max(aluno => aluno.id);
            Aluno.id = maxId + 1;
            listaAlunos.Add(Aluno);
            RescreverArquivo(listaAlunos);
            return Aluno;
        }

    }
}