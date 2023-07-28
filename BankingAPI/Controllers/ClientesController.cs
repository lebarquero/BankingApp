using AutoMapper;
using BankingAPI.Business.DTOs.Cliente;
using BankingAPI.DataAccess;
using BankingAPI.DataAccess.Repositories.IRepositories;
using BankingAPI.Entities;
using BankingAPI.Infrastructure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IRepository<Cliente> _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ClientesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.GetRepository<Cliente>();
            _mapper = mapper;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientes()
        {
            var list = await _repo.GetAllAsync();
            return Ok(_mapper.Map<List<ClienteDTO>>(list));
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(int id)
        {
            var entity = await GetEntity(id);
            return Ok(_mapper.Map<ClienteDTO>(entity));
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteCreateDTO model)
        {
            if (model == null)
                throw new BankingAppException("Debe especificar los datos del nuevo elemento");

            var entity = _mapper.Map<Cliente>(model);
            await _repo.CreateAsync(entity);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction("GetCliente", new { id = entity.ID }, _mapper.Map<ClienteCreateDTO>(entity));
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteUpdateDTO model)
        {
            if (id != model.ID)
                throw new BankingAppException("Los identificadores deben de coincidir");

            if (model == null)
                throw new BankingAppException("Debe especificar los datos del elemento que desea modificar");

            var entity = _mapper.Map<Cliente>(model);
            await _repo.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCliente(int id, JsonPatchDocument<ClienteUpdateDTO> patchDTO)
        {
            if (patchDTO == null)
                throw new BankingAppException("Debe especificar los datos que desea modificar");

            var entity = await GetEntity(id);
            var dtoToBeUpdated = _mapper.Map<ClienteUpdateDTO>(entity);

            patchDTO.ApplyTo(dtoToBeUpdated, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entityUpdated = _mapper.Map<Cliente>(dtoToBeUpdated);
            await _repo.UpdateAsync(entityUpdated);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var entity = await GetEntity(id);
            await _repo.RemoveAsync(entity);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        private async Task<Cliente> GetEntity(int id)
        {
            if (id <= 0)
                throw new BankingAppException("El identificador debe ser un número entero positivo");

            var entity = await _repo.GetAsync(i => i.ID == id, tracked: false);

            if (entity == null)
                throw new KeyNotFoundException("No se encontró la información requerida");

            return entity;
        }
    }
}
