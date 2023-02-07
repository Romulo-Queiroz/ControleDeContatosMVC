using ControleDeContatosMVC.Models;
using System.Collections.Generic;

namespace ControleDeContatosMVC.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel ListarPorId(int id);
        List<ContatoModel> BuscarTodos();
        ContatoModel Adicionar (ContatoModel contato);
        ContatoModel Atualizar (ContatoModel contato);
        bool Apagar(int id);
    }
}
