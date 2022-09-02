namespace IAlgoTrader.Back.Message.DTOs.AlgorithmDTOs
{
    public class TradeNumberDto
    {
        public IEnumerable<TradeNumberDetailDto> TradeNumbers { get; set; }
    }

    public class TradeNumberDetailDto
    {
        public string Date { get; set; }
        public int TradeNumber { get; set; }
    }
}
