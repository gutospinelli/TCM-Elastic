use TCMWEB

SELECT n.Noticia as NoticiaID
      ,c.Descricao as Categoria
      ,n.Autor as MatriculaAutor
	  ,f.Nome as NomeAutor
      ,n.Data_Atualizacao as AtualizadaEm
      ,n.Data_Publicacao as PublicadaEm
      ,n.Titulo
      ,n.Ementa
      ,s.Descricao as Status
      ,n.Arquivo
      ,n.Conteudo
  FROM Noticias as n
  left outer join Categorias c on n.Categoria = c.Categoria
  left outer join Status s on n.Status = s.Status
  left outer join vwFuncionarios f on n.Autor = f.matricula