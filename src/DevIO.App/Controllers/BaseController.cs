using DevIO.Business.Interfaces;
using DevIO.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.Controllers
{
    public class BaseController : Controller 
    {
        private readonly INotificador _notificador;

        protected BaseController(INotificador notificador)
        {
            _notificador = notificador; 
        }

        protected bool OperacaoValida()
        {
           return !_notificador.TemNotificacao();
        }
    }
}
