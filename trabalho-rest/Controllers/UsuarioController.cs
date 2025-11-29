using Microsoft.AspNetCore.Mvc;
using ServiceSoap.Interface;
using System.Net.Mime;
using trabalho_rest.Model;

namespace trabalho_rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class UsuarioController(IUsuario usuarioService) : Controller
    {
          private readonly IUsuario _usuarioService = usuarioService;
        
        /// <summary>
        /// Obtém todos os usuários
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var usuarios = await _usuarioService.ReadAll();
            return Ok(usuarios);
        }

        /// <summary>
        /// Obtém um usuário pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            try
            {
                var usuario = await _usuarioService.Read(id);
                return Ok(usuario);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Usuário com ID {id} não encontrado");
            }
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<Usuario>> Create([FromBody] Usuario usuario)
            {   
                try
                {
                    var createdUsuario = await _usuarioService.Create(usuario);
                    return CreatedAtAction(nameof(Get), new { id = createdUsuario.Id }, createdUsuario);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            /// <summary>
            /// Atualiza um usuário existente
            /// </summary>
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<Usuario>> Update(int id, [FromBody] Usuario usuario)
            {
                if (id != usuario.Id)
                    return BadRequest("O ID na URL não corresponde ao ID no corpo da requisição");

                try
                {
                    var updatedUsuario = await _usuarioService.Update(usuario);
                    return Ok(updatedUsuario);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound($"Usuário com ID {id} não encontrado");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        /// <summary>
        /// Remove um usuário
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _usuarioService.Delete(id);
            if (!result)
                return NotFound($"Usuário com ID {id} não encontrado");
                return NoContent();
        }

    }
}
