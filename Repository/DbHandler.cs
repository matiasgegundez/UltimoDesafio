namespace UltimoDesafio.Repository
{
    public static class DbHandler
    {
        public static string GetConnectionString()
        {
            return "Server=localhost;Database=SistemaGestion;Trusted_Connection=True;";
        }
    }
}
