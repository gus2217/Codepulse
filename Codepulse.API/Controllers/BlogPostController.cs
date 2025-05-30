using AutoMapper;
using Codepulse.API.DTOs.BlogPost;
using Codepulse.API.DTOs.Category;
using Codepulse.API.Repositories.Interfaces;
using Codepulse.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Codepulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BlogPostController(IMapper mapper, IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _blogPostRepository = blogPostRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBlogPostRequestDto createBlogPostRequestDto)
        {
            var blogPostDomain = _mapper.Map<BlogPost>(createBlogPostRequestDto);
            blogPostDomain.Categories = new List<Category>();
            foreach (var categoryId in createBlogPostRequestDto.Categories)
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
                if (existingCategory != null)
                {
                    blogPostDomain.Categories.Add(existingCategory);
                }
            }
            blogPostDomain = await _blogPostRepository.CreateAsync(blogPostDomain);
            return Ok(_mapper.Map<BlogPostToDisplay>(blogPostDomain));

        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<BlogPost> blogPosts = await _blogPostRepository.GetAllAsync();
            List<BlogPostToDisplay> blogPostToDisplays = _mapper.Map<List<BlogPostToDisplay>>(blogPosts);
            return Ok(blogPostToDisplays);
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(id);

            if (blogPost == null) return NotFound();

            return Ok(_mapper.Map<BlogPostToDisplay>(blogPost));
        }
        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetByUrlHandle([FromRoute] string urlHandle)
        {
            var blogPost = await _blogPostRepository.GetByUrlAsync(urlHandle);

            if (blogPost == null) return NotFound();

            return Ok(_mapper.Map<BlogPostToDisplay>(blogPost));
        }

        [HttpPut]
        [Route("{id:long}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] BlogPostToUpdate blogPostToUpdate)
        {

            var blogPostDomain = _mapper.Map<BlogPost>(blogPostToUpdate);
            blogPostDomain.Id = id;
            blogPostDomain.Categories = new List<Category>();
            foreach (var categoryId  in blogPostToUpdate.Categories)
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
                if (existingCategory != null)
                {
                    blogPostDomain.Categories.Add(existingCategory);
                }
            }

            blogPostDomain = await _blogPostRepository.UpdateAsync(blogPostDomain);
         
            if (blogPostDomain == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BlogPostToDisplay>(blogPostDomain));

        }

        [HttpDelete]
        [Route("{id:long}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute,] long id)
        {
            var blogPost = await _blogPostRepository.DeleteAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BlogPostToDisplay>(blogPost));
        }


    }
}
