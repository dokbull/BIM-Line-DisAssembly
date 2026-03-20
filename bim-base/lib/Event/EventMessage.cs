using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum EventSubject
{
    NONE = 0,

    // 시스템
    LANGUAGE_CHANGED,
    LOGIN,

    // 모델
    MODEL_CREATE,
    MODEL_COPY,
    MODEL_DELETE,
    MODEL_RENAME,
    MODEL_ACTIVE,

    // UI
    SHOW_OP_CALL,       //이것만 사용 중
    INFOBAR,
    MESSAGE
}

public static class SenderNames
{
    public const string Automation = "Automation";
    public const string VISION = "VISION";
    public const string UI = "UI";
    public const string SYSTEM = "SYSTEM";
}

public class EventMessage
{
    public EventSubject Subject { get; set; }
    public object Param { get; set; }
    public string Sender { get; set; }

    public EventMessage(string sender, EventSubject subject, object param = null)
    {
        Sender = sender;
        Subject = subject;
        Param = param;
    }
}

public class MessageEventParam
{
    public string Title { get; set; }
    public string Message { get; set; }

}

public class OpCallMessageEventParam
{
    public int CallNum { get; set; }
    public string CallText { get; set; }

}

