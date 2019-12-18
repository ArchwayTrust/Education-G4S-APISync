using System.Collections.Generic;

namespace G4SApiSync.Client
{
    interface IAPIRequest<EndPoint, DTO>
        where EndPoint : IEndPoint<DTO>
    {
        List<DTO> ToList();
        string ReturnedJSON(int? Cursor);
    }
}
