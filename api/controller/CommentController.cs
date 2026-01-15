using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.controller
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository repo, IStockRepository stockRepo)
        {
            _commentRepo = repo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentDtos = comments.Select(x => x.ToCommentDto());

            return Ok(commentDtos);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            var commentDto = comment.ToCommentDto;
            return Ok(commentDto);
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId,CreateCommentDto ccDto)
        {
            if(!await _stockRepo.StockExist(stockId))
            {
                return BadRequest("Stock is noy exist");
            }

            var commentModel = ccDto.ToComment(stockId);
            var cm = await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById),new{id =commentModel.Id},commentModel.ToCommentDto());
        }
    }
}
