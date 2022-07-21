using DeliveryUnitManager.Models;
using DeliveryUnitManager.Reponsitory.Models.Users;
using DeliveryUnitManager.Reponsitory.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using positionApi = DeliveryUnitManager.Reponsitory.Models.ApiModels.PositionApi;

namespace DeliveryUnitManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly PositionService _service;


        public PositionsController(PositionService service)
        {
            _service = service;
        }

        //GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<positionApi>>> GetPositions()
        {
            if ( await _service.GetAllAsync() == null)
            {
                return NotFound();
            }
            var positions = await _service.GetAllAsync();
            var listPositionApi = new List<positionApi>();
            foreach (var position in positions)
            {
                listPositionApi.Add(new positionApi(position));
            }
            return listPositionApi;
        }


        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<positionApi>> GetPositions(long id)
        {
            if ( await _service.GetAllAsync() == null)
            {
                return Ok(new TokenModel(false, "No data in storage"));
            }
            var positions = await _service.GetAsync(id);

            if (positions == null)
            {
                return Ok(new TokenModel(false, "Data not found"));
            }

            return new positionApi(positions);
        }

        // PUT: api/Positions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<TokenModel> PutPositions(positionApi positionApi)
        {
            if (positionApi.ID ==0 )
            {
                return new TokenModel(false, "Position Id is incorrect!");
            }
            var positions = await _service.GetAsync(positionApi.ID);
            if(positions == null)
            {
                return new TokenModel(false, "Position is not exit!");
            }
            positions.Code = positionApi.Code;
            positions.Name = positionApi.Name;
            positions.Description = positionApi.Description;
            positions.UpdateBy="testApi";
            positions.Updated = DateTime.Now;

            _service.Update(positions);

            try
            {
                await _service.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!PositionsExists(positionApi.ID))
                {
                    return new TokenModel(false,"Exception: Position is not exit!");
                }
                else
                {
                    return new TokenModel(false, ex.Message);
                }
            }

            return new TokenModel(true,"Position update succesfully");
        }

        // POST: api/Positions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<TokenModel> PostPositions(positionApi positions)
        {
            if (await _service.GetAllAsync() == null)
            {
                return new TokenModel(false,"Entity set 'DeliveryUnitDataContext.Positions'  is null.");
            }
            _service.AddNew(new Positions()
            {
                Name = positions.Name,
                Description = positions.Description,
                Code= positions.Code,
                Created= DateTime.Now,
                CreateBy="testApi",
                IsActive=true,

            });
            await _service.SaveChangesAsync();

            return new TokenModel(false, "Create position succesfully");
        }

        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        public async Task<TokenModel> DeletePositions(long id)
        {
            if ( await _service.GetAllAsync() == null)
            {
                return new TokenModel(false, "No data in storage");
            }
            var positions = await _service.GetAsync(id);
            if (positions == null)
            {
                return new TokenModel(false, "Data not found");
            }
            _service.Delete(id);
            await _service.SaveChangesAsync();

            return new TokenModel(true, "Data deleted successfully");
        }

        private bool PositionsExists(long id)
        {
            return (_service.GetAll()?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
