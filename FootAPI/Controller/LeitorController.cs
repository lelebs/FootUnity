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
    [Route("api/leitor")]
    public class LeitorController : ControllerBase
    {
        private readonly ILeitorRepository leitorRepository;
        private readonly ErrorLoggingRepository errorLoggingRepository;
        public LeitorController(ILeitorRepository leitorRepository,
             ErrorLoggingRepository errorLoggingRepository)
        {
            this.errorLoggingRepository = errorLoggingRepository;
            this.leitorRepository = leitorRepository;
        }

        [HttpGet("obtergridleitores")]
        public async Task<IActionResult> ObterLeitores()
        {
            try
            {
                var retorno = await leitorRepository.ObterLeitores();
                return Ok( new { data = retorno.Item1, total = retorno.Item2 });
            }
            catch (Exception ex)
            {
                await this.errorLoggingRepository.RegistrarErro(ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("obterleitor/{macAddress}")]
        public async Task<IActionResult> ObterLeitor(string macAddress)
        {
            try
            {
                return Ok(await leitorRepository.ObterLeitor(macAddress));
            }
            catch (Exception ex)
            {
                await this.errorLoggingRepository.RegistrarErro(ex);
                return BadRequest(ex);
            }
        }

        [HttpPost("inserirleitor")]
        public async Task<IActionResult> InserirLeitor([FromBody] Leitor model)
        {
            try
            {
                return Ok(await leitorRepository.Inserir(model));
            }
            catch (Exception ex)
            {
                await this.errorLoggingRepository.RegistrarErro(ex);
                return BadRequest(ex);
            }
        }

        [HttpPost("alterarleitor")]
        public async Task<IActionResult> AlterarProdutos([FromBody] Leitor model)
        {
            try
            {
                await leitorRepository.Alterar(model);
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