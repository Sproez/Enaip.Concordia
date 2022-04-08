using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaLib.Abstract;

using Domain;

public interface IDatabaseMiddleware
{
    void UpdateCard(Card card);
    List<Card> GetAllCards();
    CardList GetList(string listName);
    void MoveCard(Card card, CardList destination);
}

