namespace LogisticCalculationMVC.Models
{
    public class PrubeznaDobaModel
    {
        private int Tpz1 { get; set; }
        private int TkSum { get; set; }
        private int TkMax { get; set; }
        private int TmSum { get; set; }
        private int TmWithValue { get; set; }
        public int PocetPracovist { get; set; }
        public int PocetPracovniku { get; private set; }
        public int SystemZpracovani { get; set; }
        public int DavkaQ { get; set; }
        public int DavkaQd { get; set; }

        public PrubeznaDobaModel(PrubeznaDobaInputModel inputModel)
        {
            List<List<string>> prubeznaDobaList = inputModel.JsonData;

            Tpz1 = int.Parse(prubeznaDobaList[0][2]);
            TkSum = prubeznaDobaList.Sum(row => int.Parse(row[1])); 
            TkMax = prubeznaDobaList.Max(row => int.Parse(row[1]));
            TmSum = prubeznaDobaList.Sum(row => int.Parse(row[3]));
            TmWithValue = prubeznaDobaList.Count(row => int.Parse(row[3]) != 0);
            PocetPracovist = prubeznaDobaList.Count;
            DavkaQ = inputModel.DavkaQ;
            DavkaQd = inputModel.DavkaQd;
            SystemZpracovani = inputModel.Systemy;
        }

        private int SoubezneJednotlive()
        {
            PocetPracovniku = PocetPracovist + TmWithValue;
            return Tpz1 + TkSum + (DavkaQ - 1) * TkMax + TmSum;
        }

        private int SoubeznePoDavkach()
        {
            PocetPracovniku = PocetPracovist + TmWithValue - DavkaQd;
            return Tpz1 + DavkaQd * TkSum + (DavkaQ - DavkaQd) * TkMax + TmSum;
        }

        public int PrubeznaDobaVysledek()
        {
            return SystemZpracovani switch
            {
                0 => SoubezneJednotlive(),
                1 => SoubeznePoDavkach(),
                _ => 0
            };
        }
        public string PrubeznaDobaSystemyText()
        {
            return SystemZpracovani switch
            {
                0 => "Souběžně, jednotlivě, překryté",
                1 => "Souběžně, po dávkách, překryté",
                _ => ""
            };
        }
    }
}