using System;
using System.Collections.Generic;

public abstract class Veiculo
{
    public string Placa;
    public string Marca;
    public string Modelo;
    public int HorarioEntrada = 0; // em segundo
    public int HorarioSaida = 0; //em segunrdo

    public abstract float CalcularValor();
    public abstract void CriarVeiculo();
}

public class Carro : Veiculo
{
    public bool isUber = false;

    public override void CriarVeiculo()
    {
        Console.WriteLine("Digite a placa do carro: ");
        this.Placa = Console.ReadLine();

        Console.WriteLine("Digite a marca do carro: ");
        this.Marca = Console.ReadLine();

        Console.WriteLine("Digite o modelo do carro: ");
        this.Modelo = Console.ReadLine();

        Console.WriteLine("O cliente faz viagens no uber? (s/n)");
        string opc = "";

        while (opc != "s" && opc != "n")
        {
            opc = Console.ReadLine();

            if (opc != "s" && opc != "n")
            {
                Console.WriteLine("Digite uma opcao valida");
            }
        }

        if (opc == "s")
        {
            this.isUber = true;
        }
        else
        {
            this.isUber = false;
        }

        Console.WriteLine("Digite o horario de entrada: ");
        this.HorarioEntrada = int.Parse(Console.ReadLine());
    }

    public override float CalcularValor()
    {
        if (isUber)
        {
            if (this.HorarioSaida == 0)
            {
                Console.WriteLine("Defina o horario de saida");
                return 0.0f;
            }
            else
            {
                float vlr = (float)(HorarioSaida - HorarioEntrada) * 0.0056f * 0.85f; // o 0.0056 eh o preco por segundo, em media eh pra dar 20.56 reais por hora
                return vlr;
            }
        }
        else
        {
            float vlr = (float)(HorarioSaida - HorarioEntrada) * 0.0056f;
            return vlr;
        }
    }
}

public class Moto : Veiculo
{
    public override void CriarVeiculo()
    {
        Console.WriteLine("Digite a placa da moto: ");
        this.Placa = Console.ReadLine();

        Console.WriteLine("Digite a marca da moto: ");
        this.Marca = Console.ReadLine();

        Console.WriteLine("Digite o modelo da moto: ");
        this.Modelo = Console.ReadLine();

        Console.WriteLine("Digite o horario de entrada: ");
        this.HorarioEntrada = int.Parse(Console.ReadLine());
    }

    public override float CalcularValor()
    {
        float vlr = (float)(HorarioSaida - HorarioEntrada) * 0.004f; // o 0.004 eh o preco por segundo, em media eh pra dar 14.4 reais por hora
        return vlr;
    }
}

public class Estacionamento
{
    private int MaxVagas = 50;
    public int QtdeVagas = 0;
    private List<Veiculo> Veiculos = new List<Veiculo>();
    public float LucroLiquido = 0.0f;

    public bool RegistrarVeiculo(Veiculo v) // retorna false se deu alguma merda e retorna true se deu certo
    {
        // verificar se n existe um outro veiculo com a mesma placa, oq n faz sentido ter
        foreach (Veiculo veiculo in Veiculos)
        {
            if (veiculo.Placa == v.Placa)
            {
                Console.WriteLine("Ja existe um veiculo com a mesma placa registrado no estacionamento!");
                return false;
            }
        }

        if (QtdeVagas == MaxVagas)
        {
            Console.WriteLine("O estacionamento esta cheio!");
            return false;
        }

        Veiculos.Add(v);
        QtdeVagas++;
        Console.WriteLine("Veiculo registrado com sucesso!");
        return true;
    }

    private void RemoverVeiculo(String Placa)
    {
        bool temVeiculo = false;
        Veiculo VeiculoRemover = null;
        foreach (Veiculo veiculo in Veiculos)
        {
            if (veiculo.Placa == Placa)
            {
                temVeiculo = true;
                VeiculoRemover = veiculo;
            }
        }

        if (!temVeiculo)
        {
            Console.WriteLine("Placa do veiculo nao encontrada no estacionamento");
            return;
        }

        Veiculos.Remove(VeiculoRemover);
        QtdeVagas--;
        Console.WriteLine("Veiculo removido com sucesso");
        return;
    }

