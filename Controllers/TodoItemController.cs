using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Model;
using TodoList.Service.Interfaces;
using TodoList.Service.Services;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace TodoList.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [Authorize]
    public class TodoItemController : Controller
    {
        public readonly ITodoItemService _todoItemService;
        private readonly UserManager<User> _userManager;
      //  private readonly SignInManager<User> _signInManager;


        private readonly IMapper _mapper;
        public TodoItemController(ITodoItemService todoItemService, UserManager<User> userManager)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
         
        }

        /// <summary>
        /// Get List of todo items assigned to user
        /// </summary>        
        [HttpPost()]
        [ActionName("Getlist")]
        public async Task<ActionResult<IList<TodoItem>>> GetTodoItems()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                
                return Unauthorized();
            }
            if (user == null)
            {  
                return Unauthorized();
            }
            var todoitems = _todoItemService.GetTodoItems(user);     
            return Ok(todoitems);
        }

        /// <summary>
        ///  Create todo item
        /// </summary>  
        [HttpPost]
        public async Task<ActionResult<bool>> CreatetodoItem([FromBody] TodoItem item)
        {
            var user = await _userManager.GetUserAsync(User);
            if (item == null)
            {             
                return BadRequest();
            }      
            var issuccess = await _todoItemService.AddTodoItem(item,user);
            return Ok(issuccess);
        }

        /// <summary>
        ///  Update Todo item is isselected or not
        /// </summary> 
        [HttpPut()]
        public async Task<ActionResult<bool>> UpdateItem([FromBody] TodoItem newItem)
        {

            var user = await _userManager.GetUserAsync(User);

            if (newItem.Id == null)
            {          
                return BadRequest();
            }
            var issuccess =await _todoItemService.UpdateTodoItem(newItem,user);
            return Ok(issuccess);
        }

        /// <summary>
        /// Delete todo item based on  guid
        /// </summary>        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {

            if (id == null)
            {
                return BadRequest();
            }
            var user = await _userManager.GetUserAsync(User);
            var issuccess = await _todoItemService.DeleteTodoItem(id,user);
            return Ok(issuccess);
        }


    }
}
