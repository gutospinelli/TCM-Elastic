# TCM-Elastic

This repo is my first attempt at the elastic stack, witch I've been using at work since 2019. It's a console application written in .NET to experiment with Elastic and some of its libs.

A more detailed implementation on using the Stack can be found at the Tcm.Elastic.Robo repo, witch is a public part of what I do at work. Since that code is being used actively in my day-to-day job, I chose to put it as a private repo. If you want to know a little bit more about that,  we can discuss particular access to that repo. 

Nonetheless, even this simple repo you're browsing now provides a good start view on how to use the elasticstack together with .NET libs such ElasticSearch.NET and NEST.

## Project Structure 

At root dir you can find the **TCM Elastic.sln** solution file. This file can be used in Visual Studio to open this project as a VS Solution
At TCM Elastic dir, you can find the classes that make this project alive:

- **Document.cs**	
  - A simple POCO class to represent a Document Attachment.
- **Noticia.cs**
  - Another simple POCO class to represent a News Article posted inside a website.
- **NoticiasSimple.sql**	
  - This class is a simple SQL Select statement to fetch News Articles from our simple News Database. Just for you to see how our news table is structured.
- **Program.cs**
  - The start point of our app (a.k.a the MAIN method is here!). It's very simple structured, in steps you can run by commenting and uncommenting lines (not the best of practices... I know...).
  - 0 : Just startes a stopwatch to keep track of how long the code takes to execute.
  - 1 : Creates our lib objects.
  - 2 : Indexes news articles in our elasticsearch instance/endpoint.
  - 3 : Calls simple search methods using ElasticSearch.NET and NEST libs.
  - 4 : Exports news articles as JSON file.
  - 5 : Indexes News Articles Attachments (Documents) by ingesting a pipeline to elasticSearch. Also extends a little bit more our search by searching attached documents and performing highlight search.
- **RoboMigracao.cs**
  - A class with methods to fetch news from our database and save them in Json files.
- **TCMLibNest.cs**
  - My NEST implementation DE FACTO. Properly speaking, my initial experimentation with nest and my elasticsearch instance.
- **TcmLibElastic.cs**
  - My ElasticSearch.NET implementation DE FACTO. Properly speaking, my initial experimentation with elasticsearch.net and my elasticsearch instance.
