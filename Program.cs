using baseDeDatos;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

class Program
{
    static void Main(string[] args)
    {

        try
        {
            bool trusted_connection;

            BeerDB beerDB = new BeerDB("DDC-CO-LGUERRE", "CsharpDB", trusted_connection = true);
            bool again = true;

            int op = 0;

            do
            {
                ShowMenu();
                Console.WriteLine("Elige una opción: ");

                op = int.Parse(Console.ReadLine());


                switch (op)
                {
                    case 1:
                        Show(beerDB);
                        break;

                    case 2:
                        Add(beerDB);
                        break;

                    case 3:
                        Edit(beerDB);
                        break;

                    case 4:
                        Delete(beerDB);
                        break;

                    case 5:
                        Console.WriteLine("Gracias por entrar a nuestro porta beer");
                        again = false;
                        break;
                }


            }while (again);
        }
        catch(SqlException ex)
        {
            Console.WriteLine("No hemos podido encontrar tu conexión");
        }


        static void ShowMenu()
        {
            Console.WriteLine("\n------ Menu -------");
            Console.WriteLine("1. - Mostrar");
            Console.WriteLine("2. - Agregar");
            Console.WriteLine("3. - Editar");
            Console.WriteLine("4. -Eliminar");
            Console.WriteLine("5. -Salir");
        }
        

        static void Show(BeerDB beerDB)
        {
            Console.Clear();
            Console.WriteLine("Cervezas presentes en la base de datos: ");

            List<Beer> beers = beerDB.GetAll();


            foreach (var beer in beers)
            {
                Console.WriteLine($"Id: {beer.Id} Nombre: {beer.Name}");
            }
        }


        static void Add(BeerDB beerDB)
        {
            Console.Clear();
            Console.WriteLine("Agregar nueva cerveza!");

            Console.WriteLine("Escribe el nombre: ");
            string name = Console.ReadLine();

            Console.WriteLine("Escribe el id de la marca: ");
            int brandId = int.Parse(Console.ReadLine());

            Beer beer = new Beer(name, brandId);

            beerDB.Add(beer);
        }


        static void Edit(BeerDB beerDB)
        {
            Console.Clear();
            Show(beerDB);
            Console.WriteLine("Editar Cerveza");
            Console.WriteLine("Escribe el id de tu cerveza a editar: ");

            int id = int.Parse(Console.ReadLine());

            Beer beer = beerDB.Get(id);

            if (beer != null)
            {
                Console.WriteLine("Escribe el nombre: ");
                string name = Console.ReadLine();

                Console.WriteLine("Escribe el id de la marca: ");
                int brandId = int.Parse(Console.ReadLine());


                beer.Name = name;
                beer.BrandId = brandId;
                beerDB.Edit(beer);
            }
            else
            {
                Console.WriteLine("La cerveza no esta registrada en la base de datos");
            }

        }



        static void Delete(BeerDB beerDB)
        {
            Console.Clear();
            Show(beerDB);
            Console.WriteLine("Eliminar Cerveza");
            Console.WriteLine("Escribe el id de tu cerveza a eliminar: ");

            int id = int.Parse(Console.ReadLine());

            Beer beer = beerDB.Get(id);

            if (beer != null)
            {
                beerDB.delete(id);
            }
            else
            {
                Console.WriteLine("La cerveza no esta registrada en la base de datos");
            }

        }


    }
}