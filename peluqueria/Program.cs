

using MySql.Data;
using System;
using System.Data;
using MySql.Data.MySqlClient;

    namespace peluqueria
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string connectionString = "server=localhost;user=root;password=;database=peluqueriadb;";
            MySqlConnection connection = new MySqlConnection(connectionString);


            int numeroEmpleados;
            string cedula, apellido, nombre;
            int clientes;
            double valorCorte;
            double total = 0;

            Console.WriteLine("Digite el numero de empleados");
            numeroEmpleados = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < numeroEmpleados; i++)
            {
                Console.WriteLine("Digite la cedula del empleado");
                cedula = Console.ReadLine();
                Console.WriteLine("Digite el nombre del empleado");
                nombre = Console.ReadLine();
                Console.WriteLine("Digite el apellido del empleado");
                apellido = Console.ReadLine();
                Console.WriteLine("Digite el numero de clientes atendidos ");
                clientes = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Digite el precio del corte");
                valorCorte = double.Parse(Console.ReadLine());

                string insertQuery = $"INSERT INTO empleados (cedula_, nombre_, apellido_, clientes_, valor_corte) VALUES ('{cedula}', '{nombre}', '{apellido}', {clientes}, {valorCorte})";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                connection.Open();
                insertCommand.ExecuteNonQuery();
                connection.Close();
            }
            while (true)
            {
                Console.WriteLine("\nMENU DE OPCIONES:");
                Console.WriteLine("1. Actualizar empleado");
                Console.WriteLine("2. Eliminar empleado");
                Console.WriteLine("3. Listar empleados");
                Console.WriteLine("4. Calcular nómina");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                int opcion = Int32.Parse(Console.ReadLine());


                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Digite la cedula del empleado a actualizar: ");
                        string cedulaActualizar = Console.ReadLine();
                        Console.Write("Nuevo Nombre: ");
                        string nuevoNombre = Console.ReadLine();
                        Console.Write("Nuevo Apellido: ");
                        string nuevoApellido = Console.ReadLine();
                        Console.Write("Nuevas clientes: ");
                        int nuevo_clientes = int.Parse(Console.ReadLine());
                        Console.Write("Nuevo valor corte: ");
                        double nuevo_valorCorte = double.Parse(Console.ReadLine());
                        ActualizarEm(cedulaActualizar, nuevoNombre, nuevoApellido, nuevo_clientes, nuevo_valorCorte, connection);
                        break;
                        case 2:
                        Console.WriteLine("Digite la cedula del empleado a eliminar");
                        string cedulaEliminar = Console.ReadLine();
                        EliminarEmpleado(cedulaEliminar, connection);
                        break;
                        case 3:
                        ListarEmpleados(connection);
                        break;
                        case 4:
                        CalcularNomina(connection);
                        break;
                        case 5:
                        Console.WriteLine("Saliendo del programa");
                        return;
                        default: Console.WriteLine("Opcion invalida. Seleccione una opcion valida del menu");
                        break;


                }

            }


        }

     

        static void ActualizarEm( string cedula, string nuevoNombre, string nuevoApellido, int nuevo_clientes, double nuevo_valorCorte, MySqlConnection connection)
        {
            string updateQuery = $"UPDATE empleados SET nombre_ = '{nuevoNombre}', apellido_ = '{nuevoApellido}', clientes_ = {nuevo_clientes}, valor_corte = {nuevo_valorCorte} WHERE cedula_ = '{cedula}'";

            MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
            connection.Open();
            updateCommand.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine("Empleado actualizado exitosamente.");
        }

          static void CalcularNomina(MySqlConnection connection)
        {
            string selectQuery = "SELECT valor_corte, clientes_ FROM empleados";
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            connection.Open();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                double total = 0;
                while (reader.Read())
                {
                    double clientes = reader.GetDouble("valor_corte");
                    double valorCorte = reader.GetDouble("clientes_");
                    total += valorCorte * clientes;
                }

                Console.WriteLine("La nómina total es: " + total);
            }

            connection.Close();
        }
          static void ListarEmpleados(MySqlConnection connection)
        {
            string selectQuery = "SELECT cedula_, nombre_, apellido_, clientes_, valor_corte FROM empleados";
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            connection.Open();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("Lista de empleados:");
                while (reader.Read())
                {
                    string cedula = reader.GetString("cedula_");
                    string nombre = reader.GetString("nombre_");
                    string apellido = reader.GetString("apellido_");
                    int clientes = (int)reader.GetDouble("clientes_");
                    double valorCorte = reader.GetDouble("valor_corte"); ;
                    Console.WriteLine($"Cédula: {cedula}, Nombre: {nombre}, Apellido: {apellido}, : {clientes}, valorCorte: {valorCorte}");
                }
            }

            connection.Close();
        }

        static void EliminarEmpleado(string cedula, MySqlConnection connection)
            {
                string deleteQuery = $"DELETE FROM empleados WHERE cedula_ = '{cedula}'";

                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                connection.Open();
                deleteCommand.ExecuteNonQuery();
                connection.Close();

                Console.WriteLine("Empleado eliminado exitosamente.");
            }

        }

     



    }



    