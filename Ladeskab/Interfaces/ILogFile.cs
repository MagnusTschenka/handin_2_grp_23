using System;
namespace Ladeskab
{
    public interface ILogFile
    {
        public void AppendTextLock(int id);
        public void AppendTextUnlock(int id);
    }
}

