using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCM_Elastic
{
    public class TcmLibElastic
    {
        private ElasticLowLevelClient _elasticLowLevelClient;

        private ConnectionConfiguration connectionConfiguration = new ConnectionConfiguration()
        .PrettyJson()
        .RequestTimeout(TimeSpan.FromMinutes(2));

        public ElasticLowLevelClient ElasticLowLevelClient { 
            get => _elasticLowLevelClient ?? new ElasticLowLevelClient(connectionConfiguration);
        }

        public async System.Threading.Tasks.Task<string> IndexaNoticiasTCMWebAsync(List<Noticia> noticias)
        {
            

            foreach (var noticia in noticias)
            {
                var asyncIndexResponse = await ElasticLowLevelClient.IndexAsync<StringResponse>("noticias", noticia.Id.ToString(), PostData.Serializable(noticia));
            }

            return "Sucesso";
        }

        public string IndexaNoticiasSync(List<Noticia> noticias)
        {
            
            foreach (var noticia in noticias)
            {
                var ndexResponse = ElasticLowLevelClient.Index<BytesResponse>("news", noticia.Id.ToString(), PostData.Serializable(noticia)); 
            }
            
            return "Sucesso";
        }

        public string IndexaNoticiasBulk(List<Noticia> noticias)
        {
            List<object> noticiasIndexadas = new List<object>();

            int i = 1;
            foreach (var noticia in noticias)
            {
                noticiasIndexadas.Add(new { index = new { _index = "noticias", _type = "noticia", _id = i }, noticia });
                i++;
            }

            var response = ElasticLowLevelClient.Bulk<StringResponse>(PostData.MultiJson(noticiasIndexadas));
            return response.Body;
        }

        public string BuscaAutoresNoticias(string nome)
        {
            var searchResponse = ElasticLowLevelClient.Search<StringResponse>("noticias", @"
            {
                ""from"": 0,
                ""size"": 10,
                ""query"": {
                    ""match"": {
                        ""noticia.NomeAutor"" : ""Leonardo""
                    }
                }
            }");

            return searchResponse.Body;
        }

    }
}
