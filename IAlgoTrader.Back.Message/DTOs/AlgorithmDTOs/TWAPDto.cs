namespace IAlgoTrader.Back.Message.DTOs.AlgorithmDTOs
{
    public class TWAPDto
    {
        public ICollection<TWAPAverageDto> Averages { get; set; }
        public double TwapAmount { get; set; }
    }

    public class TWAPAverageDto
    {
        public string Date { get; set; }
        public double Amount { get; set; }
    }
}
