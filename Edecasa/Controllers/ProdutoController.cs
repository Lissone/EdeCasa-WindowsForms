﻿using Edecasa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edecasa.Controllers
{
    public class ProdutoController
    {
        public List<Produto> getAll()
        {
            List<Produto> produtos = new List<Produto>();

            try
            {
                using (var ctx = new ModelContext())
                {
                    produtos = ctx.Produto.ToList();

                    return produtos;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<Produto> getByCategoria(string categoria)
        {
            List<Produto> produtos = new List<Produto>();

            try
            {
                using (var ctx = new ModelContext())
                {
                    produtos = ctx.Produto
                        .Where(o => o.Categoria == categoria)
                        .OrderBy(o => o.Id)
                        .ToList();

                    return produtos;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Produto create(Produto produto)
        {
            try
            {
                produto.Id = 0;

                using (var ctx = new ModelContext())
                {
                    ctx.Produto.Add(produto);
                    ctx.SaveChanges();

                    return produto;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool update(Produto data)
        {
            try
            {
                if (data.Id == 0)
                    return false;

                using (var ctx = new ModelContext())
                {
                    var produto = ctx.Produto.SingleOrDefault(o => o.Id == data.Id);

                    if (produto == null)
                    {
                        Console.WriteLine("Este produto não existe");
                        return false;
                    }

                    produto.Descricao = data.Descricao;
                    produto.Categoria = data.Categoria;
                    produto.VlPequeno = data.VlPequeno;
                    produto.VlGrande = data.VlGrande;

                    ctx.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return false;
            }
        }

        public bool delete(int id)
        {
            try
            {
                if (id == 0)
                    return false;

                using (var ctx = new ModelContext())
                {
                    var produto = ctx.Produto.SingleOrDefault(o => o.Id == id);

                    if (produto == null)
                    {
                        Console.WriteLine("Este produto não existe");
                        return false;
                    }

                    ctx.Produto.Remove(produto);

                    ctx.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
