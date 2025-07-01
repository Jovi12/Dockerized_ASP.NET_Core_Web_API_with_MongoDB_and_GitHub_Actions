using CrudOperationWithMongodb.Models;

namespace CrudOperationWithMongodb.DataAccessLayer
{
    public interface ICrudoperations
    {
        public Task<InsertRecordResponse> InsertRecord(InsertRecordRequest request);

        public Task<GetAllRecordResponse> GetAllRecord();

        public Task<GetRecordbyName> GetRecordByName(string name);

        public Task<GetRecordbyId> GetRecordbyId(string id);

        public Task<UpdateRecordResponse> UpdateRecord(UpdateRecordRequest request);

        public Task<UpdateRecordResponse> PatchRecordAsync(UpdateRecordRequest request);

    }
}
