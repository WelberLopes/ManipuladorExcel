using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmlManipulator
{
     public class NotaFiscal
    {
        public int Numero {  get; set; }
        public string RazaoSocialTomador { get; set; }
        public double ValorBruto { get; set; }
        public double ValorLiquido { get; set; }
        public DateTime DataEmissao { get; set; }
        public string Descricao {  get; set; }

        public NotaFiscal() { }
        public NotaFiscal(int numero, string rSocial, double VBruto, double VLiquido, DateTime dEmissao, string descr) {

            Numero = numero;
            RazaoSocialTomador = rSocial;
            ValorBruto = VBruto;
            ValorLiquido = VLiquido;
            DataEmissao = dEmissao;
            Descricao = descr;
        }

        public override string ToString()
        {
            return "Razão Social: " + RazaoSocialTomador.ToString() + ", Valor Liquido: " 
                + ValorLiquido + ", Valor Bruto: " + ValorBruto + ", Numero da Nota: " 
                + Numero + ", Data Emissão: " + DataEmissao+ ", Descrição: " + Descricao;
        }

        public List<NotaFiscal> GetNotas()
        {
            var lista = new List<NotaFiscal>();
            var dt = Excel.GetNotas();
            foreach (DataRow nota in dt.Rows)
            {
                lista.Add(new NotaFiscal(Convert.ToInt32(nota["Numero"]), nota["RazãoSocial"].ToString(),
                    Convert.ToDouble(nota["ValorBruto"]), Convert.ToDouble(nota["ValorLiquido"]), 
                    Convert.ToDateTime(nota["DataEmissao"]), nota["Descricao"].ToString()));
            }
            return lista; ;
        }

        public bool AddNota()
        {
            return Database.AddNota(this);
        }

    }
}
