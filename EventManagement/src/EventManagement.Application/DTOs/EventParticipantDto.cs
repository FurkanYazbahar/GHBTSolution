using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.DTOs
{
    public class EventParticipantDto
    {
        public Guid EventId { get; set; }
        public Guid ParticipantId { get; set; }       
    }
}
