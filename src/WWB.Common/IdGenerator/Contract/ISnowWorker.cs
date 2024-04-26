namespace WWB.Common.IdGenerator
{
    internal interface ISnowWorker
    {
        //Action<OverCostActionArg> GenAction { get; set; }

        long NextId();
    }
}