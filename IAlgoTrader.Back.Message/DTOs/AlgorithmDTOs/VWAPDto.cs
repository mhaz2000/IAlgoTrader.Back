namespace IAlgoTrader.Back.Message.DTOs.AlgorithmDTOs
{
    public class VWAPDto
    {
        public ICollection<VWAPDetailDto> VWAPs { get; set; }
    }

    public class VWAPDetailDto
    {
        public string Date { get; set; }
        public double Amount { get; set; }
        public double ClosePrice { get; set; }
    }
}
