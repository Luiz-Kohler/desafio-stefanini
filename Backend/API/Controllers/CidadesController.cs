using Application.Services.Cidades.Atualizar;
using Application.Services.Cidades.Buscar;
using Application.Services.Cidades.Criar;
using Application.Services.Cidades.Excluir;
using Application.Services.Cidades.Listar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("cidades")]
    public class CidadesController : BaseController
    {
        private readonly IMediator _mediator;

        public CidadesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Criar cidade
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarCidadeRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        /// <summary>
        /// Atualizar cidade
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarCidadeRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        /// <summary>
        /// Excluir cidade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Excluir([FromRoute] int id)
        {
            await _mediator.Send(new ExcluirCidadeRequest { Id = id });
            return Ok();
        }

        /// <summary>
        /// Buscar cidade pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Buscar([FromRoute] int id)
        {
            var response = await _mediator.Send(new BuscarCidadeRequest { Id = id });
            return Ok(response);
        }

        /// <summary>
        /// Listar todos as cidades
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var response = await _mediator.Send(new ListarCidadesRequest());
            return Ok(response);
        }
    }
}
