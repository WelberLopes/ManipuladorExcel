using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Data.SqlClient;

namespace xmlManipulator;
public class Database
{
    public static string server = @"(localdb)\MSSQLLocalDB";
    public static string dataBase = "Notasfiscal";

    public static string MsgErro {  get; private set; }

    public static string StrCon
    {
        get 
        {
            return $"Data Source = {server};Initial Catalog = {dataBase};" +
                $" Integrated Security = True; Connect Timeout = 30;\r\nEncrypt=False;Trust " +
                $"Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";
        }
    }
    public static bool AddNota(NotaFiscal nota)
    {
        try
        {
            using (SqlConnection cn = new SqlConnection(StrCon))
            {
                cn.Open();
                var sql = "INSERT INTO NotaFiscal (Numero, RazaoSocial, ValorBruto, ValorLiquido, DataEmissao, Descricao) VALUES" +
                    "(@Numero, @RazaoSocial, @ValorBruto, @ValorLiquido, @DataEmissao, @Descricao)";

                using(SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Numero", nota.Numero);
                    cmd.Parameters.AddWithValue("@RazaoSocial", nota.RazaoSocialTomador);
                    cmd.Parameters.AddWithValue("@ValorBruto", nota.ValorBruto.ToString().Replace(",", "."));
                    cmd.Parameters.AddWithValue("@ValorLiquido", nota.ValorLiquido.ToString().Replace(",", "."));
                    cmd.Parameters.AddWithValue("@DataEmissao", nota.DataEmissao);
                    cmd.Parameters.AddWithValue("@Descricao", nota.Descricao);

                    cmd.ExecuteNonQuery();
                }

            }
            MsgErro = "";
            return true;
        }
        catch(Exception ex) 
        {
            MsgErro = ex.Message;
            return false;
        }
    }

}

