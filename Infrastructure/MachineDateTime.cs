using System;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
        public int CurrentYear = DateTime.Now.Year;
    }
}
