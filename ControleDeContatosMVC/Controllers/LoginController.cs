using ControleDeContatosMVC.Models;
using ControleDeContatosMVC.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ControleDeContatosMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public LoginController(IUsuarioRepositorio usuarioRepositorio) 
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                   UsuarioModel usuario = _usuarioRepositorio.BuscarporLogin(loginModel.Login);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {

                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MensagemErro"] = $"Senha do usuário é inválida, tente novamente.";
                    }

                    TempData["MensagemErro"] = $"usuário e/ou senha inválido(s). Por favor tente novamente.";
                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos realiar seu login, mais detalhes do erro {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
