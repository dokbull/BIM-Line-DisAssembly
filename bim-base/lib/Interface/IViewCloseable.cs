using System;

namespace bim_base.lib.Interfaces   // 👈 이거 중요
{
    public interface IViewCloseable
    {
        event Action OnCloseRequested;
    }
}
