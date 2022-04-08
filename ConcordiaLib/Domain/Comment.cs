using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaLib.Domain;

public class Comment
{
    public string Text { get; }
    public DateTime CreatedAt { get; }

    public string IdCard { get; }
    public string IdPerson { get; }

    public Comment(string text, DateTime createdAt)
    {
        Text = text;
        CreatedAt = createdAt;
    }

    public Comment(IDataReader dr)
    {
        Text = (string)dr["Comment.Text"];
        CreatedAt = (DateTime)dr["Comment.CreationDate"];
    }
}

