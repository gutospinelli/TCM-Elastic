using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace TCM_Elastic
{
    public class RoboMigracao
    {
        

        public List<Noticia> RetornaNoticiasTCMWeb()
        {
            List<Noticia> noticias = new List<Noticia>();

            string connectionString = @"Server=tcmbd3\dev;Database=tcmweb;Integrated Security=True;MultipleActiveResultSets=True";
            
            string queryString =
                @"SELECT n.Noticia as NoticiaID
                  ,c.Descricao as Categoria
                  ,n.Autor as MatriculaAutor
	              ,f.Nome as NomeAutor
                  ,n.Data_Atualizacao as AtualizadaEm
                  ,n.Data_Publicacao as PublicadaEm
                  ,n.Titulo
                  ,n.Ementa
                  ,s.Descricao as Status
              FROM Noticias as n
              left outer join Categorias c on n.Categoria = c.Categoria
              left outer join Status s on n.Status = s.Status
              left outer join vwFuncionarios f on n.Autor = f.matricula";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                
                SqlCommand command = new SqlCommand(queryString, connection);
                              
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        noticias.Add(new Noticia
                        {
                            Id = (int)reader[0],
                            Categoria = (string)reader[1],
                            MatriculaAutor = (int)reader[2],
                            NomeAutor = (string)reader[3],
                            AtualizadaEm = (DateTime)reader[4],
                            PublicadaEm = (DateTime)reader[5],
                            Titulo = (string)reader[6],
                            Ementa = (string)reader[7]
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return noticias;
        }

        public void SalvaNoticiasJson()
        {
            var todasNoticiasEmUmaLista = RetornaNoticiasTCMWeb();
            int numeroListas = 2;

            var noticiasNListas = todasNoticiasEmUmaLista.Select((s, i) => new { s, i })
                                  .GroupBy(x => x.i % numeroListas)
                                  .Select(g => g.Select(x => x.s).ToList())
                                  .ToList();


            string path = @"c:\temp\";

            for (int i = 0; i < numeroListas; i++)
            {
                path = @"c:\temp\noticias" + i + ".json";
                // This text is added only once to the file.
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    string createText = "Lista" + i + " de " + numeroListas + Environment.NewLine;
                    File.WriteAllText(path, createText);
                }

                // This text is always added, making the file longer over time
                // if it is not deleted.
                string appendText = JsonConvert.SerializeObject(noticiasNListas[i]);
                File.AppendAllText(path, appendText);


            }

            
            //// Open the file to read from.
            //string readText = File.ReadAllText(path);
            //Console.WriteLine(readText);

        }
    }
}
