namespace FileHelperHeaders
{
    public class OutParameter : IOutParameter
    {
        public void WhyUseThese(out bool test)
        {
            test = true;
        }
    }
}