using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaLib.Domain
{
    public class Person
    {
        public string Id { get; init; }
        public string Name { get; set; }

        /*
        public Person(string id, string name) {
            Id = id;
            Name = name;
        }
        */

        public Person(IDataReader dr)
        {
            Id = (string)dr["Person.Id"];
            Name = (string)dr["Person.Name"];
        }
    }
}
