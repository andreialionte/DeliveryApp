using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
using DeliveryAppBackend.DataLayers.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryAgentsController : ControllerBase
    {
        private readonly IDeliveryAgentsRepository _repository;

        public DeliveryAgentsController(IDeliveryAgentsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetDeliveryAgents")]
        public async Task<IActionResult> GetDeliveryAgents()
        {
            var agents = await _repository.GetAll();
            return Ok(agents);
        }

        [HttpGet("GetSingleDeliveryAgent/{id}")]
        public async Task<IActionResult> GetSingleDeliveryAgent(int id)
        {
            var agent = await _repository.GetById(id);
            if (agent == null)
            {
                return NotFound();
            }
            return Ok(agent);
        }

        [HttpPost("AddDeliveryAgent")]
        public async Task<IActionResult> AddDeliveryAgent(DeliveryAgentDTO agentDTO)
        {
            var agent = new DeliveryAgent
            {
                FirstName = agentDTO.FirstName,
                LastName = agentDTO.LastName,
                Email = agentDTO.Email,
                PhoneNumber = agentDTO.PhoneNumber,
                Status = agentDTO.Status
            };

            var addedAgent = await _repository.Add(agent);
            return Ok(addedAgent);
        }

        [HttpPut("UpdateDeliveryAgent")]
        public async Task<IActionResult> UpdateDeliveryAgent(int id, DeliveryAgentDTO agentDTO)
        {
            var existingAgent = await _repository.GetById(id);

            existingAgent.FirstName = agentDTO.FirstName;
            existingAgent.LastName = agentDTO.LastName;
            existingAgent.Email = agentDTO.Email;
            existingAgent.PhoneNumber = agentDTO.PhoneNumber;
            existingAgent.Status = agentDTO.Status;

            var updatedAgent = await _repository.Update(existingAgent, id);
            return Ok(updatedAgent); // Return the updated agent
        }


        [HttpDelete("DeleteDeliveryAgent")]
        public async Task<IActionResult> DeleteDeliveryAgent(int id)
        {
            var existingAgent = await _repository.GetById(id);
            /*            if (existingAgent == null)
                        {
                            return NotFound();
                        }*/

            var deletedAgent = await _repository.Delete(id);
            return Ok(deletedAgent);
        }
    }
}
