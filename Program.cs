using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarSeries();
						break;
					case "2":
						InserirSerie();
						break;
					case "3":
						AtualizarSerie();
						break;
					case "4":
						ExcluirSerie();
						break;
					case "5":
						VisualizarSerie();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Thank you for using our services and see you soon!");
			Console.ReadLine();
        }

        private static void ExcluirSerie()
		{
			Console.Write("Insert series Id: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			repositorio.Exclui(indiceSerie);
		}

        private static void VisualizarSerie()
		{
			Console.Write("Insert series Id: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

        private static void AtualizarSerie()
		{
			Console.Write("Insert series Id: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Enter the series genre according to the options above: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Insert series title: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Insert series Launch date: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Insert series Description: ");
			string entradaDescricao = Console.ReadLine();

			Serie atualizaSerie = new Serie(id: indiceSerie,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
		}
        private static void ListarSeries()
		{
			Console.WriteLine("See catalog");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("No series registered, use option -2- in the console to add a new one.");
				return;
			}

			foreach (var serie in lista)
			{
                var excluido = serie.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Deleted*" : ""));
			}
		}

        private static void InserirSerie()
		{
			Console.WriteLine("Add a new series");

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Enter the series genre according to the options above: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Insert series title: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Insert series Launch date: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Insert series Description: ");
			string entradaDescricao = Console.ReadLine();

			Serie novaSerie = new Serie(id: repositorio.ProximoId(),
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Insere(novaSerie);
		}

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("Welcome to Santana Series!!!");
			Console.WriteLine("Select the option you want:");

			Console.WriteLine("1- See catalog");
			Console.WriteLine("2- Add a new series");
			Console.WriteLine("3- Update a series");
			Console.WriteLine("4- Delete a series");
			Console.WriteLine("5- See details of a series");
			Console.WriteLine("C- Clear Console");
			Console.WriteLine("X- Quit");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}
