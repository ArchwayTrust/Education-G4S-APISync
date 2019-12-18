using System.Collections.Generic;
using System.Threading.Tasks;

namespace G4SApiSync.Client
{
    public interface IEndPoint<T>
    {
        string EndPoint { get; }

        IEnumerable<T> DTOs { get; set; }

        bool HasMore { get; set; }
        int? Cursor { get; set; }

        Task<bool> UpdateDatabase(string APIKey, string AcYear, string AcademyCode);

    }
}
