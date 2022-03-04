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
using Microsoft.AspNetCore.JsonPatch;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly LmsApiContext _context;
        private readonly IMapper _mapper;

        public CoursesController(LmsApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourse()
        {
            var courseDto = _mapper.ProjectTo<CourseDto>(_context.Course);

            return await courseDto.ToListAsync(); 
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return _mapper.Map<CourseDto>(course);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseUpdateDto courseUpdateDto)
        {
            var courseUpdate = await _context.Course.FindAsync(id);

            if (courseUpdate == null)
            {
                return NotFound();
            }

            _mapper.Map(courseUpdateDto, courseUpdate);
            _context.SaveChanges();

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(CourseCreateDto courseCreateDto)
        {
            var course = _mapper.Map<Course>(courseCreateDto);

            _context.Course.Add(course);

            await _context.SaveChangesAsync();

            var courseDto = _mapper.Map<CourseDto>(course);

            return CreatedAtAction("GetCourse", new { id = course.Id }, courseCreateDto);
        }

        //PATCH: 
        [HttpPatch("{id}")]
        public async Task<ActionResult<CourseUpdateDto>> PatchCourse(int id, JsonPatchDocument<CourseUpdateDto> patchDocument)
        {
            var courseToUpdate = await _context.Course.FindAsync(id);
            
            if (courseToUpdate == null) { return NotFound(); }

            var dto = _mapper.Map<CourseUpdateDto>(courseToUpdate);

            patchDocument.ApplyTo(dto, ModelState);

            if (!TryValidateModel(dto)) { return BadRequest(ModelState); }

            _mapper.Map(dto, courseToUpdate);
            
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<CourseDto>(courseToUpdate));
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
    }
}