using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankingAPI.DataAccess;
using BankingAPI.Entities;
using AutoMapper;
using BankingAPI.DataAccess.Repositories.IRepositories;
using BankingAPI.DTOs.Movimiento;
using BankingAPI.Infrastructure;
using Microsoft.AspNetCore.JsonPatch;
using BankingAPI.Business.IServices;

namespace BankingAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IService<Movimiento> _service;
        private readonly IMapper _mapper;

        public MovimientosController(IService<Movimiento> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Movimientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoDTO>>> GetMovimientos()
        {
            var list = await _service.GetAllAsync();
            return Ok(_mapper.Map<List<MovimientoDTO>>(list));
        }

        // GET: api/Movimientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoDTO>> GetMovimiento(int id)
        {
            var entity = await GetEntity(id);
            return Ok(_mapper.Map<MovimientoDTO>(entity));
        }

        // POST: api/Movimientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movimiento>> PostMovimiento(MovimientoCreateDTO model)
        {
            if (model == null)
                throw new BankingAppException("Debe especificar los datos del nuevo elemento");

            var entity = _mapper.Map<Movimiento>(model);
            await _service.CreateAsync(entity);
            return CreatedAtAction("GetMovimiento", new { id = entity.MovimientoID }, _mapper.Map<MovimientoCreateDTO>(entity));
        }

        // PUT: api/Movimientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimiento(int id, MovimientoUpdateDTO model)
        {
            if (id != model.MovimientoID)
                throw new BankingAppException("Los identificadores deben de coincidir");

            if (model == null)
                throw new BankingAppException("Debe especificar los datos del elemento que desea modificar");

            var entity = _mapper.Map<Movimiento>(model);
            await _service.UpdateAsync(entity);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMovimiento(int id, JsonPatchDocument<MovimientoUpdateDTO> patchDTO)
        {
            if (patchDTO == null)
                throw new BankingAppException("Debe especificar los datos que desea modificar");

            var entity = await GetEntity(id);
            var dtoToBeUpdated = _mapper.Map<MovimientoUpdateDTO>(entity);

            patchDTO.ApplyTo(dtoToBeUpdated, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entityUpdated = _mapper.Map<Movimiento>(dtoToBeUpdated);
            await _service.UpdateAsync(entityUpdated);

            return NoContent();
        }

        // DELETE: api/Movimientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            var entity = await GetEntity(id);
            await _service.RemoveAsync(entity);
            return NoContent();
        }

        private async Task<Movimiento> GetEntity(int id)
        {
            if (id <= 0)
                throw new BankingAppException("El identificador debe ser un número entero positivo");

            var entity = await _service.GetAsync(id);

            if (entity == null)
                throw new KeyNotFoundException("No se encontró la información requerida");

            return entity;
        }
    }
}
