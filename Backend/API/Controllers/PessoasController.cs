using Application.Services.Pessoas.Atualizar;
using Application.Services.Pessoas.Buscar;
using Application.Services.Pessoas.Criar;
using Application.Services.Pessoas.Excluir;
using Application.Services.Pessoas.Listar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("pessoas")]
    public class PessoasController : BaseController
    {
        private readonly IMediator _mediator;

        public PessoasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Criar pessoa
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarPessoaRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        /// <summary>
        /// Atualizar pessoa
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarPessoaRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        /// <summary>
        /// Excluir pessoa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Excluir([FromRoute] int id)
        {
            await _mediator.Send(new ExcluirPessoaRequest { Id = id });
            return Ok();
        }

        /// <summary>
        /// Buscar pessoa pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Buscar([FromRoute] int id)
        {
            var response = await _mediator.Send(new BuscarPessoaRequest { Id = id });
            return Ok(response);
        }

        /// <summary>
        /// Listar todos as pessoas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var response = await _mediator.Send(new ListarPessoasRequest());
            return Ok(response);
        }
    }
}
