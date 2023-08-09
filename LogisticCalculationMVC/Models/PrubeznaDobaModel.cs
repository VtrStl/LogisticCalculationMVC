namespace LogisticCalculationMVC.Models
{
    public class PrubeznaDobaModel
    {
        public int Tpz1 { get; set; }
        public int TkSum { get; set; }
        public int TkMax { get; set; }
        public int TmSum { get; set; }
        public int TmWithValue { get; set; }
        public int PocetPracovist { get; set; }
        public int SystemZpracovani { get; set; }
        public int PocetPracovniku { get; private set; }
    }
}