using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmlManipulator
{
    public static class Excel
    {
        public static DataTable GetNotas()
        {
            var arquivo = @"A:\Projetos 2025\xmlManipulator\tabela.xlsx";
            var panilha = "SELECT * FROM [tabela$]";


            var dt = new DataTable();

            var strCon = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + arquivo + "; Extended " +
                "Properties=\"Excel 12.0; HDR=Yes; IMEX=0\"";

            using (OleDbConnection con = new OleDbConnection(strCon))
            {
                using (OleDbDataAdapter da = new OleDbDataAdapter(panilha, con))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
}
