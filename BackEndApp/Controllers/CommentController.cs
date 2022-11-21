using BackEndApp.Data;
using BackEndApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : Controller
    {

        private readonly CommentDbContext commentDbContext;
        public CommentController(CommentDbContext commentDbContext)
        {
            this.commentDbContext = commentDbContext;
        }

        //Get All Commments
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            try
            {
                var comments = await commentDbContext.Comments.ToListAsync();
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        //Get single comment

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetComment")]
        public async Task<IActionResult> GetComment([FromRoute] Guid id)
        {
            try
            {
                var comments = await commentDbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
                if (comments != null)
                {
                    return Ok(comments);
                }
                return NotFound("Comment not found");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
         

        }

        //Add comment
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] Comment comment)
        {
            try
            {
                comment.Id = Guid.NewGuid();

                await commentDbContext.Comments.AddAsync(comment);
                await commentDbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment); //Para enviar respuesta 201 con la funcion de GetComment para traer el Comment recien creado
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);    
            }
           

        }

        //Updating a Comment
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateComment([FromRoute] Guid id, [FromBody] Comment comment)
        {
            try
            {
                var existingComment = await commentDbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
                if (existingComment != null)
                {
                    existingComment.Author = comment.Author;
                    existingComment.CreatedDate = comment.CreatedDate;
                    existingComment.Content = comment.Content;
                    existingComment.Title = comment.Title;
                    await commentDbContext.SaveChangesAsync();
                    return Ok(existingComment);
                }
                return NotFound("Comment not found");
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        //Delete a Comment
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            try
            {
                var existingComment = await commentDbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
                if (existingComment != null)
                {
                    commentDbContext.Remove(existingComment);
                    await commentDbContext.SaveChangesAsync();
                    return Ok(existingComment);
                }
                return NotFound("Comment not found");
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
           
        }

    }
}
