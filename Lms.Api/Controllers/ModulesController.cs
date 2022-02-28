#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Core.Entities;
using Lms.Data.Data;
using AutoMapper;
using Lms.Core.Dto;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly LmsApiContext _context;
        private readonly IMapper _mapper;

        public ModulesController(LmsApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModule()
        {
            var moduleDto = _mapper.ProjectTo<ModuleDto>(_context.Module);
            return await moduleDto.ToListAsync();
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleDto>> GetModule(int id)
        {
            var module = await _context.Module.FindAsync(id);

            if (module == null)
            {
                return NotFound();
            }

            return _mapper.Map<ModuleDto>(module);
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, ModuleUpdateDto moduleUpdateDto)
        {
            var moduleUpdate = await _context.Module.FindAsync(id);

            if (moduleUpdate == null)
            {
                return NotFound();
            }

            _mapper.Map(moduleUpdateDto, moduleUpdate);
            _context.SaveChanges();

            return NoContent();
        }

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(ModuleCreateDto moduleCreateDto)
        {
            {
                var module = _mapper.Map<Module>(moduleCreateDto);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Module.Add(module);
                await _context.SaveChangesAsync();
                var moduleDto = _mapper.Map<ModuleDto>(module);
                return CreatedAtAction("GetModule", new { id = module.Id }, moduleCreateDto);
            }
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var @module = await _context.Module.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            _context.Module.Remove(@module);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModuleExists(int id)
        {
            return _context.Module.Any(e => e.Id == id);
        }
    }
}
