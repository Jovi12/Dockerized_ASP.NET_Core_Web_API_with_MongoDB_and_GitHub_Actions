using CrudOperationWithMongodb.DataAccessLayer;
using CrudOperationWithMongodb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperationWithMongodb.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly ICrudoperations _Crudoperations;

        public CRUDController(ICrudoperations crudoperations)
        {
            _Crudoperations = crudoperations;
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecord( InsertRecordRequest request)
        {
            InsertRecordResponse response = new InsertRecordResponse();
            try
            {
               
                if (request == null)
                {
                    return BadRequest("Request cannot be null");
                }
                
               response = await _Crudoperations.InsertRecord(request);
               if(response!=null)
                {
                    response.IsSuccess = true;
                    response.Message = "Record Inserted Successfully";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;


            }
            
             return Ok(response);
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRecord()
        {   
            GetAllRecordResponse response = new GetAllRecordResponse();
            try
            {
                response = await _Crudoperations.GetAllRecord();
                if(response== null)
                {
                    return BadRequest("bad request");
                }
            }
            catch(Exception ex)
            {
                response.IsSuccess= false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecordByName([FromQuery]string name)
        {
            GetRecordbyName response= new GetRecordbyName();
            try
            {
                response= await _Crudoperations.GetRecordByName(name);
            }
            catch(Exception ex)
            {
                response.IsSuccess= false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetRecordById([FromQuery] string id)
        {
            GetRecordbyId response = new GetRecordbyId();
            try
            {
                response = await _Crudoperations.GetRecordbyId(id);
            }
            catch(Exception ex)
            {
                response.IsSuccess= false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        [HttpPut]

        public async Task<IActionResult> UpdateRecordById(UpdateRecordRequest request)
        {
            UpdateRecordResponse response= new UpdateRecordResponse();
            try
            {
                response=await _Crudoperations.UpdateRecord(request);
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;  
            }
            return Ok(response);
        }

        [HttpPatch]
        public async Task<IActionResult> PatchRecord(UpdateRecordRequest request)
        {
            UpdateRecordResponse response= new UpdateRecordResponse();
            try
            {
                response= await _Crudoperations.PatchRecordAsync(request);
            }
            catch(Exception ex)
            {
                response.IsSuccess= false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
    }
}
