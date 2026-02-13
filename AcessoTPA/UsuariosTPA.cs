using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class UsuariosTPA
{
    private readonly AppDbContext _context;

    public UsuariosTPA(AppDbContext context)
    {
        _context = context;   
    }

    public async Task<string> CriarCodigoCliente()
    {
        var maxCodigoCliente = await _context.CadastroUsuarioTPA.MaxAsync(usuario => usuario.codCadastro);
        var intCodigo = Convert.ToInt32(maxCodigoCliente) + 1;
        var novoCodigo = intCodigo.ToString("D6");
        Console.WriteLine(novoCodigo);
        return novoCodigo;
    }

    public async Task<string> CriarPkCadastro()
    {
        //var ultimoId = await _context.CadastroUsuarioTPA.OrderByDescending(u => u.id).Select(u => u.id).FirstAsync();
        var ultimoId = await _context.Database
        .SqlQueryRaw<int>("SELECT MAX(ID) AS Value FROM TPACADASTRO")
        .FirstAsync();
        var ultimoIdMaisUm = ultimoId + 1;
        //var teste = "       50526";
        var novoPkCadastro = $"       {ultimoIdMaisUm}";
        return novoPkCadastro;
    }

    public async Task<string> CriarPkEndereco()
    {
        var ultimoId = await _context.Enderecos.MaxAsync(endereco=> endereco.id);
        var ultimoIdMaisUm = ultimoId + 1;
        var novoPkEndereco = $"       {ultimoIdMaisUm}";
        return novoPkEndereco;
    }

    public async Task<string> CriarPkContato()
    {
        var ultimoId = await _context.Contatos.MaxAsync(contato => contato.id);
        var ultimoIdMaisUm = ultimoId + 1;
        var novoPkContato = $"       {ultimoIdMaisUm}";
        return novoPkContato;
        
    }

    public async Task<TpaCadastroDTO> VerUsuario()
    {
        var result = await _context.CadastroUsuarioTPA.Where(usuario => usuario.id == 50538).SingleOrDefaultAsync() ?? null;
        return result;
    }

    private async Task<UserKeys> ClienteCadastrado(string _cnpjcpf)
    {
        var cadastro = await _context.CadastroUsuarioTPA.Where(usuario => usuario.cnpjCpf == _cnpjcpf).SingleOrDefaultAsync() ?? null;
        string _pkEndereco;
        if(cadastro == null)
        {
            return new UserKeys();
        }
        else
        {
            Console.WriteLine(cadastro.pkCadastro);
            Console.WriteLine("################################");
            Console.WriteLine(JsonSerializer.Serialize(cadastro));
            _pkEndereco = await _context.Enderecos.Where(end => end.idxTabela == cadastro.pkCadastro).Select(end => end.pkEndereco).SingleOrDefaultAsync() ?? "";
        }
        return new UserKeys(){pkCadastro = cadastro.pkCadastro, pkEndereco = _pkEndereco};
    }

    public async Task<TpaCadastroDTO> CadastrarUsuario(DadosCliente dadosCliente)
    {
        var novoCodigo = await CriarCodigoCliente();
        //var novoPkCadastro = await CriarPkCadastro();
        Console.WriteLine(novoCodigo);
        var novoUsuario = new TpaCadastroDTO()
        {
            pkCadastro = await CriarPkCadastro(),
            codCadastro = novoCodigo,
            nome = dadosCliente.nomeCliente,
            fantasia = dadosCliente.nomeCliente,
            cnpjCpf = dadosCliente.cpf_cnpj,
            sexo = "M",
            estadoCivil = "",
            naturalCidade = dadosCliente.cidade,
            naturalUf = dadosCliente.estado,
            telefone1 = dadosCliente.celular,
            email = dadosCliente.email,
            opInc = 436,
            opAlt = 436
        };

        _context.CadastroUsuarioTPA.Add(novoUsuario);
        await _context.SaveChangesAsync();
        Console.WriteLine("adicionado");
        Console.WriteLine(novoUsuario.id);
        await _context.Entry(novoUsuario).ReloadAsync();
        return novoUsuario;
    }

    public async Task<string> CadastrarEnderecoUsuario(string idUsuario, DadosEntrega dadosEntrega)
    {
        var idEndereco = await CriarPkEndereco();
        var enderecoUsuario = new TpaEnderecoDTO()
        {
            pkEndereco = idEndereco,
            endereco = dadosEntrega.rua,
            endNum = dadosEntrega.numero,
            bairro = dadosEntrega.bairro,
            cidade = dadosEntrega.cidade,
            uf = dadosEntrega.estado,
            cep = dadosEntrega.cep.Replace("-", ""),
            opInc = 436,
            opAlt = 436,
            idxTabela = idUsuario
        };
        _context.Enderecos.Add(enderecoUsuario);
        await _context.SaveChangesAsync();
        await _context.Entry(enderecoUsuario).ReloadAsync();
        return enderecoUsuario.pkEndereco;
    }

    public async Task CadastrarContato(TpaCadastroDTO cadastro)
    {
        var idContato = await CriarPkContato();
        var contatoUsuario = new TpaContatoDTO()
        {
            pkContato = idContato,
            idxCadastro = cadastro.pkCadastro,
            nome = cadastro.nome,
            email = cadastro.email,
            ddd = cadastro.ddd,
            telefone1 = cadastro.telefone1,
            sexo = cadastro.sexo,
            opInc = 436,
            opAlt = 436,
        };
        _context.Contatos.Add(contatoUsuario);
        await _context.SaveChangesAsync();
        await _context.Entry(contatoUsuario).ReloadAsync();
    }

    public async Task<UserKeys> BuscarUsuario(DadosCliente dadosCliente, DadosEntrega dadosEntrega)
    {
       var userKeys = await ClienteCadastrado(dadosCliente.cpf_cnpj);
       if(userKeys.pkCadastro == null)
        {
            var novoUsuario = await CadastrarUsuario(dadosCliente);
            var _pkEndereco = await CadastrarEnderecoUsuario(novoUsuario.pkCadastro, dadosEntrega);
            await CadastrarContato(novoUsuario);
            return new UserKeys(){pkCadastro = novoUsuario.pkCadastro, pkEndereco = _pkEndereco};
        }
        else
        {
            Console.WriteLine("Usuário já existe");
            Console.WriteLine(userKeys.pkCadastro);
        }
        return userKeys;
    }
}