namespace BasicWebServer.WebServer;

public static class Servidor
{
    // Definindo o número máximo de conexões simultâneas
    public static int maxConexoesSimultaneas = 20;
    // Criando um semáforo para gerenciar as conexões simultâneas
    private static Semaphore semaforo = new(maxConexoesSimultaneas, maxConexoesSimultaneas);

    // Método para iniciar o servidor
    public static void Iniciar()
    {
        List<IPAddress> ipsLocalHost = ObterIPsLocalHost();
        HttpListener escutador = InstanciarEscutador(ipsLocalHost);
        Iniciar(escutador);
    }

    // Método para recuperar os endereços IP do host local
    private static List<IPAddress> ObterIPsLocalHost()
    {
        IPHostEntry host; // Declarando um objeto IPHostEntry

        // Obtendo informações do host para "localhost"
        host = Dns.GetHostEntry(Dns.GetHostName());

        // Filtrando a lista de endereços IP para incluir apenas os endereços IPv4
        List<IPAddress> ipsFiltrados = host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToList();

        // Retornando a lista de endereços IPv4
        return ipsFiltrados;
    }

    // Método para instanciar e configurar o HttpListener
    private static HttpListener InstanciarEscutador(List<IPAddress> ipsLocalHost)
    {
        // Cria uma nova instância de HttpListener
        HttpListener escutador = new HttpListener();

        // Adiciona um prefixo que indica que o escutador deve ouvir as requisições HTTP no endereço "http://localhost/"
        escutador.Prefixes.Add("http://localhost/");

        // Itera sobre cada endereço IP fornecido na lista ipsLocalHost
        ipsLocalHost.ForEach(ip =>
        {
            // Imprime no console o endereço IP no qual o escutador está ouvindo
            Console.WriteLine("Ouvindo no IP " + "http://" + ip.ToString() + "/");

            // Adiciona o prefixo para cada endereço IP na lista, indicando que o escutador deve ouvir nesse endereço também
            escutador.Prefixes.Add("http://" + ip.ToString() + "/");
        });

        // Retorna a instância do HttpListener configurada
        return escutador;
    }

    // Método para iniciar o escutador
    private static void Iniciar(HttpListener escutador)
    {
        escutador.Start();
        // Inicia o servidor em uma nova tarefa
        Task.Run(() => ExecutarServidor(escutador));
    }

    // Método para executar o servidor
    private static void ExecutarServidor(HttpListener escutador)
    {
        while (true)
        {
            // Aguarda por uma conexão e decrementa o semáforo
            semaforo.WaitOne();
            // Inicia a conexão do escutador
            IniciarConexaoEscutador(escutador);
        }
    }

    // Método para iniciar a conexão do escutador
    private static async void IniciarConexaoEscutador(HttpListener escutador)
    {
        // Aguarda de forma assíncrona por um contexto de requisição HTTP
        HttpListenerContext contexto = await escutador.GetContextAsync();

        // Libera o semáforo após a conexão ser estabelecida
        semaforo.Release();
        Log(contexto.Request);

        // Define a resposta que será enviada ao navegador
        string resposta = @"<html><head><meta http-equiv='content-type' content='text/html; charset=utf-8'/>
      </head>Olá navegador!</html>";
        byte[] respostaCodificada = Encoding.UTF8.GetBytes(resposta);
        contexto.Response.ContentLength64 = respostaCodificada.Length;
        contexto.Response.OutputStream.Write(respostaCodificada, 0, respostaCodificada.Length);
        contexto.Response.OutputStream.Close();

    }

    public static void Log(HttpListenerRequest requisicao)
    {
        // Obtendo a parte da URL após o terceiro '/' (após 'http://', por exemplo)
        string caminho = string.Join("/", requisicao.Url.Segments.Skip(3));

        // Registrando informações sobre a requisição
        Console.WriteLine($"{requisicao.RemoteEndPoint} {requisicao.HttpMethod} /{caminho}");
    }
}

