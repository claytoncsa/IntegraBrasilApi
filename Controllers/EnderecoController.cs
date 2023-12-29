using System.Net;
using IntegraBrasilApi.Interfaces;
using IntegraBrasilApi.Models;
using IntegraBrasilApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegraBrasilApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class EnderecoController : Controller
    {
        public readonly IEnderecoService _enderecoService;

        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpGet("busca/{cep}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarEndereco([FromRoute] string cep)
        {
            var response = await _enderecoService.BuscarEndereco(cep);
            
            if (response.CodigoHttp == HttpStatusCode.OK)
            {
                //ViewBag.Retorno = response.DadosRetorno;
                //return View(response.DadosRetorno);
                //return Ok(response.DadosRetorno);
                // var modelo = new EnderecoModel(); 
//#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
                var enderecoModel = new EnderecoModel
                {
                    Cep = response.DadosRetorno?.Cep,
                    Estado = response.DadosRetorno?.Estado,
                    Cidade = response.DadosRetorno?.Cidade,
                    Regiao = response.DadosRetorno?.Regiao,
                    Rua = response.DadosRetorno?.Rua,
                    Servico = response.DadosRetorno?.Servico
                };
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.

                // Retorne a view passando o objeto EnderecoModel
                //localhost:7246/api/v1/Endereco/busca/70650131
                return View(enderecoModel);

            }
            else
            {
                return StatusCode((int)response.CodigoHttp, response.ErroRetorno);
            }

        }

       /* public ActionResult Index(EnderecoModel enderecoModel)
        {
            return View(enderecoModel);
        }*/
    }
    
}