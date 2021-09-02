using System;
using WebApi.Application.Interfaces;

namespace WebApi.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
        public int CurrentYear = DateTime.Now.Year;
    }
}