    public void PagarVeiculo(String Placa)
    {
        bool temVeiculo = false;
        Veiculo veiculoPagar = null;

        foreach (Veiculo veiculo in Veiculos)
        {
            if (veiculo.Placa == Placa)
            {
                temVeiculo = true;
                veiculoPagar = veiculo;
            }
        }

        if (!temVeiculo)
        {
            Console.WriteLine("Placa do veiculo nao encontrada no estacionamento");
            return;
        }

        int op = -1;

        Console.WriteLine("Digite o horario de saida");

        int saida = 0;

        while (saida < veiculoPagar.HorarioEntrada)
        {
            Console.WriteLine("Digite um horario de saida valido");
            saida = int.Parse(Console.ReadLine());
        }
        veiculoPagar.HorarioSaida = saida;
        while (op != 1 && op != 2 && op != 3)
        {
            Console.WriteLine("Qual vai ser a forma de pagamento?");
            Console.WriteLine("1 - debito\n2 - credito\n3 - pix");

            op = int.Parse(Console.ReadLine());

            if (op != 1 && op != 2 && op != 3)
            {
                Console.WriteLine("Digite uma opcao valida ne");
            }
        }

        float valorPagar = veiculoPagar.CalcularValor();

        if (op == 1)
        {
            Console.WriteLine($"Maquininha com valor para pagar de {valorPagar} reais");
        }
        else if (op == 2)
        {
            Console.WriteLine($"Maquininha com valor para pagar de {valorPagar} reais");
        }
        else if (op == 3)
        {
            Console.WriteLine("Codigo pix: bWUgZGEgbm90YQ==");
        }

        string resposta = "bruh";

        while (resposta != "s" && resposta != "n")
        {
            Console.WriteLine("O pagamento foi feito?");
            resposta = Console.ReadLine();

            if (resposta != "s" && resposta != "n")
            {
                Console.WriteLine("Digite uma resposta valida");
            }
        }

        if (resposta == "n")
        {
            Console.WriteLine("Pagamento nao foi concluido");
            return;
        }
        else if (resposta == "s")
        {
            LucroLiquido += valorPagar;
            Console.WriteLine("Pagamento foi concluido");
        }

        RemoverVeiculo(Placa);
        return;
    }

    public void MostrarVeiculos()
    {
        foreach (Veiculo v in Veiculos)
        {
            String tipoVeiculo = "";

            if (v is Carro)
            {
                tipoVeiculo = "Carro";
            }

            if (v is Moto)
            {
                tipoVeiculo = "Moto";
            }
            Console.WriteLine($"Tipo do veiculo: {tipoVeiculo} | Marca: {v.Marca} | Modelo: {v.Modelo} | Placa: {v.Placa} | Entrada: {v.HorarioEntrada}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Estacionamento estacionamento = new Estacionamento();

        Console.Clear(); // tira tudo do console pra ficar bonitinho
        Console.WriteLine("Sistema iniciado!");

        int opcao = 0;

        while (opcao != 4)
        {
            Console.WriteLine("Digite a opcao que deseja fazer:\n1 - Adicionar Veiculo\n2 - Mostrar Veiculos\n3 - Pagar Veiculo\n4 - Sair");
            opcao = int.Parse(Console.ReadLine());

            if (opcao != 1 && opcao != 2 && opcao != 3 && opcao != 4)
            {
                Console.WriteLine("Digite uma opcao valida");
            }

            if (opcao == 1)
            {
                int opc = 0;

                while (opc != 1 && opc != 2)
                {
                    Console.WriteLine("Selecione o tipo do veiculo:\n1 - Carro\n2 - Moto");

                    opc = int.Parse(Console.ReadLine());

                    if (opc != 1 && opc != 2)
                    {
                        Console.WriteLine("Digite uma opcao valida");
                    }
                }

                Veiculo veiculo = null;

                if (opc == 1)
                {
                    veiculo = new Carro();
                }
                else if (opc == 2)
                {
                    veiculo = new Moto();
                }

                veiculo.CriarVeiculo();

                estacionamento.RegistrarVeiculo(veiculo);
            }

            if (opcao == 2)
            {
                estacionamento.MostrarVeiculos();
            }

            if (opcao == 3)
            {
                Console.WriteLine("Digite a placa do veiculo");
                string Placa = Console.ReadLine();

                estacionamento.PagarVeiculo(Placa);
            }
        }

        Console.WriteLine("Sistema encerrado!");
    }
}
