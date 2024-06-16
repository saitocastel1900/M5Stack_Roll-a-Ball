using System;

public static class GameEnum
{
    /// <summary>
    /// 状態
    /// </summary>
    [Flags]
    public enum State
    {
        None      = 0,
        ReadyAsync = 1,
        Play      = 1 << 1,
        Result    = 1 << 2,
    }
}