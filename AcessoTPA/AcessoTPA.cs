using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WooCommerceNET;

public class AcessoTPA
{
    private readonly AppDbContext _context;
    private UsuariosTPA _usuariosService;
    public AcessoTPA(AppDbContext context, UsuariosTPA usuariosService)
    {
        _context = context;
        _usuariosService = usuariosService;
    }

    public async Task<int?> CriarNumeroDocumentoEC()
    { 
        var maxDocumento = await _context.Doctoped.Where(doc => doc.tpDocto == "EC").MaxAsync(op => (int?)op.documento) ?? 0;
        Console.WriteLine(maxDocumento);
        Console.WriteLine(maxDocumento + 1);
        return maxDocumento + 1;
    }

    public async Task<IEnumerable<TpaCadastroDTO>> GetMovtoped()
    {
        var result = await _context.CadastroUsuarioTPA.Where(doc => doc.id == 50526).ToListAsync();
        return result;
    }

    public async Task<InformacoesProdutoTPA> GetDadosProduto(ulong _pkProduto)
    {
        var pk = _pkProduto.ToString();
        var result = await _context.Produto.Where(produto => produto.pkProduto.Trim() == pk).SingleOrDefaultAsync();
        if(result == null)
        {
            throw new InvalidOperationException("Produto não encontrado no banco de dados");
        }
        return result;
    }

    public async Task<List<ServicoProduto>> GetTiposServico(QueryFilter filter, CancellationToken cancellationToken = default)
    {
        var pageNumber = Math.Max(1, filter.PageNumber);
        var pageSize = Math.Clamp(filter.PageSize, 1, 50);
        var offset = (pageNumber - 1) * pageSize;

        const string _query = @$"
            SELECT PK_PRODEVENTOSV, TPSV.PK_EVENTOTPSV, TPSV.DESCRICAO, PRODUTO.PK_PRODUTO, PRODUTO.DESCRICAO AS NOME_PRODUTO
                FROM TPAPRODEVENTOSV as PRODSV
            INNER JOIN TPAEVENTOTPSV AS TPSV ON PRODSV.RDX_EVENTOSV = TPSV.PK_EVENTOTPSV
            INNER JOIN TPAPRODUTO AS PRODUTO ON PRODSV.IDX_PRODUTO = PRODUTO.PK_PRODUTO
            ORDER BY PK_PRODEVENTOSV
            OFFSET @offset ROWS
            FETCH NEXT @pageSize ROWS ONLY
        ";


        var response = await _context.ServicosProdutos
            .FromSqlRaw(_query, 
                new SqlParameter("@offset", offset), 
                new SqlParameter("@pageSize", pageSize)).AsNoTracking().ToListAsync(cancellationToken);
        return response;
    }

    public async Task<List<VendedoresDTO>> GetVendedores()
    {
        const string _query = @$"
            SELECT F.PK_FUNCIONARIO, F.NOME, F.NOMEINTERNO, O.IDX_OPSETOR FROM TPAFUNCIONARIO AS F
                INNER JOIN TPAOPERADOR AS O ON F.PK_FUNCIONARIO = O.IDX_FUNCIONARIO 
            WHERE F.STATUS = 'A' AND F.VENDEDOR = 'S'
            ORDER BY F.NOME
        ";
        var vendedores = await _context.Vendedores.FromSqlRaw(_query).ToListAsync();
        return vendedores; 
    }


    public async Task<TpaDoctopedDTO> CadastrarEventoDoctoped(DadosPedido dadosPedido, string _idxEntidade, string _idxEnderecoObra)
    {
        var numeroDocumento = await CriarNumeroDocumentoEC();
        var totalPedido = decimal.Parse(dadosPedido.totalPedido, CultureInfo.InvariantCulture);
        Console.WriteLine(numeroDocumento);
        var novoDoctoped = new TpaDoctopedDTO()
        {
            documento = numeroDocumento,
            idxEntidade = _idxEntidade,
            nome = dadosPedido.dadosCliente.nomeCliente,
            cnpjCpf = dadosPedido.dadosCliente.cpf_cnpj,
            idxVendedor1 = "         907",
            totalDocto = totalPedido,
            prodValor = totalPedido,
            prodTotal = totalPedido,
            freteValor = 100,
            despDivValor = 0,
            dtSaida = BrazilTime.Now(),
            dtEvento = BrazilTime.Now(),
            totalItensProd = dadosPedido.produtos.Count,
            totalQtProd = dadosPedido.produtos.Count,
            opInc = 436,
            opAlt = 436,
            idxEnderecoObra = _idxEnderecoObra,
            entregar = dadosPedido.modoEntregaId,
            nfDtEmissao = null,
            nfDtSaida = null,
            nfHoraSaida = "",
            idxDoctoEvento = dadosPedido.idPedido.ToString(),
            idxDeptoEnt = "",
            dtPrevisao = BrazilTime.Now(),
            horaPrevisao = "",
            dtPrevisaoIni = null,
            totalFinanceiro = totalPedido
        };
        Console.WriteLine(JsonSerializer.Serialize(novoDoctoped));
        
        return novoDoctoped;
    }

