

using System.Xml;
using System.Xml.Linq;

namespace xmlManipulator;

class Program
{


    static void Main()
    {
        string path = @"A:\Projetos 2025\xmlManipulator\NFSe_E_17515300212_20250301_20250331.xml";
        string filepath = @"A:\Projetos 2025\tabela.cvs";

        
        /*var nf = GetNf(path);*/

        var nfe = new NotaFiscal();
        var listaNf = nfe.GetNotas();

        foreach ( var item in listaNf )
        {
            nfe = item;

            if(!nfe.AddNota())
            {
                Console.WriteLine($"{Database.MsgErro}");
            }
          Console.WriteLine("Processo encerrado");
        }


        /*Console.WriteLine(nf.ToString());*/

        /*CreateCvs(nf, filepath);*/
    }

    static NotaFiscal GetNf(string path)
    {
        XDocument doc = XDocument.Load(path);
        XNamespace ns = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd";

        int numero = int.TryParse(doc.Descendants(ns + "Numero").FirstOrDefault()?.Value, out int n) ? n : 0;
        string rSocial = doc.Descendants(ns + "TomadorServico").Descendants(ns + "RazaoSocial").FirstOrDefault()?.Value ?? "Desconhecido";
        double vBruto = double.TryParse(doc.Descendants(ns + "ValorServicos").FirstOrDefault()?.Value, out double vb) ? vb : 0.0;
        double vLiquido = double.TryParse(doc.Descendants(ns + "ValorLiquidoNfse").FirstOrDefault()?.Value, out double vl) ? vl : 0.0;
        DateTime dEmissao = DateTime.TryParse(doc.Descendants(ns + "DataEmissao").FirstOrDefault()?.Value, out DateTime d) ? d : DateTime.MinValue;
        string descr = doc.Descendants(ns + "Discriminacao").FirstOrDefault()?.Value ?? "Sem Descrição";
        NotaFiscal nfe = new NotaFiscal(numero, rSocial, vBruto, vLiquido, dEmissao, descr);
        return nfe;

    }
    static void CreateCvs(NotaFiscal nf, string filepath)
    {
        using (StreamWriter sw = new StreamWriter(filepath))
        {
            sw.WriteLine("Numero, Razao Social, Valor Bruto, Valor Liquido, Data Emissao, Descriçao");

            sw.WriteLine($"{nf.Numero}, {nf.RazaoSocialTomador}, {nf.ValorBruto}, {nf.ValorLiquido}, {nf.Descricao}");
            Console.WriteLine("Arquivo CSV gerado com sucesso!");
        }

    }
}