using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;

namespace Neo4jPOC
{
    class Program
    {
        static void Main()
        {
            var user = new User { Id = 1, Age = 10, Name = "Agustín", Email = "asd@pp.com" };

            var client = CreateGraphClient();
            
            BorrarNodo(client, user);
            CrearNodo(client, user);

            var usuarios = ObtenerTodos<User>(client);

            foreach (var u in usuarios)
            {
                Console.WriteLine("{0}:{1}:{2}:{3}", u.Id, u.Name, u.Age, u.Email);
            }

            var usuario = ObtenerNodo<User>(client, user.Id);
            Console.WriteLine("{0}:{1}:{2}:{3}", usuario.Id, usuario.Name, usuario.Age, usuario.Email);

            Console.ReadKey();
        }        

        

        
    }

    public class EntidadConId
    {
        public long Id { get; set; }
    }

    public class User : EntidadConId
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
