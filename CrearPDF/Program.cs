using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Crystal
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace CrearPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string DB = "PruebaCrytal";
                string Path = @"C:\Users\Sistemas\source\repos\CrearPDF\CrearPDF\CR\";
                string PathPDF = @"C:\Users\Sistemas\source\repos\CrearPDF\CrearPDF\PDF\";

                ReportDocument oRep = new ReportDocument();

                oRep.Load(Path + "PruebaPDF.rpt");

                oRep.SetDatabaseLogon("sa", "12345", "LT-OValladares", DB);
                //Conexion SQL para obtener DocEntre
                using (SqlConnection cn = new SqlConnection("Data Source=LT-OValladares; User Id=sa; Password=12345;Max Pool Size=2000"))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Select Top 1 ID,Usuario,Contra from "+ DB + ".dbo.TUsuarios", cn);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            oRep.SetParameterValue("@usuario", "Oscar");

                            oRep.ExportToDisk(ExportFormatType.PortableDocFormat, PathPDF + "Pruebas_" + DB +".pdf");
                            Console.WriteLine("Exito - PDF creado Correctamente");

                        }
                    }
                    else
                    {
                        Console.WriteLine("Info - No existen registros");
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);

            }
        }
    }
}