    public async Task<TpaMovtopedDTO> CadastrarProdutosEventoMovtoped(DadosProduto produto, int rdxDoctoped, int itemId)
    {
        var dadosProdutoTpa = await GetDadosProduto(produto.idProdutoTPA);
        Console.WriteLine(JsonSerializer.Serialize(dadosProdutoTpa));
        var novoMovtoped = new TpaMovtopedDTO
        {
            rdxDoctoped = rdxDoctoped,
            item = itemId,
            codProduto = dadosProdutoTpa.codProduto,
            descricao = dadosProdutoTpa.descricao,
            idxProduto = dadosProdutoTpa.pkProduto, 
            unidade = dadosProdutoTpa.unidade,
            cst = dadosProdutoTpa.csti,
            l_quantidade = produto.quantidade,
            l_precouni = produto.preco,
            l_precofinal = produto.preco,
            l_precototal = produto.preco * produto.quantidade,
            l_precovenda = produto.preco,
            l_precoavista = produto.preco,
            e_precocusto = dadosProdutoTpa.pcCusto,
            e_precomedio = dadosProdutoTpa.pcMedio,
            e_quantidade = produto.quantidade * -1,
            peso = dadosProdutoTpa.peso,
            dtInc = BrazilTime.Now(),
            opInc = 436,
            dtAlt = BrazilTime.Now(),
            opAlt = 436,
            ncm = dadosProdutoTpa.ncm
        };
        
        return novoMovtoped;
    }

    public async Task CadastrarDoctopedFP(int _rdxDoctoped, decimal _valor)
    {
        var novoFP = new TpaDoctoPedFpDTO
        {
            rdxDoctoPed = _rdxDoctoped,
            valor = _valor,
            opInc = 436,
            opAlt = 436
        };        
        _context.DoctopedFp.Add(novoFP);
    }

    public async Task CadastrarPedido(DadosPedido dadosPedido)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var idPedidoString = dadosPedido.idPedido.ToString();
            var exists = await _context.Doctoped.AnyAsync(d => d.idxDoctoEvento.Trim() == idPedidoString && d.tpDocto == "EC");
            if(exists)
            {
                Console.WriteLine(idPedidoString);
                Console.WriteLine(JsonSerializer.Serialize(exists));
                return;
            }
            // var idxEntidade = await _usuariosService.CadastrarUsuario(dadosPedido.dadosCliente); 
            // var idxEndereco = await _usuariosService.CadastrarEnderecoUsuario(idxEntidade, dadosPedido.dadosEntrega);
           var userKeys = await _usuariosService.BuscarUsuario(dadosPedido.dadosCliente, dadosPedido.dadosEntrega);
           var doctoped = await CadastrarEventoDoctoped(dadosPedido, userKeys.pkCadastro, userKeys.pkEndereco);
           _context.Doctoped.Add(doctoped);
           await _context.SaveChangesAsync();

           int item = 1;
          
            foreach(var produto in dadosPedido.produtos)
            { 
                var movtoped = await CadastrarProdutosEventoMovtoped(produto, doctoped.pkDoctoped, item++ );
                _context.Movtoped.Add(movtoped);
            }
            await CadastrarDoctopedFP(doctoped.pkDoctoped, doctoped.totalDocto);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            }
            catch(Exception e)
            {
                await transaction.RollbackAsync();
                Console.WriteLine(e);
                Console.WriteLine(JsonSerializer.Serialize(dadosPedido));
                throw;
            }
    }
}