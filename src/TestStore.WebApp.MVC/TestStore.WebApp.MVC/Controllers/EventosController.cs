﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestStore.Core.Data.EventSourcing;

namespace TestStore.WebApp.MVC.Controllers
{
    public class EventosController : Controller
    {
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public EventosController(IEventSourcingRepository eventSourcingRepository)
        {
            _eventSourcingRepository = eventSourcingRepository;
        }

        [HttpGet("eventos/{id:guid}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var eventos = await _eventSourcingRepository.ObterEventos(id);
            return View(eventos);
        }
    }
}