using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcordiaLib.Domain;

namespace ConcordiaLib.Abstract;

public interface IReader
{
    IEnumerable<TEntity> QueryAsync<TEntity>(string query);
    Task<TEntity?> QueryOnceAsync<TEntity>(string query);
}

