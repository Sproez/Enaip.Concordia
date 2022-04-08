using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcordiaLib.Enums;

namespace ConcordiaLib.Domain;

public class Card
{
    public string Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Comment? LastComment { get; set; }
    public DateTime? DueBy { get; set; }
    public Priority Priority { get; set; }
    public List<Person> Assignees { get; set; }
    public CardList List { get; set; }
    /*
    public Card(string id, string title, string description, Comment lastComment, DateTime dueBy,
        Priority priority, List<Person> assignees, CardList list)
    {
    }
    */

    public Card(IDataReader dr)
    {
        Id = (string)dr["Card.Id"];
        Title = (string)dr["Card.Title"];
        Description = (string)dr["Card.Description"];

        Comment LastComment = dr["Comment.Id"] is not null ? new Comment(dr) : null;

        DueBy = dr["Card.DueBy"] as DateTime?;
        //TODO check enum values
        Priority = (Priority)dr["Card.Priority"];

        Assignees = null; //TODO query
        List = (CardList)dr["Card.List"];
    }

}