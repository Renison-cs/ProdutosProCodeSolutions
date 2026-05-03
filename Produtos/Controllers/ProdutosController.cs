using Produtos.Models;
using Microsoft.AspNetCore.Mvc;
using Produtos.Context;

namespace Produtos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<Produto> produtos = _context.Produtos;
                produtos.Where(x => x.Preco > 1000)
                .ToList();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var produtoEncontrado = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
            if (produtoEncontrado == null)
                return NotFound($"Produtos com id {id} não existe");

            return Ok(produtoEncontrado);
        }

        [HttpPost]
        public IActionResult Cadastrar(Produto novoProduto)
        {
            if (novoProduto is null)
                return BadRequest();

            _context.Add(novoProduto);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, Produto produtoEditado)
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
             if (produto == null)
                return NotFound("Produto não encontrado.");

             produto.Nome = produtoEditado.Nome;
            produto.Descricao = produtoEditado.Descricao;
            produto.Preco = produtoEditado.Preco;
            produto.Quantidade = produtoEditado.Quantidade;

            _context.SaveChanges();

            return Ok(produto);
            
        }

        [HttpDelete("{id}")]
        public IActionResult Exluir(int id)
        {
            var paraExcluir = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
            if (paraExcluir == null)
                return NotFound($"Produto com o id {id} não encontrado");

            _context.Remove(paraExcluir);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
