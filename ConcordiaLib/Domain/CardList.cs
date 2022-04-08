using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaLib.Domain
{
    public class CardList
    {
        public string Name { get; set; }
        public List<Card> Cards { get; } = new List<Card>();

        public CardList(string name)
        {
            Name = name;
        }

        public CardList(IDataReader dr)
        {
            Name = "DEFAULT"; //TODO query
        }
    }
}
