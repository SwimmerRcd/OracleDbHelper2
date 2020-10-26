using System.Data;
using System.Threading.Tasks;
using static System.Console;

namespace oracleDbHelper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            OracleDbHelperAsyncTests tests = new OracleDbHelperAsyncTests();
            // await tests.OpenConnAsyncTest();
            // tests.ExcecuteSqlInsTest();
            // await tests.ExcecuteSqlInsAsyncTest();
            // await tests.ExecuteSqlUpdateTestAsync(201);
            // DataTable dt = await tests.ReadTableAsyncTest();
            // tests.PrintDataTableTest(dt);
            await tests.ExecuteProcNormalAsyncTest();
            // ReadLine();
            // tests.PrintDataTableTest(dt);
        }
    }

}
