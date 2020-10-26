using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using static Utils.OracleDbHelperAsync;
using static System.Console;
using System;
using System.Collections.Generic;
using System.Data;
namespace oracleDbHelper
{
    public class OracleDbHelperAsyncTests
    {
        public async Task OpenConnAsyncTest()
        {
            //given
            //when
            using (OracleConnection conn = await OpenConnAsync())
            {
                //then
                if (conn.State == ConnectionState.Open)
                {
                    WriteLine("conn is opened");
                }
            }
        }


        public async Task<DataTable> ReadTableAsyncTest()
        {
            //given
            string querySql = "select t.* from charactor t where rownum < 100";
            OracleConnection conn = await OpenConnAsync();
            //when
            DataTable dt = await ReadTable(querySql, null, conn);
            //then
            // string json = DataTableToJson(dt);
            // List<Charactor> charactors = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Charactor>>(json);
            // List<Charactor> charactors = DataTableToList<Charactor>(dt);
            // WriteLine(charactors.ToString());
            WriteLine(dt.Rows.Count);
            conn.Close();
            return dt;
        }

        public void PrintDataTableTest(DataTable dt)
        {
            PrintDataTable(dt);
        }

        public async Task ExecuteSqlUpdateTestAsync(int id)
        {
            //given
            string updateSql = $@"update charactor set name=Name||to_char(sysdate, 'yyyymmdd') where id = {id}";
            // OracleParameter[] param = new OracleParameter[]
            // {
            //     AddInputParameter("viSfTdyp", "Y", OracleDbType.Varchar2,2),
            //     AddInputParameter("viShangpId", "SPH11117186", OracleDbType.Varchar2, 50)
            // };
            OracleConnection conn = await OpenConnAsync();
            int affectRows = 0;
            //when
            try
            {
                affectRows = await ExecuteSqlAsync(updateSql, null, conn);
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
            //then
            finally
            {
                // Assert.True(affectRows > 0);
                conn.Close();
            }
        }


        // public void ExecuteSqlDelTest()
        // {
        //     //given
        //     string delSql = @"delete jc_zdwh_mx t where t.english_name = :viEnglishName and t.hanghao = :viHangHao";
        //     OracleParameter[] param = new OracleParameter[]
        //     {
        //         AddInputParameter("viEnglishName", "YAOP_CATEGORY", OracleDbType.Varchar2, 50),
        //         AddInputParameter("viHangHao", "28", OracleDbType.Varchar2, 50)
        //     };
        //     OracleConnection conn = OpenConn();
        //     int affectRows = 0;
        //     //when
        //     try
        //     {
        //         affectRows = ExecuteSql(delSql, param, conn);
        //     }
        //     catch (Exception ex)
        //     {
        //         Assert.True(ex.Message.Length > 0);
        //     }
        //     //then
        //     finally
        //     {
        //         Assert.True(affectRows >= 0);
        //         conn.Close();
        //     }
        // }


        public async Task ExcecuteSqlInsAsyncTest()
        {
            //given
            string insSql = @"insert into charactor
  (id, name, hitpoints, strength, defense, intelligence, class)
values
  (SEQ_CHARACTORS.NEXTVAL, :viName || to_char(sysdate,'yyyymmdd HH24:MI'), :viHitpoints, :viStrength, :viDefense, :viIntelligence, :viRpgclass)";
            OracleParameter[] param = new OracleParameter[]
            {
                // AddInputParameter("viId", 1, OracleDbType.Int64),
                AddInputParameter("viName", "Frodo", OracleDbType.Varchar2, 50),
                AddInputParameter("viHitpoints", 100, OracleDbType.Int64),
                AddInputParameter("viStrength", 10, OracleDbType.Int64),
                AddInputParameter("viDefense", 10, OracleDbType.Int64),
                AddInputParameter("viIntelligence", 10, OracleDbType.Int64),
                AddInputParameter("viRpgclass", "01", OracleDbType.Varchar2, 2)
            };
            OracleConnection conn = await OpenConnAsync();
            int affectRows = 0;
            //when
            try
            {
                affectRows = await ExecuteSqlAsync(insSql, param, conn);
                WriteLine(affectRows);
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
            //then
            finally
            {
                conn.Close();
            }
        }


        public async Task ExecuteProcNormalAsyncTest()
        {
            //given
            string procedureName = "PRC_UTL_GETSEQNO";
            OracleParameter[] param = new OracleParameter[]
            {
                AddInputParameter("iv_type", "DWI", OracleDbType.Varchar2, 50),
                AddInputParameter("iv_commit", "Y", OracleDbType.Varchar2, 50),
                AddOutputParameter("ov_seqno", OracleDbType.Varchar2, 50)
            };
            OracleConnection conn = await OpenConnAsync();
            //when
            await ExecuteProcAsync(procedureName, param, conn);
            //then
            WriteLine(param[2].Value);
        }


        // public void ExecuteFunctionTest()
        // {
        //     //given
        //     string fncName = "FNC_GET_SEQNO";
        //     OracleParameter[] param = new OracleParameter[]
        //     {
        //         AddInputParameter("iv_djlx", "DWI", OracleDbType.Varchar2, 50),
        //         AddInputParameter("iv_house_id", "CK000000001", OracleDbType.Varchar2, 50),
        //         AddReturnValue("rvNewDwId", OracleDbType.Varchar2, 50)
        //     };
        //     OracleConnection conn = OpenConn();
        //     //when
        //     ExecuteProc(fncName, param, conn);
        //     //then
        //     Assert.True(param[2] != null);
        // }
    }
}