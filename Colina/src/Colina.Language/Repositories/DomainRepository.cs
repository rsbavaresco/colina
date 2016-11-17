using Colina.Infrastructure.DataAccess;
using Colina.Language.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Writing.Formatting;

namespace Colina.Language.Repositories
{
    public class DomainRepository : IDomainRepository
    {
        private readonly OntologySettings _ontologySettings;
        public DomainRepository(OntologySettings ontologySettings)
        {
            _ontologySettings = ontologySettings;
        }

        public void RetrieveCommands()
        {
            IGraph graph = new Graph();            
            RdfXmlParser rdfParser = new RdfXmlParser();

            //Load using a Filename
            rdfParser.Load(graph, _ontologySettings.OwlPath);

            InMemoryDataset ds = new InMemoryDataset(graph);

            //Get the Query processor
            ISparqlQueryProcessor processor = new LeviathanQueryProcessor(ds);

            //Create a Parameterized String
            SparqlParameterizedString queryString = new SparqlParameterizedString();

            //Add a namespace declaration
            queryString.Namespaces.AddNamespace("ns", new Uri("https://github.com/rsbavaresco/colina/ontology#"));

            //Set the SPARQL command
            //For more complex queries we can do this in multiple lines by using += on the
            //CommandText property
            //Note we can use @name style parameters here
            //queryString.CommandText = "SELECT * WHERE { ?s ns:property @value }";
            queryString.CommandText = "SELECT DISTINCT ?class WHERE { ?s a ?class . } LIMIT 25 OFFSET 0";
            
            //When we call ToString() we get the full command text with namespaces appended as PREFIX
            //declarations and any parameters replaced with their declared values
            Console.WriteLine(queryString.ToString());

            //We can turn this into a query by parsing it as in our previous example
            SparqlQueryParser parser = new SparqlQueryParser();
            SparqlQuery query = parser.ParseFromString(queryString);

            var results = processor.ProcessQuery(query);
            if (results is IGraph)
            {
                //Print out the Results
                IGraph g = (IGraph)results;
                NTriplesFormatter formatter = new NTriplesFormatter();
                foreach (Triple t in g.Triples)
                {
                    Console.WriteLine(t.ToString(formatter));
                }
                Console.WriteLine("Query took " + query.QueryExecutionTime + " milliseconds");
            }
        }
    }
}
