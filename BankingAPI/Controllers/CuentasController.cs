using AutoMapper;
using BankingAPI.Business.DTOs.Cuenta;
using BankingAPI.Business.Exceptions;
using BankingAPI.DataAccess;
using BankingAPI.DataAccess.Repositories.IRepositories;
using BankingAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly IRepository<Cuenta> _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CuentasController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.GetRepository<Cuenta>();
            _mapper = mapper;
        }

        // GET: api/Cuentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentaDTO>>> GetCuentas()
        {
            var list = await _repo.GetAllAsync();
            return Ok(_mapper.Map<List<CuentaDTO>>(list));
        }

        // GET: api/Cuentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CuentaDTO>> GetCuenta(string id)
        {
            var entity = await GetEntity(id);
            return Ok(_mapper.Map<CuentaDTO>(entity));
        }

        // PUT: api/Cuentas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuenta(string id, CuentaUpdateDTO model)
        {
            if (id != model.NumeroCuenta)
                throw new BankingAppException("Los identificadores deben de coincidir");

            if (model == null)
                throw new BankingAppException("Debe especificar los datos del elemento que desea modificar");

            var entity = _mapper.Map<Cuenta>(model);
            await _repo.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        // POST: api/Cuentas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cuenta>> PostCuenta(CuentaCreateDTO model)
        {
            if (model == null)
                throw new BankingAppException("Debe especificar los datos del nuevo elemento");

            var entity = _mapper.Map<Cuenta>(model);
            await _repo.CreateAsync(entity);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction("GetCuenta", new { id = entity.NumeroCuenta }, _mapper.Map<CuentaCreateDTO>(entity));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCuenta(string id, JsonPatchDocument<CuentaUpdateDTO> patchDTO)
        {
            if (patchDTO == null)
                throw new BankingAppException("Debe especificar los datos que desea modificar");

            var entity = await GetEntity(id);
            var dtoToBeUpdated = _mapper.Map<CuentaUpdateDTO>(entity);

            patchDTO.ApplyTo(dtoToBeUpdated, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entityUpdated = _mapper.Map<Cuenta>(dtoToBeUpdated);
            await _repo.UpdateAsync(entityUpdated);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/Cuentas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta(string id)
        {
            var entity = await GetEntity(id);
            await _repo.RemoveAsync(entity);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        private async Task<Cuenta> GetEntity(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new BankingAppException("El identificador debe ser un número de cuenta valido");

            var entity = await _repo.GetAsync(i => i.NumeroCuenta == id, tracked: false);

            if (entity == null)
                throw new KeyNotFoundException("No se encontró la información requerida");

            return entity;
        }
    }
}
