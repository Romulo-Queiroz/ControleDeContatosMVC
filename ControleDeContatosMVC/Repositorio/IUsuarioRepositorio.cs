using ControleDeContatosMVC.Models;
using System.Collections.Generic;

namespace ControleDeContatosMVC.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarporLogin(string login);
        UsuarioModel BuscarPorEmailELogin(string email, string login);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel BuscarPorID(int id);
        UsuarioModel Adicionar (UsuarioModel usuario);
        UsuarioModel Atualizar (UsuarioModel usuario);
        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);
        bool Apagar(int id);
    }
}
