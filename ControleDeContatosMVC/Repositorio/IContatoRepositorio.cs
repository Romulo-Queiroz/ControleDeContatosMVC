using ControleDeContatosMVC.Models;
using System.Collections.Generic;

namespace ControleDeContatosMVC.Repositorio
{
    public interface IContatoRepositorio
    {
        List<ContatoModel> BuscarTodos();
        ContatoModel Adicionar (ContatoModel contato);
    }
}
