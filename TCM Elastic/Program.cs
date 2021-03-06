﻿using Elasticsearch.Net;
using System;
using System.Diagnostics;

namespace TCM_Elastic
{
    class Program
    {
        static void Main(string[] args)
        {
            //0 - Starta os testes
            Console.WriteLine("Testes ElasticSearch Augusto");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //1 - Instancia libs
            RoboMigracao robo = new RoboMigracao();
            TcmLibElastic lib = new TcmLibElastic();
            TCMLibNest nest = new TCMLibNest();

            //2 - Indexa noticias do WEBNEWS usando ElasticSearch.NET
            //lib.IndexaNoticiasBulk(robo.RetornaNoticiasTCMWeb());

            //2.1 - Indexa noticias do WEBNEWS usando NEST
            var noticiasBD = robo.RetornaNoticiasTCMWeb();
            foreach (var noticia in noticiasBD)
            {
                var reponse = nest.IndexaNoticia(noticia);
                Console.WriteLine("Noticia: " + noticia.Id + " Resultado: " + reponse.Result.ToString());
            }

            //3 - Realiza uma busca com ELasticSearch.NET            
            //Console.WriteLine("Você deseja buscar notícias do: ");
            //string nome = Console.ReadLine();
            //string resposta = lib.BuscaAutoresNoticias(nome);
            //Console.WriteLine(resposta);

            //3.1 - Realiza uma busca com NEST
            //Console.WriteLine("Você deseja buscar notícias do: ");
            //string nome = Console.ReadLine();
            //var resultado = nest.BuscarNoticiaPorAutor(nome);

            //foreach (Noticia n in resultado)
            //{
            //    Console.WriteLine("http://www.tcm.rj.gov.br/WEB/Site/Noticia_Detalhe.aspx?noticia=" + n.Id);
            //}

            //4 - Cria JSON com todas noticias em c:\temp\
            //robo.SalvaNoticiasJson();

            //5 -Cria Índice de Documentos Word
            //var indexName = "conhecimento_v2";
            //nest.IngestPipeline(indexName);

            //var path = @"C:\tmp\es";
            //var searchPattern = @"*.docx";
            //nest.IngestKnowledgeBaseDocs(path, searchPattern, indexName);

            //5.1 - Realiza uma busca dentro de Documentos DOCX com NEST
            //Console.WriteLine("O que deseja buscar na base de conhecimento? ");
            //string termo = Console.ReadLine();

            //5.1.1 - Busca todo Doc
            //var resultado = nest.BuscarTermoBaseConhecimento(termo, indexName);

            //if (resultado.Count == 0)
            //{
            //    Console.WriteLine("Nada encontrado!");
            //}
            //else
            //{
            //    Console.WriteLine("Encontrado em: " + resultado.Count + "documentos! (MAX 10)");
            //}

            //foreach (Document n in resultado)
            //{
            //    Console.WriteLine("===================================================================");
            //    Console.WriteLine("Termo encontrado no Documento: " + n.Path);
            //    Console.WriteLine("========================== INICIO ================================");
            //    Console.WriteLine("Conteudo");
            //    Console.WriteLine(n.Attachment.Content);
            //    Console.WriteLine("=========================== FIM ==================================");
            //    Console.WriteLine("");
            //    Console.WriteLine("");
            //}

            //5.2.2 - Busca por highlights
            //var resultado = nest.BuscarTermoBaseConhecimentoHighlights(termo, indexName);
            //foreach (var hit in resultado)
            //{
            //    foreach (var highlightField in hit.Highlight)
            //    {
            //        foreach (var highlight in highlightField.Value)
            //        {
            //            Console.WriteLine("===================================================================");
            //            Console.WriteLine(highlight);
            //            Console.WriteLine("");Console.WriteLine("");
            //        }
                    
            //    }
            //}


            Console.WriteLine("Processado em {0}", stopwatch.Elapsed.ToString());
            stopwatch.Stop();


        }

        
    }
}
