using ControleDeContatosMVC.Models;
using System.Collections.Generic;

namespace ControleDeContatosMVC.Repositorio
{
    public interface IUsuarioRepositorio
    {
        List<UsuarioModel> BuscarTodos();
        UsuarioModel BuscarPorID(int id);
        UsuarioModel Adicionar (UsuarioModel usuario);
        UsuarioModel Atualizar (UsuarioModel usuario);
        bool Apagar(int id);
    }
}
