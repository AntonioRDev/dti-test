using ProvaDTI.Models;
using System.Collections.Generic;

namespace ProvaDTI.Databases
{
    public static class FakeQueryDatabase
    {
        private static List<Query> Queries { get; set; } = new List<Query>();

        public static Query GetQueryById(int id)
        {
            return Queries.Find(query => query.Id == id);
        }

        public static List<Query> GetAllQueries()
        {
            return Queries;
        }

        public static void SaveQuery(Query query)
        {
            Queries.Add(query);
        }

        public static int GetLength()
        {
            return Queries.Count;
        }
    }
}
