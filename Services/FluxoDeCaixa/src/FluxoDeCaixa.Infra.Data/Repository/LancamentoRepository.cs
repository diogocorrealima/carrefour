using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluxoDeCaixa.Domain.Interfaces;
using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace FluxoDeCaixa.Infra.Data.Repository
{
    public class LancamentoRepository : ILancamentoRepository
    {
        protected readonly FluxoDeCaixaContext Db;
        protected readonly DbSet<Lancamento> DbSet;

        public LancamentoRepository(FluxoDeCaixaContext context)
        {
            Db = context;
            DbSet = Db.Set<Lancamento>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<Lancamento> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Lancamento>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Add(Lancamento lancamento)
        {
           DbSet.Add(lancamento);
        }

        public void Update(Lancamento lancamento)
        {
            DbSet.Update(lancamento);
        }

        public void Remove(Lancamento lancamento)
        {
            DbSet.Remove(lancamento);
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public async Task<IEnumerable<Lancamento>> FindAsync(Expression<Func<Lancamento, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Lancamento>> FindConsolidadeAsync(List<string> idsUsuario, int pagina, int quantidade)
        {

          var consolidados =  (await FindAsync
                (lancamento =>
                 (idsUsuario == null || idsUsuario.Any(idUsuario => idUsuario == lancamento.IdUsuario))
                 ))
                .Skip(pagina > 1 ? pagina * quantidade : 0)
                .Take(quantidade)
                .GroupBy(lancamento => lancamento.IdUsuario)
                .Select(g => 
                new 
                {
                   IdUsuario = g.Key,
                   Valor = g.Where(lancamento => lancamento.Tipo == "Credito").Sum(x => x.Valor) - g.Where(lancamento => lancamento.Tipo == "Debito").Sum(x => x.Valor)

                }).ToList();
            var lancamentosConsolidados = consolidados.Select(lancamento => new Lancamento(Guid.Empty, lancamento.IdUsuario, lancamento.Valor));
            return lancamentosConsolidados;

        }
    }
}
