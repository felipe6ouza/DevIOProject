using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DevIO.App.Extensions;

namespace DevIO.App.Controllers
{
    [Authorize]
    [Route("produtos")]
    public class ProdutosController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository, 
                                  IFornecedorRepository fornecedorRepository,
                                  IMapper mapper, IProdutoService produtoService, 
                                  INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;   
        }

        [AllowAnonymous]
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores()));
        }

        [AllowAnonymous]
        [Route("detalhes-de-produto/{id:guid}")]

        public async Task<IActionResult> Details(Guid id)
        {
            var produto = await ObterProduto(id);
           
            if (produto == null) return NotFound();

            return View(produto);
        }

        [ClaimsAuthorize("Produto", "Adicionar")]
        [Route("criar-novo-produto")]
        public async Task<IActionResult> Create()
        {
           var produtoViewModel = await PopularFornecedores(new ProdutoViewModel());
           return View(produtoViewModel);
        }

        [HttpPost]
        [ClaimsAuthorize("Produto", "Adicionar")]
        [Route("criar-novo-produto")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
             produtoViewModel = await PopularFornecedores(produtoViewModel);


            if (!ModelState.IsValid) return View(produtoViewModel);

            var imgPrefixo = Guid.NewGuid() + "_";

            if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
            {
                return View(produtoViewModel);
            }

            produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;

            await _produtoService.Adicionar( _mapper.Map<Produto>(produtoViewModel));

            if (!OperacaoValida()) return View(produtoViewModel);


            return RedirectToAction("Index");

        }

        [Route("editar-produto/{id:guid}")]
        [ClaimsAuthorize("Produto", "Editar")]
        public async Task<IActionResult> Edit(Guid id)
        {

            var produto = await ObterProduto(id);
            
            if (produto == null) return NotFound();
       
           
            return View(produto);
        }

        [HttpPost]
        [Route("editar-produto/{id:guid}")]
        [ClaimsAuthorize("Produto", "Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            
            if (id != produtoViewModel.Id) return NotFound();

            var produtoAtualizacao = await ObterProduto(id);

            produtoAtualizacao.Fornecedor = produtoViewModel.Fornecedor;
            produtoViewModel.Imagem = produtoAtualizacao.Imagem;

            if (!ModelState.IsValid) return View(produtoViewModel);

            if (produtoViewModel.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";

                if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
                {
                    return View(produtoViewModel);
                }

                produtoAtualizacao.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
            }

            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Ativo = produtoViewModel.Ativo;

            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));

            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction(nameof(Index));


        }

        [Route("remover-produto/{id:guid}")]
        [ClaimsAuthorize("Produto", "Excluir")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto == null) return NotFound();

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ClaimsAuthorize("Produto", "Excluir")]
        [Route("remover-produto/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto == null) return NotFound();

            await _produtoService.Remover(id);

            if (!OperacaoValida()) return View(produto);

            TempData["Sucesso"] = "Produto excluído com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }

        private async Task<ProdutoViewModel> PopularFornecedores(ProdutoViewModel produto)
        {
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {

            if (arquivo.Length <= 0)
            {
                return false;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(String.Empty, "Já existe um arquivo com este nome");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;

        }

    }
}
