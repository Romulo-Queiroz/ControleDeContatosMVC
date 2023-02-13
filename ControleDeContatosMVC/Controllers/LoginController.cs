﻿using ControleDeContatosMVC.Helper;
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
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio,
                                 ISessao sessao,
                                 IEmail email) 
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }

        public IActionResult Index()
        {
            //Se usuario estiver logado redirecionar para home

            if(_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();

            return RedirectToAction("Index", "Login");
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
                            _sessao.CriarSessaoDoUsuario(usuario);
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

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Sua nova senha é : {novaSenha}";

                       bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de Contatos- Nova Senha", mensagem);

                        if (emailEnviado) 
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos para seu email cadastrado uma nova senha";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Não conseguimos enviar o e-mail. Por favor tente novamente. ";
                        }

                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha. Por favor, verifique os dados informados.";

                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos redefinir sua senha, mais detalhes do erro {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
