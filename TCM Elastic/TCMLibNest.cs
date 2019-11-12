using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TCM_Elastic
{
    public class TCMLibNest
    {
        private ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"))
       .DefaultIndex("noticias").PrettyJson();

        private ElasticClient _elasticClient;

        public ElasticClient ElasticClient
        {
            get => _elasticClient ?? new ElasticClient(settings);
        }

        //Exemplo de Indexação de documento - Sincrono
        public IndexResponse IndexaNoticia(Noticia noticia)
        {
            return ElasticClient.IndexDocument(noticia);
        }

        //Exemplo de Indexação de documento - Assíncrono
        public async Task<IndexResponse> IndexaNoticiaAsync(Noticia noticia)
        {
            return await ElasticClient.IndexDocumentAsync(noticia);
            
        }

        //Exemplo de Busca
        public IReadOnlyCollection<Noticia> BuscarNoticiaPorAutor(string autor)
        {
            var searchResponse = ElasticClient.Search<Noticia>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                     .Match(m => m
                        .Field(f => f.NomeAutor)
                        .Query(autor)
                     )
                )
            );

            var resultadoBusca = searchResponse.Documents;
            return resultadoBusca;
            
        }

        public IReadOnlyCollection<Document> BuscarTermoBaseConhecimento(string termo, string indexName)
        {
            var searchResponse = ElasticClient.Search<Document>(s => s
              .Index(indexName)
              .Query(q => q
                .Match(m => m
                  .Field(a => a.Attachment.Content)
                  .Query(termo)
                )
              )
            );

            var resultadoBusca = searchResponse.Documents;
            return resultadoBusca;
            
        }

        public void IngestPipeline(string indexName)
        {
            ElasticClient.Indices.Create(indexName, c => c
                .Map<Document>(p => p
                    .AutoMap() 
                    .Properties(ps => ps
                    .Text(s => s
                      .Name(n => n.Path)
                      .Analyzer("windows_path_hierarchy_analyzer")
                    )
                    .Object<Attachment>(a => a
                      .Name(n => n.Attachment)
                      .AutoMap()) 
                    )
                )
            );


            ElasticClient.Ingest.PutPipeline("attachments", p => p
              .Description("Document attachment pipeline")
              .Processors(pr => pr
                .Attachment<Document>(a => a
                  .Field(f => f.Content)
                  .TargetField(f => f.Attachment)
                )
              )
            );
        }

        //Exemplo de Ingestão de Documentos (indexação de PDFs, DOCs, etc na base do elastic
        public void IngestKnowledgeBaseDocs(string path, string searchPattern, string indexName)
        {
            
            DirectoryInfo d = new DirectoryInfo(path);

            foreach (var file in d.GetFiles(searchPattern))
            {
                var docBaseConhecimento = new Document
                {
                    Id = Guid.NewGuid(),
                    Path = file.FullName,
                    Content = Convert.ToBase64String(File.ReadAllBytes(file.FullName))
                };

                var indexResponse = ElasticClient.Index(docBaseConhecimento, doc => doc.Index(indexName).Pipeline("attachments"));
            }
        }

        
    }
}
