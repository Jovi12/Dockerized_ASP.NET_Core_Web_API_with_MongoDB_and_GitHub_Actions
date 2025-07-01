using CrudOperationWithMongodb.Models;
using CrudOperationWithMongodb.MongodbConfiguration;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CrudOperationWithMongodb.DataAccessLayer
{
    public class Crudoperations:ICrudoperations
    {
        
       private readonly MongodbService _mongodbService;
        public Crudoperations(MongodbService mongodbService)
        {
           _mongodbService = mongodbService;  
        }

        public async Task<GetAllRecordResponse> GetAllRecord()
        { 
            GetAllRecordResponse response = new GetAllRecordResponse();
            response.IsSuccess = true;
            response.Message = "Fetch Records Successfull";
            try
            {
                response.data = new List<InsertRecordRequest>();
                response.data = await _mongodbService.User.Find(x => true).ToListAsync();
                if(response.data.Count == 0)
                {
                    response.Message = "no records found";

                }
            }
            catch(Exception ex)
            {
                response.IsSuccess= false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<GetRecordbyName> GetRecordByName(string name)
        {
            GetRecordbyName response = new GetRecordbyName();
            response.IsSuccess = true;
            response.Message = "Fetch Record Success";
            try
            {
                response.data = new List<InsertRecordRequest>();
                response.data = await _mongodbService.User.Find(x => (x.FirstName == name ||x.LastName==name)).ToListAsync();
               
                if(response.data.Count ==0)
                {
                    response.Message = "No record found";
                }
            }
            catch( Exception ex )
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
          
            return response;
        }

        public async  Task<InsertRecordResponse> InsertRecord(InsertRecordRequest request)
        {   InsertRecordResponse response = new InsertRecordResponse();
            try
            {
                request.CreatedDate = DateTime.Now.ToString();
                request.UpdatedDate = string.Empty;

                await _mongodbService.User.InsertOneAsync(request);
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<GetRecordbyId> GetRecordbyId( string id)
        {
            GetRecordbyId response = new GetRecordbyId();
            response.IsSuccess = true;
            response.Message = "Fetch Record Success";
            try
            {

                response.data = await _mongodbService.User.Find(x=>x.Id == id).FirstOrDefaultAsync();

                if(response.data==null)
                {
                    response.Message = "No Record found";
                }

            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<UpdateRecordResponse> UpdateRecord(UpdateRecordRequest request)
        {
            UpdateRecordResponse response = new UpdateRecordResponse();
            InsertRecordRequest requestinsert= new InsertRecordRequest();
            response.IsSuccess = true;
            response.Message = "Updated Successfull";
            try
            {   
                //fetch id
                GetRecordbyId getRecordbyId= await GetRecordbyId(request.id);
                //created date
                requestinsert.CreatedDate= getRecordbyId.data.CreatedDate;
                requestinsert.UpdatedDate = DateTime.Now.ToString();
                requestinsert.Id=request.id;
                requestinsert.FirstName = request.FirstName;
                requestinsert.LastName = request.LastName;
                requestinsert.Age = request.Age;
                requestinsert.Contact= request.Contact;
                requestinsert.Salary= request.Salary;
                var result = await _mongodbService.User.ReplaceOneAsync(x => x.Id == requestinsert.Id, requestinsert);

                if(!result.IsAcknowledged)
                {
                    response.Message = "update is not perform";
                }
            }
            catch( Exception ex)
            {
                response.IsSuccess= false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<UpdateRecordResponse> PatchRecordAsync(UpdateRecordRequest request)
        {
            var response = new UpdateRecordResponse
            {
                IsSuccess = true,
                Message = "Updated successfully"
            };

            try
            {
                // Fetch the existing record
                var existingRecord = await GetRecordbyId(request.id);
                if (existingRecord == null || existingRecord.data == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Record not found";
                    return response;
                }

                var updateDef = Builders<InsertRecordRequest>.Update
                    .Set(x => x.UpdatedDate, DateTime.Now.ToString());

                if (!string.IsNullOrWhiteSpace(request.FirstName))
                    updateDef = updateDef.Set(x => x.FirstName, request.FirstName);

                if (!string.IsNullOrWhiteSpace(request.LastName))
                    updateDef = updateDef.Set(x => x.LastName, request.LastName);

                if (request.Age > 0)
                    updateDef = updateDef.Set(x => x.Age, request.Age);

                if (!string.IsNullOrWhiteSpace(request.Contact))
                    updateDef = updateDef.Set(x => x.Contact, request.Contact);

                if (request.Salary > 0)
                    updateDef = updateDef.Set(x => x.Salary, request.Salary);

                var result = await _mongodbService.User.UpdateOneAsync(x => x.Id == request.id, updateDef);

                if (!result.IsAcknowledged || result.ModifiedCount == 0)
                {
                    response.Message = "No records were updated";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

    }
}
