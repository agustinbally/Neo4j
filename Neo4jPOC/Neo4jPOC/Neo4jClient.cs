using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neo4jPOC
{
    public class Neo4jClient
    {
        private static GraphClient _client;
        public static void Create()
        {
            _client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            _client.Connect();         _
        }

        public static void BorrarNodo(EntidadConId entidad)
        {
            _client.Cypher
                .Match(string.Format("(nodo:{0})", entidad.GetType().Name))
                .Where((EntidadConId nodo) => nodo.Id == entidad.Id)
                .Delete("nodo")
                .ExecuteWithoutResults();
        }

        public static void CrearNodo(object nodo)
        {
            _client.Cypher
                .Create(string.Format("(nodo:{0} {{newNodo}})", nodo.GetType().Name))
                .WithParam("newNodo", nodo)
                .ExecuteWithoutResults();
        }

        public static TNodo ObtenerNodo<TNodo>(long id)
        {
            return _client.Cypher
                .Match(string.Format("(nodo:{0})", typeof(TNodo).Name))
                .Where((EntidadConId nodo) => nodo.Id == id)
                .Return(nodo => nodo.As<TNodo>())
                .Results.FirstOrDefault();
        }

        public static IEnumerable<TNodo> ObtenerTodos<TNodo>()
        {
            return _client.Cypher
                .Match(string.Format("(nodo:{0})", typeof(TNodo).Name))
                .Return(nodo => nodo.As<TNodo>())
                .Results;
        }
    }
}
