using cat.itb.M6NF2Prac.CRUD;

public class Program()
{
    static void Main(string[] args)
    {
        GeneralCRUD generalCRUD = new GeneralCRUD();

        generalCRUD.CreateTables();
        //generalCRUD.DeleteTables(["client", "product", "orderprod", "provider", "salesperson"]);
    }
}
