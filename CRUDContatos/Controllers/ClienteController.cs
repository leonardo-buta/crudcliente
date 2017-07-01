using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using CrudContatos.Sistema.Entidades;
using CrudContatos.Sistema.Repositorio;
using CRUDContatos.Models;

namespace CRUDContatos.Controllers
{
    /// <summary>
    /// Controller Cliente
    /// </summary>
    public class ClienteController : Controller
    {
        #region Propriedades
        private IUnityOfWork<Cliente> UnitOfWorkCliente { get; set; }
        private IUnityOfWork<TelefoneCliente> UnitOfWorkTelefone { get; set; }
        private int ClientesPorPagina = 4;
        #endregion

        #region Construtor
        public ClienteController()
        {
            UnitOfWorkCliente = new ClienteRepositorio();
            UnitOfWorkTelefone = new TelefoneClienteRepositorio();
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Método responsável por listar os clientes
        /// </summary>
        public ActionResult ListagemClientes(int pagina = 1)
        {
            ClienteViewModel clienteViewModel = new ClienteViewModel();
            Paginacao paginacao = new Paginacao();

            clienteViewModel.Clientes = UnitOfWorkCliente.GetAll()
                .OrderByDescending(c => c.DataCriacao)
                .Skip((pagina - 1) * ClientesPorPagina)
                .Take(ClientesPorPagina);

            // Monta a paginação
            paginacao.PaginaAtual = pagina;
            paginacao.ItensPorPagina = ClientesPorPagina;
            paginacao.ItensTotal = UnitOfWorkCliente.GetAll().Count();


            clienteViewModel.Paginacao = paginacao;

            return View(clienteViewModel);
        }

        /// <summary>
        /// Cadastro de cliente do tipo GET
        /// </summary>
        public ActionResult CadastroCliente()
        {
            return View();
        }

        /// <summary>
        /// Cadastro de cliente do tipo POST
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroCliente([Bind(Include = "Nome,Telefones,Email")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                // Reune os telefones
                string[] telefonesAtuais = cliente.Telefones.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                if (ValidarTelefones(telefonesAtuais))
                {
                    cliente.DataCriacao = DateTime.Now;
                    UnitOfWorkCliente.Save(cliente);

                    // Adiciona os telefones
                    foreach (string telefone in telefonesAtuais)
                    {
                        UnitOfWorkTelefone.Save(new TelefoneCliente() { CodCliente = cliente.CodCliente, Telefone = telefone });
                    }
                }
                else
                {
                    ViewBag.ErroTelefone = true;
                    return View();
                }
            }

            return RedirectToAction("ListagemClientes", new { mensagem = "cadastrado" });
        }

        /// <summary>
        /// Edição do cliente do tipo GET
        /// </summary>
        public ActionResult EditarCliente(int? codCliente)
        {
            Cliente cliente = new Cliente();

            // Busca o cliente
            cliente = UnitOfWorkCliente.GetById(codCliente);            
            
            if (cliente != null)
            { 
                // Armazena o telefone em uma lista de string para exibição
                cliente.Telefones = string.Join("\n", cliente.TelefoneCliente.Select(t => t.Telefone));            
                return View(cliente);
            }
            else
            {
                return RedirectToAction("ListagemClientes");
            }            
        }

        /// <summary>
        /// Edição do cliente do tipo POST
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCliente([Bind(Include = "CodCliente,Nome,Email, Telefones")] Cliente cliente)
        {
            List<TelefoneCliente> telefonesAntigos = new List<TelefoneCliente>();
            string[] telefonesAtuais = cliente.Telefones.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            if (ValidarTelefones(telefonesAtuais))
            {
                // Reune os telefones antigos
                telefonesAntigos = UnitOfWorkTelefone.Where(t => t.CodCliente == cliente.CodCliente).ToList();

                // Apaga os telefones existentes
                foreach (TelefoneCliente telefone in telefonesAntigos)
                {
                    UnitOfWorkTelefone.Delete(telefone);
                }

                // Adiciona os novos telefones
                foreach (string telefone in telefonesAtuais)
                {
                    UnitOfWorkTelefone.Save(new TelefoneCliente() { CodCliente = cliente.CodCliente, Telefone = telefone });
                }

                // Atualiza o cliente
                UnitOfWorkCliente.Update(cliente);
            }
            else
            {
                ViewBag.ErroTelefone = true;
                return View();
            }

            return RedirectToAction("ListagemClientes", new { mensagem = "alterado" });
        }
        
        /// <summary>
        /// Método para excluir um cliente
        /// </summary>
        public ActionResult ExcluirCliente(int id)
        {
            Cliente cliente = new Cliente();

            cliente = UnitOfWorkCliente.GetById(id);

            if (cliente != null)
            {
                
                List<TelefoneCliente> telefonesCliente = new List<TelefoneCliente>();

                telefonesCliente = UnitOfWorkTelefone.Where(t => t.CodCliente == cliente.CodCliente).ToList();

                // Apaga os telefones
                foreach (TelefoneCliente telefone in telefonesCliente)
                {
                    UnitOfWorkTelefone.Delete(telefone);
                }

                // Apaga o cliente
                UnitOfWorkCliente.Delete(cliente);
            }

            return RedirectToAction("ListagemClientes", new { mensagem = "excluído" });
        }

        /// <summary>
        /// Valida se os telefones tem até 12 caracteres
        /// </summary>
        public bool ValidarTelefones(string[] telefones)
        {
            foreach (string telefone in telefones)
            {
                if (telefone.Length > 12)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}