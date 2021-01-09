using Scriban.Parsing;
using Scriban.Syntax;
using ScribanSourceSpan = Scriban.Parsing.SourceSpan;
using ScribanTextPosition = Scriban.Parsing.TextPosition;

namespace NTypewriter
{
    public enum MessageType
    {
        Error = 0,
        Warning = 1
    }

    public enum MesssageSource
    {
        Parsing,
        Rendering,
    }


    public class MessageItem
    {
        public MesssageSource Source { get;  }
        public MessageType Type { get; }
        public SourceSpan Span { get;  }
        public string Message { get;  }


        public MessageItem(LogMessage msg)
        {
            Source = MesssageSource.Parsing;
            Type = (MessageType)msg.Type;
            Message = msg.Message;
            Span = new SourceSpan(msg.Span);
        }

        public MessageItem(ScriptRuntimeException exception)
        {
            Source = MesssageSource.Rendering;
            Type = MessageType.Error;
            Message = exception.OriginalMessage;
            Span = new SourceSpan(exception.Span);
        }

        public override string ToString()
        {
            return $"{Span.FileName}({Span.Start.Line + 1},{Span.Start.Column + 1}) {Message}";
        }
    }


    public class SourceSpan
    {
        public string FileName { get;  }
        public TextPosition Start { get;  }
        public TextPosition End { get;  }


        public SourceSpan(ScribanSourceSpan span)
        {
            FileName = span.FileName;
            Start = new TextPosition(span.Start);
            End = new TextPosition(span.End);
        }
    }

    public class TextPosition
    {
        public int Offset { get;  }
        public int Column { get;  }
        public int Line { get;  }


        public TextPosition(ScribanTextPosition textPosition)
        {
            Offset = textPosition.Offset;
            Column = textPosition.Column;
            Line = textPosition.Line;
        }
    }
}