
using System;
using System.Collections.Generic;
using cat.itb.M6NF2Prac.CRUD;
using cat.itb.M6NF2Prac.Models;
using NHibernate.Util;

public class Program
{
    static void Main(string[] args)
    {
        // Crear objetos CRUD para las entidades
        GeneralCRUD generalCRUD = new GeneralCRUD();
        ProductCRUD productCRUD = new ProductCRUD();
        ClientCRUD clientCRUD = new ClientCRUD();
        ProviderCRUD providerCRUD = new ProviderCRUD();
        SalesPersonCRUD salesPersonCRUD = new SalesPersonCRUD();
        OrderProdCRUD orderProdCRUD = new OrderProdCRUD();

        generalCRUD.DeleteTables(new List<string> { "orderprod", "provider", "product", "salesperson", "client" });
        generalCRUD.CreateTables();
        while (true)
        {
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. ADO");
            Console.WriteLine("2. NHibernate");
            Console.WriteLine("3. Salir");
            string mainMenuChoice = Console.ReadLine();

            switch (mainMenuChoice)
            {
                case "1":
                    ShowADOMenu(productCRUD, clientCRUD, providerCRUD, salesPersonCRUD, orderProdCRUD);
                    break;

                case "2":
                    ShowNHibernateMenu(clientCRUD, salesPersonCRUD, orderProdCRUD, providerCRUD, productCRUD);
                    break;

                case "3":
                    return;

                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    static void ShowADOMenu(ProductCRUD productCRUD, ClientCRUD clientCRUD, ProviderCRUD providerCRUD, SalesPersonCRUD salesPersonCRUD, OrderProdCRUD orderProdCRUD)
    {
        while (true)
        {
            Console.WriteLine("\n--- MENÚ ADO ---");
            Console.WriteLine("1. Insertar clientes");
            Console.WriteLine("2. Seleccionar cliente por nombre");
            Console.WriteLine("3. Actualizar precio de productos");
            Console.WriteLine("4. Obtener proveedores con crédito menor a 6000");
            Console.WriteLine("5. Insertar vendedores");
            Console.WriteLine("6. Regresar");
            string adoChoice = Console.ReadLine();

            switch (adoChoice)
            {
                case "1":
                    InsertClients(clientCRUD);
                    break;

                case "2":
                    SelectClientByName(clientCRUD);
                    break;

                case "3":
                    UpdateProductPrices(productCRUD);
                    break;

                case "4":
                    SelectProvidersWithLowCredit(providerCRUD);
                    break;

                case "5":
                    InsertSalesPeople(salesPersonCRUD);
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    static void ShowNHibernateMenu(ClientCRUD clientCRUD, SalesPersonCRUD salesPersonCRUD, OrderProdCRUD orderProdCRUD, ProviderCRUD providerCRUD, ProductCRUD productCRUD)
    {
        while (true)
        {
            Console.WriteLine("\n--- MENÚ NHibernate ---");
            Console.WriteLine("6. Seleccionar número de comandas de un cliente");
            Console.WriteLine("7. Seleccionar vendedores por apellido");
            Console.WriteLine("8. Obtener productos con costo mayor a 1200");
            Console.WriteLine("9. Obtener el proveedor con el monto más bajo");
            Console.WriteLine("10. Insertar productos y proveedores");
            Console.WriteLine("11. Obtener y mostrar todos los clientes");
            Console.WriteLine("12. Actualizar crédito de proveedor");
            Console.WriteLine("13. Seleccionar productos con precio mayor a 100");
            Console.WriteLine("14. Seleccionar crédito de clientes");
            Console.WriteLine("15. Regresar");
            string nhibernateChoice = Console.ReadLine();

            switch (nhibernateChoice)
            {
                case "6":
                    SelectClientOrderCost(clientCRUD);
                    break;

                case "7":
                    SelectSalesPersonBySurname(salesPersonCRUD);
                    break;

                case "8":
                    SelectProductsByCost(orderProdCRUD);
                    break;

                case "9":
                    SelectProviderWithLowestAmount(providerCRUD);
                    break;

                case "10":
                    InsertProductsAndProviders(productCRUD, providerCRUD, salesPersonCRUD);
                    break;

                case "11":
                    DisplayAllClients(clientCRUD);
                    break;

                case "12":
                    UpdateProviderCredit(providerCRUD);
                    break;

                case "13":
                    SelectProductsWithPriceHigherThan100(productCRUD);
                    break;

                case "14":
                    SelectClientsWithHighCredit(clientCRUD);
                    break;

                case "15":
                    return;

                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    static void InsertClients(ClientCRUD clientCRUD)
    {
        // Crear y agregar clientes
        Client c1 = new Client() { Code = 2998, Name = "Sun Systems", Credit = 1000 };
        Client c2 = new Client() { Code = 2677, Name = "Roxy Stars", Credit = 2000 };
        Client c3 = new Client() { Code = 2865, Name = "Clen Ferrant", Credit = 2000 };
        Client c4 = new Client() { Code = 2873, Name = "Roast Coast", Credit = 2000 };

        // 1 -Insertar clientes en la base de datos
        clientCRUD.InsertADO(new List<Client> { c1, c2, c3, c4 });
    }

    static void SelectClientByName(ClientCRUD clientCRUD)
    {
        // 2 - Seleccionar un cliente por nombre usando ADO
        var sclient = clientCRUD.SelectByNameADO("Roast Coast");
        clientCRUD.DeleteADO(sclient);
    }

    static void UpdateProductPrices(ProductCRUD productCRUD)
    {
        // 3 - Actualizar el precio de los productos
        Product p1 = productCRUD.SelectByCodeADO(100890);
        p1.Price = 59.05f;
        productCRUD.UpdateADO(p1);
        Product p2 = productCRUD.SelectByCodeADO(200376);
        p2.Price = 25.56f;
        productCRUD.UpdateADO(p2);
        Product p3 = productCRUD.SelectByCodeADO(200380);
        p3.Price = 33.12f;
        productCRUD.UpdateADO(p3);
        Product p4 = productCRUD.SelectByCodeADO(100861);
        p4.Price = 17.34f;
        productCRUD.UpdateADO(p4);
    }

    static void SelectProvidersWithLowCredit(ProviderCRUD providerCRUD)
    {
        // 4 - Obtener proveedores con crédito menor a 6000
        var providers = providerCRUD.SelectCreditLowerThanADO(6000);
        foreach (var provider in providers)
        {
            Console.WriteLine($"Provider: {provider.Name}, {provider.Addres}, {provider.City}, {provider.StCode}, {provider.ZipCode}, {provider.Area}, {provider.Phone}, {provider.Amount}, {provider.Credit}, {provider.Remark}");
            Console.WriteLine("-------------PRODUCT THAT SELL-------------------");
            Console.WriteLine($"Product: {provider.Product.Description}, {provider.Product.CurrentStock}, {provider.Product.MinStock}, {provider.Product.Price}");
            Console.WriteLine("-------------------------------------------------");
        }
    }

    static void InsertSalesPeople(SalesPersonCRUD salesPersonCRUD)
    {
        // 5 - Insertar vendedores ADO
        SalesPerson sp1 = new SalesPerson() { Name = "WASHINGTON", Job = "MANAGER", StartDate = DateTime.Parse("1974-12-01"), Salary = 139000, Commission = 62000, Dep = "REPAIR" };
        SalesPerson sp2 = new SalesPerson() { Name = "FORD", Job = "ASSISTANT", StartDate = DateTime.Parse("1985-03-25"), Salary = 105000, Commission = 25000, Dep = "REPAIR" };
        SalesPerson sp3 = new SalesPerson() { Name = "FREEMAN", Job = "ASSISTANT", StartDate = DateTime.Parse("1965-09-12"), Salary = 90000, Commission = null, Dep = "REPAIR" };
        SalesPerson sp4 = new SalesPerson() { Name = "DAMON", Job = "ASSISTANT", StartDate = DateTime.Parse("1995-11-15"), Salary = 90000, Commission = null, Dep = "WOOD" };
        salesPersonCRUD.InsertADO(new List<SalesPerson> { sp1, sp2, sp3, sp4 });
    }

    // Métodos del menú NHibernate
    static void SelectClientOrderCost(ClientCRUD clientCRUD)
    {
        // 6 - Seleccionar numero comandas
        var clientOrder = clientCRUD.SelectByName("Carter & Sons");
        float costoAcumulado = 0;
        foreach (var client in clientOrder)
        {
            foreach (var comanda in client.OrderProds)
            {
                costoAcumulado += comanda.Cost;
            }
            Console.WriteLine($"Numero de comandas : {client.OrderProds.Count} y se ha gastado un total de {costoAcumulado}");
        }
    }

    static void SelectSalesPersonBySurname(SalesPersonCRUD salesPersonCRUD)
    {
        // 7 - Seleccionar vendedores por apellido
        var salePerson = salesPersonCRUD.SelectBySurname("YOUNG");
        foreach (var sp in salePerson)
        {
            foreach (var spr in sp.Products)
            {
                Console.WriteLine($"Provider: {spr._Provider.Name}, {spr._Provider.Addres}, {spr._Provider.City}, {spr._Provider.Phone}");
            }
        }
    }

    static void SelectProductsByCost(OrderProdCRUD orderProdCRUD)
    {
        // 8 - Obtener productos con costo mayor a 1200
        var orderProduct = orderProdCRUD.SelectByCostHigherThan(1200, 100);
        foreach (var op in orderProduct)
        {
            Console.WriteLine($"OrderDate: {op.OrderDate}, Cost: {op.Cost}, Amount: {op.Amount}");
            Console.WriteLine($"Product: {op._Product.Description}, {op._Product.CurrentStock}, {op._Product.MinStock}, {op._Product.Price}");
        }
    }

    static void SelectProviderWithLowestAmount(ProviderCRUD providerCRUD)
    {
        // 9 - Obtener el proveedor con el monto más bajo
        var lowestProvider = providerCRUD.SelectLowestAmount();
        foreach (var lp in lowestProvider)
        {
            Console.WriteLine($"Provider: {lp.Name}, {lp.Addres}, {lp.City}, {lp.StCode}, {lp.ZipCode}, {lp.Area}, {lp.Phone}, {lp.Amount}, {lp.Credit}, {lp.Remark}");
        }
    }

    static void InsertProductsAndProviders(ProductCRUD productCRUD, ProviderCRUD providerCRUD, SalesPersonCRUD salesPersonCRUD)
    {
        // 10 - Insertar productos
        Product newp1 = new Product()
        {
            Code = 1,
            Description = "SHUFFLE",
            CurrentStock = 100,
            MinStock = 10,
            Price = 100,
            SalesP = salesPersonCRUD.SelectBySurname("YOUNG").FirstOrDefault()
        };
        Product newp2 = new Product()
        {
            Code = 2,
            Description = "MEASUREMENT TAPE",
            CurrentStock = 200,
            MinStock = 20,
            Price = 200,
            SalesP = salesPersonCRUD.SelectBySurname("YOUNG").FirstOrDefault()
        };

        productCRUD.Insert(new List<Product> { newp1, newp2 });

        Provider provider1 = new Provider()
        {
            Name = "JUAN",
            Addres = "CALLE 1",
            City = "MADRID",
            StCode = "M",
            ZipCode = "28001",
            Area = 1,
            Phone = "123456789",
            Amount = 100,
            Credit = 1000,
            Remark = "REMARK",
            Product = newp1
        };

        Provider provider2 = new Provider()
        {
            Name = "PEDRO",
            Addres = "CALLE 2",
            City = "MADRID",
            StCode = "M",
            ZipCode = "28002",
            Area = 2,
            Phone = "987654321",
            Amount = 200,
            Credit = 2000,
            Remark = "REMARK",
            Product = newp2
        };

        providerCRUD.Insert(new List<Provider> { provider1, provider2 });
    }

    static void DisplayAllClients(ClientCRUD clientCRUD)
    {
        // 11 - Obtener y mostrar todos los clientes
        var allClients = clientCRUD.SelectAll();
        foreach (var c in allClients)
        {
            Console.WriteLine($"Client: {c.Name}, Code: {c.Code}, Credit: {c.Credit}");
        }
    }

    static void UpdateProviderCredit(ProviderCRUD providerCRUD)
    {
        // 12 - Actualizar credito
        var updateProvider = providerCRUD.SelectByCity("BELMONT");
        foreach (var up in updateProvider)
        {
            up.Credit = 25000;
            providerCRUD.Update(up);
        }
    }

    static void SelectProductsWithPriceHigherThan100(ProductCRUD productCRUD)
    {
        // 13 - Seleccionar descripcion y precio de los productos con precio mayor a 100
        var products = productCRUD.SelectByPriceHigherThan(100);
        foreach (var p in products)
        {
            Console.WriteLine($"Description: {p[0]}, Price: {p[1]}");
        }
    }

    static void SelectClientsWithHighCredit(ClientCRUD clientCRUD)
    {
        // 14 - Seleccionar credito de clientes
        var credit = clientCRUD.SelectByCredtiHigherThan(50000);
        foreach (var c in credit)
        {
            Console.WriteLine($"Client: {c.Name}, Credit: {c.Credit}");
        }
    }
}

