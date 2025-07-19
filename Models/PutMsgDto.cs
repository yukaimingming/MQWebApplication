namespace MQWebApplication.Models;

public class PutMsgDto
{
    #region Properties

    public string? DataChannel { get; set; }
    public string? Msg { get; set; }
    public string? MsgId { get; set; }
    public string? QMgrName { get; set; }
    public int WaitInternal { get; set; }

    #endregion Properties
}