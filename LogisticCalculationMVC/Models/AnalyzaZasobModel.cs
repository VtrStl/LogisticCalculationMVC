namespace LogisticCalculationMVC.Models
{
    public class AnalyzaZasobModel
    {
        public double Spotreba { get; set; }
        public double ObjednavaciDavka { get; set; }
        public double PojistnaZasoba { get; set; }
        public double PokrytiPoptavky { get; set; }
        public double DodaciLhuta { get; set; }
        public double DnyNaTyden { get; set; }
        public int IntervalKontroly { get; set; }
        public string? Systemy { get; set; }
        private double OcekavanaSpotreba { get; set; }


        public double ObjUrovenVysledek(string systemy)
        {
            OcekavanaSpotreba = Spotreba / DnyNaTyden;
            return systemy switch
            {
                "BQ" => BQsystem(),
                "sQ" => SQsystem(),
                _ => 0
            };
        }

        private double BQsystem()
        {
            switch (PokrytiPoptavky)
            {
                case > 0:
                    double xPojistnaZasoba = OcekavanaSpotreba * PokrytiPoptavky;
                    return Math.Ceiling(xPojistnaZasoba + DodaciLhuta * OcekavanaSpotreba);
                default:
                    return Math.Ceiling(PojistnaZasoba + DodaciLhuta * OcekavanaSpotreba);
            }
        }

        private double SQsystem()
        {
            switch (PokrytiPoptavky)
            {
                case > 0:
                    double xPojistnaZasoba = OcekavanaSpotreba * PokrytiPoptavky;
                    return Math.Ceiling(xPojistnaZasoba + OcekavanaSpotreba * (DodaciLhuta + 0.7 * IntervalKontroly));
                default:
                    return Math.Ceiling(PojistnaZasoba + OcekavanaSpotreba * (DodaciLhuta + 0.7 * IntervalKontroly));
            }
        }
        
        public double PrumernaZasoba()
        {
            double TydnyNaDny = DnyNaTyden * 7;
            return Math.Round(TydnyNaDny / OcekavanaSpotreba, 2);
        }
        
        public double PocetObjednavekZaRok()
        {
            return Math.Ceiling(Spotreba / ObjednavaciDavka);
        }

        public string ObjUrovenText(string systemy)
        {
            return systemy switch
            {
                "BQ" => "B,Q",
                "sQ" => "s,Q",
                _ => ""
            };
        }
    }
}