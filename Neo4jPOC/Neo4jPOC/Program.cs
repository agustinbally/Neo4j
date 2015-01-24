using System;
using System.Collections.Generic;
using Neo4jClient;

namespace Neo4jPOC
{
    class Program
    {
        static void Main()
        {
            var user = new User { Id = 1, Age = 33, Name = "Agustín", Email = "asd@pp.com" };

            var client = CreateGraphClient();
            
            DeleteUser(client, user.Id);            
            CreateUser(client, user);
            var allUsers = GetAllUsers(client);

            foreach (var u in allUsers)
            {
                Console.WriteLine("{0}:{1}:{2}:{3}", u.Id, u.Name, u.Age, u.Email);
            }

            Console.ReadKey();
        }

        private static void DeleteUser(ICypherGraphClient client, long id)
        {
            client.Cypher
                .Match("(user:User)")
                .Where((User user) => user.Id == id)
                .Delete("user")
                .ExecuteWithoutResults();
        }

        private static GraphClient CreateGraphClient()
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.Connect();

            return client;
        }

        private static void CreateUser(ICypherGraphClient cypherGraphClient, User user)
        {
            cypherGraphClient.Cypher
                .Create("(user:User {newUser})")
                .WithParam("newUser", user)
                .ExecuteWithoutResults();
        }

        private static IEnumerable<User> GetAllUsers(ICypherGraphClient cypherGraphClient)
        {
            return cypherGraphClient.Cypher
                .Match("(user:User)")
                .Return(user => user.As<User>())
                .Results;
        }
    }

    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
