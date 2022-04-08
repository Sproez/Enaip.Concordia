using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcordiaLib.Abstract;
using ConcordiaLib.Domain;

namespace ConcordiaLib.Middleware;

public class DatabaseMiddleware : IDatabaseMiddleware
{

    private readonly IReader _reader;
    private readonly IWriter _writer;


    public DatabaseMiddleware(IReader reader, IWriter writer)
    {
        _reader = reader;
        _writer = writer;
    }

    public void UpdateCard(Card card)
    {
        var query = @"UPDATE Cards
                      SET Title=@Title,Description=@Description,DueBy=@DueBy,Priority=@Priority
                      WHERE (Id=@Id)";

        var parameters = new List<(string, object?)>
        {
            ("@Id", card.Id),
            ("@Title", card.Title),
            ("@Description", card.Description),
            ("@DueBy", DateTime.Now),
            ("@Priority", (int)card.Priority),
        };

        //Update assignments

        //Update comment
        if (card.LastComment is not null) CreateOrUpdateComment(card.LastComment);
        //Update card
        _writer.WriteAsync(query, parameters);

    }

    public List<Card> GetAllCards()
    {
        throw new NotImplementedException();
    }

    public CardList GetList(string listName)
    {
        throw new NotImplementedException();
    }

    public void MoveCard(Card card, CardList destination)
    {
        throw new NotImplementedException();
    }

    //TODO create first comment if there is none
    private void CreateOrUpdateComment(Comment comment)
    {
        var query = @"UPDATE Comment
                      SET Text=@Text, CreatedAt=@CreatedAt, IdPerson=@IdPerson
                      WHERE (IdCard=@IdCard)";

        var parameters = new List<(string, object?)>
        {
            ("@IdCard", comment.IdCard),

            ("@Text", comment.Text),
            ("@CreatedAt", comment.CreatedAt),
            ("@IdPerson", comment.IdPerson)
        };

        _writer.WriteAsync(query, parameters);
    }

    private void UpdateAssignmentsOnCard(Card card)
    {
        var deletionQuery = @"DELETE FROM Assignments
                              WHERE (IdCard=@IdCard)";

        var parametersDeletion = new List<(string, object?)>
        {
            ("@IdCard", card.Id)
        };

        _writer.WriteAsync(deletionQuery, parametersDeletion);

        //TODO all writes in a single query
        var creationQuery = @"INSERT INTO Assignments(IdCard, IdPerson)
                             VALUES (@IdCard, @IdPerson)";
        var parametersCreation = new List<(string, object?)>();

        foreach (var person in card.Assignees)
        {
            parametersCreation.Clear();
            parametersCreation.Add(("@IdCard", card.Id));
            parametersCreation.Add(("@IdPerson", person.Id));
            _writer.WriteAsync(creationQuery, parametersCreation);
        }

    }
    

    #region ROBA_VECCHIA
    /*
    public IAsyncEnumerable<Card> GetAllCards()
    {
        const string query = @"SELECT M.Id, M.Title, M.Body, M.CreatedAt, M.AuthorId, M.UpdatedAt,
                               A.Id, A.Name, A.Surname, A.Mail, A.CreatedAt
                               FROM Message M JOIN Author A
                               ON A.Id = M.AuthorId";

        return _reader.QueryAsync(query, MapMessage);
    }

    public Task<Comment?> GetById(int id)
    {
        var query = $@"SELECT M.Id, M.Title, M.Body, M.CreatedAt, M.AuthorId, M.UpdatedAt,
                       A.Id, A.Name, A.Surname, A.Mail, A.CreatedAt
                       FROM Message M JOIN Author A
                       ON A.Id = M.AuthorId
                       WHERE M.Id={id}";

        return _reader.SingleQueryAsync(query, MapMessage);
    }

    public object MapMessage { get; set; }

    public Task<bool> Create(Message message)
    {
        var query = @"INSERT INTO Message(Title, Body, CreatedAt, AuthorId)
                      VALUES (@Title, @Body, @CreatedAt, @AuthorId)";

        var parameters = new List<(string, object?)>
        {
            ("@Title", message.Title),
            ("@Body", message.Body),
            ("@CreatedAt", DateTime.Now),
            ("@AuthorId", message.AuthorId)
        };

        return _writer.WriteAsync(query, parameters);
    }

    public Task<bool> Update(Message message)
    {
        var query = @"UPDATE Message
                      SET Title=@Title,Body=@Body,UpdatedAt=@UpdatedAt,AuthorId=@AuthorId
                      WHERE (Id=@Id)";

        var parameters = new List<(string, object?)>
        {
            ("@Title", message.Title),
            ("@Body", message.Body),
            ("@UpdatedAt", DateTime.Now),
            ("@AuthorId", message.AuthorId),
            ("@Id", message.Id)
        };

        return _writer.WriteAsync(query, parameters);
    }

    //public Task<bool> Delete(int id)
    //{
    //    const string query = @"DELETE FROM Message
    //                  WHERE (Id=@Id)";

    //    return _writer.WriteAsync(query, new[] { ("@Id", (object?)id) });
    //}

    private static Message MapCard(IDataReader dr)
    {
        return new()
        {
            Id = dr["id"].ToString()!,
            Title = dr["title"].ToString()!,
            Description = dr["description"].ToString()!,
            CreatedAt = dr["createdAt"] as DateTime?,
            UpdatedAt = dr["updatedAt"] as DateTime?,

            //{
            //    Id = dr["authorId"] as int?,
            //    Name = dr["name"].ToString()!,
            //    Surname = dr["surname"].ToString()!
            //}
        };
    }
    */
    #endregion

}

