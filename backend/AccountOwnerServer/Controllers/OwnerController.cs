using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountOwnerServer.Controllers;
[Route("api/owner")]
[ApiController]
public class OwnerController : ControllerBase
{
    private ILoggerManager _logger;
    private IRepositoryWrapper _repository;
    private IMapper _mapper;
    public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper
    mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }
    [HttpGet]
    public IActionResult GetAllOwners()
    {
        try
        {
            var owners = _repository.Owner.GetAllOwners();
            _logger.LogInfo($"Retornando todos os owners do banco de dados.");
            var ownersResult = _mapper.Map<IEnumerable<OwnerDto>>(owners);
            return Ok(ownersResult);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro no método GetAllOwners: {ex.Message}");
            return StatusCode(500, "Erro Interno do Servidor");
        }
    }

    [HttpGet("{id}", Name = "OwnerById")]
    public IActionResult GetOwnerById(Guid id)
    {
        try
        {
            var owner = _repository.Owner.GetOwnerById(id);
            if (owner is null)
            {
                _logger.LogError($"Owner com Id: {id}, não encontrado.");
                return NotFound();
            }
            else
            {
                _logger.LogInfo($"Retornando o owner com Id: {id}");
                var ownerResult = _mapper.Map<OwnerDto>(owner);
                return Ok(ownerResult);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro no método GetOwnerById: {ex.Message}");
            return StatusCode(500, "Erro Interno do Servidor");
        }
    }

    [HttpGet("{id}/account")]
    public IActionResult GetOwnerWithDetails(Guid id)
    {
        try
        {
            var owner = _repository.Owner.GetOwnerWithDetails(id);
            if (owner == null)
            {
                _logger.LogError($"Owner com Id: {id}, não encontrado.");
                return NotFound();
            }
            else
            {
                _logger.LogInfo($"Retornando o owner com detalhes e Id: {id}");
                var ownerResult = _mapper.Map<OwnerDto>(owner);
                return Ok(ownerResult);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro no método GetOwnerWithDetails: {ex.Message}");
            return StatusCode(500, "Erro Interno do Servidor");
        }
    }

    [
HttpPost]
    public IActionResult CreateOwner([FromBody] OwnerForCreationDto owner)
    {
        try
        {
            if (owner is null)
            {
                _logger.LogError("Objeto Owner enviado está nulo.");
                return BadRequest("Objeto Owner é nulo");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Objeto owner enviado é inválido.");
                return BadRequest("Objeto de modelo inválido");
            }
            var ownerEntity = _mapper.Map<Owner>(owner);
            _repository.Owner.CreateOwner(ownerEntity);
            _repository.Save();
            var createdOwner = _mapper.Map<OwnerDto>(ownerEntity);
            return CreatedAtRoute("OwnerById", new { id = createdOwner.Id }, createdOwner);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro no método CreateOwner: {ex.Message}");
            return StatusCode(500, "Erro Interno do Servidor");
        }
    }


    [HttpPut("{id}")]
    public IActionResult UpdateOwner(Guid id, [FromBody] OwnerForUpdateDto owner)
    {
        try
        {
            if (owner is null)
            {
                _logger.LogError("Objeto Owner enviado está nulo.");
                return BadRequest("Objeto Owner é nulo");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Objeto owner enviado é inválido.");
                return BadRequest("Objeto de modelo inválido");
            }
            var ownerEntity = _repository.Owner.GetOwnerById(id);
            if (ownerEntity is null)
            {
                _logger.LogError($"Owner com Id: {id}, não encontrado.");
                return NotFound();
            }
            _mapper.Map(owner, ownerEntity);
            _repository.Owner.UpdateOwner(ownerEntity);
            _repository.Save();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro no método UpdateOwner: {ex.Message}");
            return StatusCode(500, "Erro Interno do Servidor");
        }
    }

    [
    HttpDelete("{id}")]
    public IActionResult DeleteOwner(Guid id)
    {
        try
        {
            var owner = _repository.Owner.GetOwnerById(id);
            if (owner == null)
            {
                _logger.LogError($"Owner com Id: {id}, não encontrado.");
                return NotFound();
            }
            if (_repository.Account.AccountsByOwner(id).Any())
            {
                _logger.LogError($"O owner com id: {id}, não pode ser excluído, pois possuir contas relacionadas.Exclua as contas primeiro.");
                return BadRequest("Não é possível excluir o owner. Possui contas relacionadas.Exclua as contas primeiro.");
            }

            _repository.Owner.DeleteOwner(owner);
            _repository.Save();
            return NoContent();

        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro no método DeleteOwner: {ex.Message}");
            return StatusCode(500, "Erro Interno do Servidor");
        }
    }
}

