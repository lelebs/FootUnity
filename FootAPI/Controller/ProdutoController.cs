using FootAPI.Interfaces;
using FootAPI.Model;
using FootAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FootAPI.Controller
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly ErrorLoggingRepository errorLoggingRepository;
        public ProdutoController(IProdutoRepository produtoRepository,
            ErrorLoggingRepository errorLoggingRepository)
        {
            this.errorLoggingRepository = errorLoggingRepository;
            this.produtoRepository = produtoRepository;
        }

        [HttpGet("obtergridprodutos")]
        public async Task<IActionResult> ObterProdutos()
        {
            try
            {
                return Ok(await produtoRepository.GetProducts());
            }
            catch(Exception ex) 
            {
                await this.errorLoggingRepository.RegistrarErro(ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("obterproduto/{idProduto}")]
        public async Task<IActionResult> ObterProduto(int idProduto)
        {
            try
            {
                return Ok(await produtoRepository.GetProduct(idProduto));
            }
            catch (Exception ex)
            {
                await this.errorLoggingRepository.RegistrarErro(ex);
                return BadRequest(ex);
            }
        }

        [HttpPost("inserirproduto")]
        public async Task<IActionResult> InserirProduto([FromBody] Produto model)
        {
            try
            {
                return Ok(await produtoRepository.InsertProduct(model));
            }
            catch (Exception ex)
            {
                await this.errorLoggingRepository.RegistrarErro(ex);
                return BadRequest(ex);
            }
        }

        [HttpPost("alterarproduto")]
        public async Task<IActionResult> AlterarProdutos([FromBody] Produto model)
        {
            try
            {
                await produtoRepository.UpdateProduct(model);
                return Ok();
            }
            catch (Exception ex)
            {
                await this.errorLoggingRepository.RegistrarErro(ex);
                return BadRequest(ex);
            }
        }
    }
}
