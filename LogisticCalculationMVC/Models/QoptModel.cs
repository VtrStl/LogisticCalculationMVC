namespace LogisticCalculationMVC.Models
{
    public class QoptModel
    {
        public double VelikostPoptavky { get; set; }
        public double Npz { get; set; }
        public double Ns { get; set; }
        public double Nj { get; set; }
        public double Obdobi { get; set; }

        public double Qopt()
        {
            return Math.Ceiling(Math.Sqrt(2 * VelikostPoptavky * Npz) / Math.Sqrt(Nj * Ns * Obdobi));
        }
        
        public double PocetDavek()
        {
            return Math.Ceiling(VelikostPoptavky / Qopt());
        }

        public double PeriodicitaZadavani()
        {
            return Math.Ceiling(360 * Obdobi / PocetDavek());
        }
        
        public double CelkoveNaklady()
        {
            double PrislusneNaklady = Qopt() / 2 * Nj * Ns * Obdobi;
            return Math.Round(VelikostPoptavky / Qopt() * Npz + PrislusneNaklady, 2);
        }
    }
}