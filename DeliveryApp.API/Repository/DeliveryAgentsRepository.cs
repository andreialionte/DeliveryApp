using DeliveryApp.API.DataLayers;
using DeliveryAppBackend.DataLayers.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.API.Repository
{
    public class DeliveryAgentsRepository : IDeliveryAgentsRepository
    {
        private readonly DataContext _context;

        public DeliveryAgentsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeliveryAgent>> GetAll()
        {
            return await _context.DeliveryAgents.ToListAsync();
        }

        public async Task<DeliveryAgent> GetById(int id)
        {
            return await _context.DeliveryAgents.FindAsync(id);
        }

        public async Task<DeliveryAgent> Add(DeliveryAgent deliveryAgent)
        {
            await _context.DeliveryAgents.AddAsync(deliveryAgent);
            await _context.SaveChangesAsync();
            return deliveryAgent;
        }

        public async Task<DeliveryAgent> Update(DeliveryAgent deliveryAgent, int id)
        {
            var existingAgent = await _context.DeliveryAgents.FindAsync(id);
            /*            if (existingAgent == null)
                        {
                            throw new Exception("Delivery Agent not found");
                        }*/

            existingAgent.FirstName = deliveryAgent.FirstName;
            existingAgent.LastName = deliveryAgent.LastName;
            existingAgent.Email = deliveryAgent.Email;
            existingAgent.PhoneNumber = deliveryAgent.PhoneNumber;
            existingAgent.Status = deliveryAgent.Status;

            _context.DeliveryAgents.Update(existingAgent);
            await _context.SaveChangesAsync();

            return existingAgent;
        }

        public async Task<DeliveryAgent> Delete(int id)
        {
            var agent = await _context.DeliveryAgents.FindAsync(id);
            if (agent == null)
            {
                throw new Exception("Delivery Agent not found");
            }

            _context.DeliveryAgents.Remove(agent);
            await _context.SaveChangesAsync();

            return agent;
        }
    }
}
